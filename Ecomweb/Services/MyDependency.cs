using System.Security.Claims;
using Ecomweb.Interfaces;
namespace Ecomweb.Services;
public class MyDependency : IMyDependency
{

  private readonly IHttpContextAccessor _httpContextAccessor;

  public MyDependency(IHttpContextAccessor httpContextAccessor)
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
}