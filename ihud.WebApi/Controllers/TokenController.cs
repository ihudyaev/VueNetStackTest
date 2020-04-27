using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ihud.WebApi.Models;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace ihud.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ForumDbContext _context;

        public TokenController( IConfiguration config, ForumDbContext context )
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] UserSubmit _userData )
        {
            ClaimsIdentity identity = new ClaimsIdentity();
            if (_userData != null && _userData.email != null && _userData.password != null)
            {
                var user = await GetUser(_userData.email, _userData.password);

                if (user != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    //new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserName", user.UserName),
                    new Claim("DisplayName", user.DisplayName),
                    new Claim("Email", user.Email)};

                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                    var now = DateTime.UtcNow;
                    // создаем JWT-токен
                    var token = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            notBefore: now,
                            claims: claimsIdentity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
                    var UserAnswerResponse = new UserAnswer();
                    UserAnswerResponse.profile = new Profile(user);
                    UserAnswerResponse.token = encodedJwt;


                    return Ok(UserAnswerResponse);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid credentials");
            }

        }

        private async Task<User> GetUser( string email, string password )
        {
            string Hash = ihud.WebApi.Models.User.GetPasswordHash(password);
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == Hash);
        }
    }
}