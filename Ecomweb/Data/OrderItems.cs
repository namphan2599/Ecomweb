using Ecomweb.Data;

namespace ecomweb.Data;

public class OrderItems
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; }

    public Product Product { get; set; } = null!;

    public double PriceAtTime { get; set; }
}