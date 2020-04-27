using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ihud.WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ihud.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ForumDbContext _context;

        public TopicsController( ForumDbContext context )
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicViewModel>>> GetTopic( int? categoryId, int? userId, int? skip, int? take )
        {
            //дописать маппер
            var topics = _context.Topic.Include(x => x.User).Include(t => t.TopicReply).OrderBy(m => m.UpdatedAt).AsQueryable().TopicToTopicViewModel();
            if (categoryId != null && categoryId > 0)
            {
                topics = topics.Where(t => t.CategoryId == categoryId);
            }
            if (userId != null && userId > 0)
            {
                topics = topics.Where(t => t.UserId == userId);
            }

            if (skip != null && skip > 0)
            {
                topics = topics.Skip(( int )skip);
            }

            if (take != null && take > 0)
            {
                topics = topics.Take(( int )take);
            }


            return await topics.ToListAsync();
        }

        // GET: api/Topics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicViewModel>> GetTopic( int id )
        {
            var topic = await _context.Topic.TopicToTopicViewModel().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic( int id, Topic topic )
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic( TopicSubmit topicS )
        {
            if (topicS.Category == null || topicS.Description == null || topicS.Username == null || topicS.Title == null || topicS.Category == "" || topicS.Description == "" || topicS.Username == "" || topicS.Title == "" || topicS == null)
            { return null; }
            Category cat = new Category();
            User usr = new User();

            cat = _context.Category.Where(x => x.Title == topicS.Category).FirstOrDefault();
            //Если ничего не нашли то записываем как анонимного пользователя
            usr = ( _context.User.Where(x => x.DisplayName == topicS.Username).Count() == 0 ?
                _context.User.Where(x => x.DisplayName.ToLower() == "anonym").FirstOrDefault() :
                _context.User.Where(x => x.DisplayName == topicS.Username).FirstOrDefault() );

            Topic tempt = new Topic();
            tempt.CategoryId = cat.Id;
            tempt.UserId = usr.Id;
            tempt.Title = topicS.Title;
            tempt.Description = topicS.Description;
            tempt.UserDisplayname = topicS.Username;

            _context.Topic.Add(tempt);
            await _context.SaveChangesAsync();
            var topicView = await _context.Topic.TopicToTopicViewModel().Where(x => x.Id == tempt.Id).FirstOrDefaultAsync();
            return CreatedAtAction("GetTopic", new { id = tempt.Id }, topicView);
        }

        // DELETE: api/Topics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Topic>> DeleteTopic( int id )
        {
            var topic = await _context.Topic.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }

            _context.Topic.Remove(topic);
            await _context.SaveChangesAsync();

            return topic;
        }

        private bool TopicExists( int id )
        {
            return _context.Topic.Any(e => e.Id == id);
        }
    }
}
