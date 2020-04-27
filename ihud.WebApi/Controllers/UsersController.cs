using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ihud.WebApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ihud.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ForumDbContext _context;

        public UsersController( ForumDbContext context )
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpGet("profile/{username}")]
        public async Task<ActionResult<Profile>> GetProfile( string username )
        {
            string Searchstr = username.Substring(username.IndexOf('@') + 1);
            var user = await _context.User.Include(t => t.Topic).Include(r => r.TopicReply).FirstOrDefaultAsync(x => x.DisplayName.ToLower() == Searchstr.ToLower());

            if (user == null)
            {
                return NotFound();
            }

            return new Profile(user);
        }

        [Authorize]
        [HttpGet("profileFull/{username}")]
        public async Task<ActionResult<ProfileFull>> GetProfileFull( string username )
        {
            string Searchstr = username.Substring(username.IndexOf('@') + 1);
            var user = await _context.User.Include(t => t.Topic).Include(r => r.TopicReply).FirstOrDefaultAsync(x => x.DisplayName.ToLower() == Searchstr.ToLower());

            if (user == null)
            {
                return NotFound();
            }

            return new ProfileFull(user);
        }

        [Authorize]
        [HttpPut("profileFull")]
        public async Task<ActionResult<ProfileFull>> UpdateProfile([FromBody] ProfileUpdateForm _userData )
        {
            
            string UserName = this.User.Claims.First(i => i.Type == "UserName").Value;

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (user == null)
            {
                return BadRequest();
            }

            var UserAnswerResponse = new UserAnswer();
            UserAnswerResponse.profilefull = new ProfileFull(user);

            user.Name = _userData.name;
            user.Surname = _userData.surname;
            user.Birthdate = _userData.birthdate;
            user.DisplayName = _userData.displayname;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(UserAnswerResponse);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser( int id )
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        private async Task<User> CheckUser( string email, string username )
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email || u.UserName == username);
        }

        [HttpPost("check")]
        [Produces("application/json")]
        public async Task<IActionResult> Post( [FromBody] UserRegisterCheck _userData )
        {
            UserMessageAnswer UserAnswerResponse = new UserMessageAnswer();
            if (_userData != null && _userData.email != null && _userData.username != null)
            {
                var user = await CheckUser(_userData.email, _userData.username);

                if (user != null)
                {
                    UserAnswerResponse.Messages.Add("Username or login already exist");
                };
            }
            return Ok(UserAnswerResponse);

        }

        //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet("userinfo")]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByToken()
        {
            string UserName = this.User.Claims.First(i => i.Type == "UserName").Value;

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == UserName);

            var UserAnswerResponse = new UserAnswer();
            UserAnswerResponse.profile = new Profile(user);
            
            return Ok(UserAnswerResponse);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser( int id, User user )
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser( User user )
        //{
        //    _context.User.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        [HttpPost]
        public async Task<ActionResult<User>> PostUser( UserRegisterForm userForm )
        {
            User user = new User(userForm);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser( int id )
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists( int id )
        {
            return _context.User.Any(e => e.Id == id);
        }
        private bool UserExists( string username )
        {
            return _context.User.Any(e => e.UserName == username);
        }
    }
}
