using ecomweb.Data;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Ecomweb.Data;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    [JsonIgnore]
    public byte[] Salt { get; set; } = Array.Empty<byte>();

    public bool IsActive { get; set; } = false;

    public ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    // [JsonIgnore]
    // [DefaultValue("user")]
    // public string Role { get; set; } = "";
}