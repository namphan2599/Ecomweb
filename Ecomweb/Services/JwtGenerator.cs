using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecomweb.Data;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;

namespace Ecomweb.Services;
public class JwtGenerator
{

  private readonly IConfiguration _config;
  public JwtGenerator(IConfiguration config)
  {
    _config = config;
  }

  public string GenToken(User user)
  {
    var issuer = _config["Jwt:Issuer"];
    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Username),
          new Claim(ClaimTypes.Role, user.Role),
          new Claim(JwtRegisteredClaimNames.Sub, user.Username),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
    return jwtToken;
  }
}