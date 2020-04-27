using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ihud.WebApi.Models;

namespace ihud.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicRepliesController : ControllerBase
    {
        private readonly ForumDbContext _context;

        public TopicRepliesController(ForumDbContext context)
        {
            _context = context;
        }

        // GET: api/TopicReply
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicReplyViewModel>>> GetTopicReplies( int? topicId, int? userId, int? skip, int? take )
        {
            
            var topicReplies = _context.TopicReply.Include(x => x.User).Include(c=>c.Topic).ThenInclude(t => t.Category).OrderBy(m=> m.UpdatedAt).AsQueryable().TopicReplyToTopicReplyViewModel();
            if (topicId != null && topicId > 0)
            {
                topicReplies = topicReplies.Where(t => t.TopicId == topicId);
            }
            if (userId != null && userId > 0)
            {
                    topicReplies = topicReplies.Where(t => t.UserId == userId);
            }

            if (skip != null && skip > 0)
            {
                    topicReplies = topicReplies.Skip(( int )skip);
            }

            if (take != null && take > 0)
            {
                    topicReplies = topicReplies.Take(( int )take);
            }
            return await topicReplies.ToListAsync();
        }

        // GET: api/TopicReplies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicReplyViewModel>> GetTopicReply(int id)
        {
            
            var topicReply = await _context.TopicReply.TopicReplyToTopicReplyViewModel().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (topicReply == null)
            {
                return NotFound();
            }

            return topicReply;
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopicReply(int id, TopicReply topicReply)
        {
            if (id != topicReply.Id)
            {
                return BadRequest();
            }

            _context.Entry(topicReply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicReplyExists(id))
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
        public async Task<ActionResult<TopicReplyViewModel>> PostTopicReply(TopicReplySubmit topicReplyS)
        {
            if (topicReplyS.Description == null || topicReplyS.UserName == null ||  topicReplyS.Description == "" || topicReplyS.UserName == "" || topicReplyS == null)
            { return null; }
            Topic top = new Topic();
            User usr = new User();
            top = _context.Topic.Where(x => x.Id == topicReplyS.TopicId).FirstOrDefault();
            //Если ничего не нашли то записываем как анонимного пользователя
            usr = (topicReplyS.isAnonym ?
                _context.User.Where(x => x.DisplayName.ToLower() == "anonym").FirstOrDefault() :
                _context.User.Where(x => x.DisplayName == topicReplyS.UserName).FirstOrDefault() );

            TopicReply tempt = new TopicReply();
            tempt.TopicId = top.Id;
            tempt.UserId = usr.Id;
            tempt.UserDisplayname = topicReplyS.UserName;
            tempt.Description = topicReplyS.Description;

            _context.TopicReply.Add(tempt);
            await _context.SaveChangesAsync();
            var topicReplyView = await _context.TopicReply.TopicReplyToTopicReplyViewModel().Where(x => x.Id == tempt.Id).FirstOrDefaultAsync();
            return CreatedAtAction("GetTopicReply", new { id = tempt.Id }, topicReplyView);
        }

        // DELETE: api/TopicReplies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopicReply>> DeleteTopicReply(int id)
        {
            var topicReply = await _context.TopicReply.FindAsync(id);
            if (topicReply == null)
            {
                return NotFound();
            }

            _context.TopicReply.Remove(topicReply);
            await _context.SaveChangesAsync();

            return topicReply;
        }

        private bool TopicReplyExists(int id)
        {
            return _context.TopicReply.Any(e => e.Id == id);
        }
    }
}
