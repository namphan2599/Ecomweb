using Bogus;
using Ecomweb.Data;

namespace ecomweb.Services
{
    public class DataSeeder
    {
        private readonly EcomContext _context;

        public DataSeeder(EcomContext context)
        {
            _context = context;
        }

        public void SeedProducts()
        {
            if (!_context.Products.Any())
            {
                var productFaker = new Faker<Product>()
                   .RuleFor(product => product.Id, f => f.IndexFaker + 1)
                   .RuleFor(product => product.Name, f => f.Commerce.Product())
                   .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                   .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
                   .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));

                _context.Products.AddRange(productFaker.Generate(100));
                _context.SaveChanges();
            }
        }
    }
}
