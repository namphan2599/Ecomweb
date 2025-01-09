namespace ecomweb.Data;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; }

    public string Status { get; set; }

    public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

}
