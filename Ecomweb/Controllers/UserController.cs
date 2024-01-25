using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecomweb.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Ecomweb.Interfaces;
using Ecomweb.Data.Dto;

namespace ecomweb
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly EcomContext _context;
        private IConfiguration _config;

        private IPasswordHasher _passwordHasher;

        public UserController(EcomContext context, IConfiguration config, IPasswordHasher passwordHasher)
        {
            _context = context;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(UserCreateDto userCreateDto)
        {

            if (await _context.Users.Where(x => x.Username == userCreateDto.Username).AnyAsync())
            {
                throw new BadHttpRequestException("Name is in use");
            }

            var salt = Guid.NewGuid().ToByteArray();
            var user = new User()
            {
                Username = userCreateDto.Username,
                Password = await _passwordHasher.Hash(userCreateDto.Password, salt),
                Salt = salt
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            IActionResult response = Unauthorized();

            var user = await _context.Users
                        .Where(user => user.Username == userLoginDto.Username)
                        .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new BadHttpRequestException("Invalid Name / Password");
            }

            if (
                !user.Password.SequenceEqual(
                    await _passwordHasher.Hash(userLoginDto.Password, user.Salt))
            )
            {
                throw new BadHttpRequestException("Invalid Name / Password");
            }

            var issuer = _config["Jwt:Issuer"];
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(30),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(new { token = stringToken });
        }

        [HttpGet("secret")]
        public string Test()
        {
            return "?";
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
