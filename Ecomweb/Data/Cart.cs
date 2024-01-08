public class Cart
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}