using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomweb.Data;
using Microsoft.AspNetCore.Authorization;
using ecomweb.Data.Dto;
using Ecomweb.Data;

namespace ecomweb
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EcomContext _context;

        public OrderController(EcomContext context)
        {
            _context = context;
        }

        // GET: api/Order/user/5
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersForUser(int userId)
        {
            var orders = await _context.Set<Order>()
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Set<Order>()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        // POST: api/Order/from-cart/5
        [HttpPost("from-cart/{userId}")]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrderFromCart(int userId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return BadRequest("Cart is empty.");
            }

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    UserId = userId,
                    AddedAt = DateTime.UtcNow,
                    Status = "Pending",
                    Quantity = cartItems.Sum(c => c.Quantity)
                };

                _context.Set<Order>().Add(order);
                await _context.SaveChangesAsync();

                foreach (var cart in cartItems)
                {
                    var product = cart.Product;
                    if (product == null)
                    {
                        await tx.RollbackAsync();
                        return BadRequest($"Product not found for cart item {cart.Id}.");
                    }

                    if (product.Quantity < cart.Quantity)
                    {
                        await tx.RollbackAsync();
                        return BadRequest($"Not enough stock for product {product.Id}.");
                    }

                    var orderItem = new OrderItems
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = cart.Quantity,
                        AddedAt = DateTime.UtcNow,
                        PriceAtTime = (double)product.Price
                    };

                    _context.Set<OrderItems>().Add(orderItem);

                    product.Quantity -= cart.Quantity;
                    _context.Entry(product).State = EntityState.Modified;

                    _context.Carts.Remove(cart);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                var created = await _context.Set<Order>()
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, created);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        // PUT: api/Order/5/status
        // Update order status - admin only
        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
        {
            var order = await _context.Set<Order>().FindAsync(id);
            if (order == null) return NotFound();

            var allowed = new[] { "Pending", "Processing", "Shipped", "Completed", "Cancelled" };
            if (string.IsNullOrWhiteSpace(dto.Status) || !allowed.Contains(dto.Status))
            {
                return BadRequest($"Invalid status. Allowed: {string.Join(", ", allowed)}");
            }

            // enforce simple transition rules
            var transitions = new Dictionary<string, string[]>
            {
                { "Pending", new[] { "Processing", "Cancelled" } },
                { "Processing", new[] { "Shipped", "Cancelled" } },
                { "Shipped", new[] { "Completed" } },
                { "Completed", Array.Empty<string>() },
                { "Cancelled", Array.Empty<string>() }
            };

            var current = order.Status ?? "Pending";
            if (!transitions.ContainsKey(current) || !transitions[current].Contains(dto.Status))
            {
                return BadRequest($"Invalid transition from '{current}' to '{dto.Status}'.");
            }

            order.Status = dto.Status;
            order.UpdatedAt = DateTime.UtcNow;
            // try to capture user identity
            var userName = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(userName))
            {
                // try claim
                userName = User?.Claims?.FirstOrDefault(c => c.Type == "name" || c.Type == "unique_name")?.Value;
            }
            order.UpdatedBy = userName;

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Order
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto dto)
        {
            if (dto == null || dto.Items == null || dto.Items.Count == 0)
                return BadRequest("No items provided.");

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return BadRequest("User not found.");

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    UserId = dto.UserId,
                    AddedAt = DateTime.UtcNow,
                    Status = dto.Status ?? "Pending",
                    Quantity = dto.Items.Sum(i => i.Quantity)
                };

                _context.Set<Order>().Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in dto.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        await tx.RollbackAsync();
                        return BadRequest($"Product {item.ProductId} not found.");
                    }

                    if (product.Quantity < item.Quantity)
                    {
                        await tx.RollbackAsync();
                        return BadRequest($"Not enough stock for product {product.Id}.");
                    }

                    var orderItem = new OrderItems
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        AddedAt = DateTime.UtcNow,
                        PriceAtTime = (double)product.Price
                    };

                    _context.Set<OrderItems>().Add(orderItem);

                    product.Quantity -= item.Quantity;
                    _context.Entry(product).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                var created = await _context.Set<Order>()
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, created);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
