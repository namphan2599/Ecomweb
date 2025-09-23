using System.Security.Claims;
using Ecomweb.Interfaces;
namespace Ecomweb.Services;
public class CurrentUserAccessor : ICurrentUserAccessor
{

  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string PrintText()
  {
    return "Hello";
  }

  public string? GetUserName()
  {
    return _httpContextAccessor.HttpContext
            ?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;
  }

  public int GetUserId()
  {

        return 0;
    //var userIdString = _httpContextAccessor.HttpContext
    //        ?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
    //        ?.Value;

    //if (int.TryParse(userIdString, out int userId))
    //{
    //  return userId;
    //}
    //throw new Exception("User ID is not valid");
  }
}