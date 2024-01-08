namespace Ecomweb.Data
{
  public class Product
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price {get; set;}
    public int Quantity { get; set; }
    publict List<CartItem> Carts { get; set; } = new List<CartItem>();
  }

}



