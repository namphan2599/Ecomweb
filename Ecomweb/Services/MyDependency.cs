using Ecomweb.Interfaces;
namespace Ecomweb.Services;
public class MyDependency : IMyDependency
{
  public string PrintText()
  {
    return "Hello";
  }
}