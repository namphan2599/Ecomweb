namespace Ecomweb.Data.Dto;

public class AddToCartDto
{

  public long UserId { get; set; }
  public long? CartId { get; set; }

  public long ProductId { get; set; }

  public int Quantity { get; set; }
}