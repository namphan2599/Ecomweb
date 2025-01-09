using Ecomweb.Data;

namespace ecomweb.Data;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int ParentId { get; set; }

    public Category? Parent { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}