using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecomweb.Data;
using Microsoft.AspNetCore.Authorization;
using ecomweb.Data.Dto;




namespace ecomweb
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcomContext _context;

        const string baseUrl = "https://localhost:7077";


        public ProductController(EcomContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ProductsDto>> GetProducts(int page = 1, int pageSize = 10, string sort = "price-asc")
        {
            IQueryable<Product> query = _context.Products.AsNoTracking();

            query = sort switch
            {
                "price-asc" => query.OrderBy(p => p.Price),
                "price-desc" => query.OrderByDescending(p => p.Price),
                "name-asc" => query.OrderBy(p => p.Name),
                "name-desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Price),
            };

            

            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new ProductsDto() { Products = products, TotalCount = query.Count() };
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Product>> PostProduct([FromForm] Product product, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var extension = Path.GetExtension(image.FileName);
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    return BadRequest("Invalid image format.");
                }

                var fileName = $"product_{Guid.NewGuid()}{extension}";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // Ensure folder exists

                using var stream = new FileStream(imagePath, FileMode.Create);
                await image.CopyToAsync(stream);

                product.ImageUrl = $"{baseUrl}/images/products/{fileName}";
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
