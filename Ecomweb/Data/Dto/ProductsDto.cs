using Ecomweb.Data;

namespace ecomweb.Data.Dto
{
    public class ProductsDto
    {
        public List<Product> Products { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
