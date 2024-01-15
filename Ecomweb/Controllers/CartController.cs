using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecomweb.Data;
using System.Runtime.CompilerServices;
using Ecomweb.Data.Dto;

namespace ecomweb
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EcomContext _context;

        public CartController(EcomContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {

            /*
                Query cart item join with product
                must have some way to optimize this
            */
            // var query = from cartT in _context.Set<CartItem>()
            //             join product in _context.Set<Product>()
            //                 on cartT.ProductId equals product.Id
            //             where cartT.CartId == id
            //             select new { cartT, product.Name, product.Price };

            // Console.WriteLine(query.ToList());

            // foreach (var item in query.ToList())
            // {
            //     Console.WriteLine(item.Name);
            // }

            var cart = await _context.Carts.Include(cart => cart.CartItems).SingleAsync(cart => cart.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            Console.WriteLine("no print {0}", cart.CartItems.Count);

            foreach (var item in cart.CartItems)
            {
                Console.WriteLine(item.ProductId);
            }

            return cart;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(string productId, Cart cart)
        {
            var product = _context.Products.FindAsync();
            Console.WriteLine(productId);
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        [HttpPost("/AddToCart")]
        public async void AddToCart(AddToCartDto addToCartDto)
        {

            var cart = await _context.Carts.FindAsync(addToCartDto.CartId) ?? new Cart
            {
                UserId = addToCartDto.UserId
            };

            var cartItem = await _context.CartItems.SingleAsync(cartItem => cartItem.ProductId == addToCartDto.ProductId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = addToCartDto.ProductId,

                    Quantity = addToCartDto.Quantity
                };

                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += addToCartDto.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
