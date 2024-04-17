namespace Ecomweb.Interfaces;
public interface ICurrentUserAccessor
{
  string PrintText();
  string? GetUserName();
}