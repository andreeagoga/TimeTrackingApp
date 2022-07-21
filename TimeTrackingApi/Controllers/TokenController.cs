using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApi.Models;
using TimeTrackingApi.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace TimeTrackingApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly TimeTrackingContext _context;

         private readonly PasswordHasher<User> passwordHasher;

        public TokenController(IConfiguration config, TimeTrackingContext context)
        {
            _configuration = config;
            _context = context;
            this.passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost]
        [Route("register")]

        public async Task<ActionResult<User>> Register(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if(await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email))
            {
                return BadRequest("Username ot email already exists");
            }
            
            if(user.Password.Length < 6)
            {
                return BadRequest("Password must be at least 6 characters long");
            }

            user.IsConfirmed = false;
            user.Password = PasswordUtils.ComputeSha256Hash(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("confirm/{username}")]
        public IActionResult Confirm(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            user.IsConfirmed = true;
            _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticateUser(User userData)
        {
            if (userData != null && userData.Username != null && userData.Password != null && userData.IsConfirmed != false)
            {
                var user = await GetUser(userData.Username, PasswordUtils.ComputeSha256Hash(userData.Password));

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Username", user.Username),
                        new Claim("IsConfirmed", user.IsConfirmed.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(100),
                        signingCredentials: signIn);

                    return Ok( new 
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}