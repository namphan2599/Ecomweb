
namespace Ecomweb.Data;

public class CartItem
{
  public long CartId { get; }
  public long ProductId { get; set; }
  public int Quantity { get; set; }
  public Cart Cart { get; set; }
}