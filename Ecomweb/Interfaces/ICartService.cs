using Ecomweb.Data;
using Ecomweb.Data.Dto;
using Microsoft.AspNetCore.Mvc;
namespace Ecomweb.Interfaces;

public interface ICartService
{
  Task<ActionResult<CartItem>> AddToCart(AddToCartDto dto);
  Task<ActionResult> RemoveFromCart(int cartItemId);
  Task<Cart> GetUserCart();
  Task<ActionResult> UpdateCartItemQuantity(int cartItemId, int newQuantity);
}