
using System.Text.Json.Serialization;

namespace Ecomweb.Data;

public class CartItem
{
  public int Id { get; set; }
  public int CartId { get; }
  public Product Product { get; set; }
  public int Quantity { get; set; }

  // prevent the loop reference(for now)
  [JsonIgnore]
  public Cart Cart { get; set; }
}