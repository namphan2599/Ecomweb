using System.Text.Json.Serialization;

namespace Ecomweb.Data;
public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [JsonInclude]
    public ICollection<CartItem> CartItems = new List<CartItem>();
}