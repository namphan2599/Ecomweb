using Ecomweb.Data;
using Ecomweb.Data.Dto;
using Ecomweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ecomweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly EcomContext _context;
        private readonly JwtGenerator _jwtGenerator;

        private readonly IPasswordHasher _passwordHasher;

        public AuthController(EcomContext context, JwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            IActionResult response = Unauthorized();

            var user = await _context.Users
                        .Where(user => user.Name == userLoginDto.Username)
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


            var jwtToken = _jwtGenerator.GenerateToken(user);

            return Ok(new
            {
                token = jwtToken,
                user,
            });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { id, name, role });
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
    }
}
