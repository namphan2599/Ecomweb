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
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using ecomweb.Data.Dto;

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
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart(int userId)
        {
            return await _context.Carts
                .Include(cart => cart.Product)
                .Where(cart => cart.UserId == userId)
                .ToListAsync();
        }


        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCart(int id, UpdateCartDto updateCartDto)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            if (updateCartDto.Quantity <= 0)
            {
                return BadRequest();
            }

            cart.Quantity = updateCartDto.Quantity;

            _context.Entry(cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(cart);
        }


        [HttpPost("AddToCart")]
        [Authorize]
        public async Task<ActionResult<Cart>> AddToCart(AddToCartDto addToCartDto)
        {

            if (addToCartDto.Quantity <= 0)
            {
                return BadRequest();
            }

            
            var product = await _context.Products.FindAsync(addToCartDto.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                    .Include(cart => cart.Product)
                    .FirstOrDefaultAsync(cart => cart.Id == addToCartDto.CartId);

            if(cart != null)
            {
                cart.Quantity += addToCartDto.Quantity;
            }
            else
            {
                cart = new Cart
                {
                    UserId = addToCartDto.UserId,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = addToCartDto.Quantity,
                };
                await _context.Carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
