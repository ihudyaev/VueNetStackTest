using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ihud.WebApi.Connector;
using ihud.WebApi.Models;
using Nest;
using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ihud.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ForumDbContext _context;
        private readonly ConnectionToEs _connectionToEs;

        public SearchController( ForumDbContext context )
        {
            _context = context;
            _connectionToEs = new ConnectionToEs();
        }

        //Метод для обновления индекса для удобства во время разработки
        [HttpGet("refreshindex")]
        public async void GetRefreshIndex()
        {
            List<Topic> topics = _context.Topic.ToList();
            List<TopicReply> topicreplies = _context.TopicReply.ToList();
            List<Category> categories = _context.Category.ToList();

            List<ESSearchItem> searchItems = new List<ESSearchItem>();
            foreach (var item in topics)
            {
                searchItems.Add(new ESSearchItem(item));
            }

            foreach (var item in topicreplies)
            {
                searchItems.Add(new ESSearchItem(item));
            }
            foreach (var item in categories)
            {
                searchItems.Add(new ESSearchItem(item));
            }
            await _connectionToEs.EsClient().Indices.DeleteAsync("forumtestindex");
            var response = await _connectionToEs.EsClient().IndexManyAsync(searchItems, "forumtestindex");
        }

        [HttpPost("dataseacrh")]
        public async Task<ActionResult<ESSearchItem>> DataSearchAsync( [FromBody] ForumSearchRequest searchreq )
        {
            var client = _connectionToEs.EsClient();

            //var searchresult = await client.SearchAsync<ESSearchItem>(
            //    s =>
            //s.Index("forumtestindex").MatchAll());

            var searchresult =await client.SearchAsync<ESSearchItem>(s => s
                        .Size(searchreq.Size)
                        .Index("forumtestindex")
                        .Query(q => q.QueryString(

                            qs => qs.Query(searchreq.SearchString)
                            )

                        )
                    );

            var Result = searchresult.Documents;

            return Ok(Result);
        }
    }
}