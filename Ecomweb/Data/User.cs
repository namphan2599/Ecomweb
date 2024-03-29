using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Ecomweb.Data;

public class User
{
  public int Id { get; set; }
  public string Username { get; set; } = null!;
  public Cart? Cart { get; set; }

  [JsonIgnore]
  public byte[] Password { get; set; } = Array.Empty<byte>();

  [JsonIgnore]
  public byte[] Salt { get; set; } = Array.Empty<byte>();

  [JsonIgnore]
  [DefaultValue("user")]
  public string Role { get; set; } = "";
}