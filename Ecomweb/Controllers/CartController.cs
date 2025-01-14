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
            return await _context.Carts
                .Include(cart => cart.Product)
                .ToListAsync();
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

            var cart = await _context.Carts
                        .Include(cart => cart.Product)
                        .SingleOrDefaultAsync(cart => cart.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            // Console.WriteLine("no print {0}", cart.CartItems.ToList().Tak);

            Console.WriteLine(cart.Product.Name);
            

            string jsonCart = JsonSerializer.Serialize(cart);


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

            // still a mess
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addToCartDto.UserId,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = addToCartDto.Quantity,
                };
                await _context.Carts.AddAsync(cart);
            } else
            {
                if(cart.Product.Id == addToCartDto.ProductId)
                {
                    // update quantity if product already in cart
                    cart.Quantity += addToCartDto.Quantity;
                } else
                {
                    return BadRequest();
                }
            }

            await _context.SaveChangesAsync();

            return Ok(cart);
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
