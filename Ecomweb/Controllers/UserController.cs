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

        private IPasswordHasher _passwordHasher;

        public UserController(EcomContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
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
        public async Task<ActionResult<User>> CreateUser(UserCreateDto userCreateDto)
        {

            if (await _context.Users.Where(x => x.Name == userCreateDto.Username).AnyAsync())
            {
                throw new BadHttpRequestException("Name is in use");
            }

            var salt = Guid.NewGuid().ToByteArray();
            var user = new User()
            {
                Name = userCreateDto.Username,
                PasswordHash = await _passwordHasher.Hash(userCreateDto.Password, salt),
                Salt = salt,
                Email = userCreateDto.Email,
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
            if (await _context.Users.Where(x => x.Name == userCreateDto.Username).AnyAsync())
            {
                throw new BadHttpRequestException("Name is in use");
            }

            var salt = Guid.NewGuid().ToByteArray();
            var user = new User()
            {
                Name = userCreateDto.Username,
                Role = "admin",
                PasswordHash = await _passwordHasher.Hash(userCreateDto.Password, salt),
                Salt = salt,
                Email = userCreateDto.Email,
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
