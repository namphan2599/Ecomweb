
namespace Ecomweb.Data;

public class CartItem
{
  public int Id { get; set; }
  public int CartId { get; }
  public Product Product { get; set; }
  public int Quantity { get; set; }
  public Cart Cart { get; set; }
}