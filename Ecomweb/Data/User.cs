using System.Text.Json.Serialization;

namespace Ecomweb.Data;

public class User
{
  public long Id { get; set; }
  public string Username { get; set; } = null!;

  [JsonIgnore]
  public string Password { get; set; } = null!;
  public Cart? Cart { get; set; }
}