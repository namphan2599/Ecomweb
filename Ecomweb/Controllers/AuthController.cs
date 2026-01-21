using Ecomweb.Data;
using Ecomweb.Data.Dto;
using Ecomweb.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AuthController(EcomContext context, JwtGenerator jwtGenerator, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
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

        // New endpoint: client posts Google ID token here
        [HttpPost("google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleSignIn(GoogleLoginDto dto)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _configuration["Google:ClientId"] }
                };

                payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);
            }
            catch
            {
                return BadRequest("Invalid Google token.");
            }

            // Extract fields
            var email = payload.Email ?? string.Empty;
            var name = string.IsNullOrWhiteSpace(payload.Name) ? email.Split('@')[0] : payload.Name;

            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Google token does not contain an email.");
            }

            // Find or create local user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    IsActive = true,
                    Role = "user"
                    // PasswordHash and Salt left as defaults since authentication is external
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
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
