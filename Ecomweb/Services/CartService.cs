using Ecomweb.Data;
using Ecomweb.Data.Dto;
using Ecomweb.Interfaces;
using Microsoft.AspNetCore.Mvc;
public class CartService : ICartService
{
  private readonly EcomContext _context;

  public CartService(
    EcomContext context
  )
  {
    _context = context;
  }

  public Task<ActionResult<CartItem>> AddToCart(AddToCartDto dto)
  {
    throw new NotImplementedException();
  }

  public Task<Cart> GetUserCart()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult> RemoveFromCart(int cartItemId)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult> UpdateCartItemQuantity(int cartItemId, int newQuantity)
  {
    throw new NotImplementedException();
  }
}