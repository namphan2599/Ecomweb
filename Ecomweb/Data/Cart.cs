using System.Text.Json.Serialization;

namespace Ecomweb.Data;
public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; }

    public Product Product { get; set; } = null!;
}