namespace Ecomweb.Data;
public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ICollection<CartItem> CartItems = new List<CartItem>();
}