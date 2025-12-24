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
using Ecomweb.Services;

namespace ecomweb
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly EcomContext _context;
        private JwtGenerator _jwtGenerator;

        private IPasswordHasher _passwordHasher;

        public UserController(EcomContext context, JwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
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
                PasswordHash = await _passwordHasher.Hash(userCreateDto.Password, salt),
                Salt = salt,
                Email = userCreateDto.Email,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
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

        [HttpPost("admin")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostAdmin(UserCreateDto userCreateDto)
        {
            if (await _context.Users.Where(x => x.Username == userCreateDto.Username).AnyAsync())
            {
                throw new BadHttpRequestException("Name is in use");
            }

            var salt = Guid.NewGuid().ToByteArray();
            var user = new User()
            {
                Username = userCreateDto.Username,
                Role = "admin",
                PasswordHash = await _passwordHasher.Hash(userCreateDto.Password, salt),
                Salt = salt,
                Email = userCreateDto.Email,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
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
                !user.PasswordHash.SequenceEqual(
                    await _passwordHasher.Hash(userLoginDto.Password, user.Salt))
            )
            {
                throw new BadHttpRequestException("Invalid Name / Password");
            }


            var jwtToken = _jwtGenerator.GenToken(user);

            return Ok(new { 
                token = jwtToken,
                user,
            });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { userId, username, role });
        }

        [HttpGet("secret")]
        public string Test()
        {
            return "Authenticated";
        }

        [HttpGet("admin-test")]
        [Authorize(Roles = "admin")]
        public string AdminTest()
        {
            return "Admin Authenticated";
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
