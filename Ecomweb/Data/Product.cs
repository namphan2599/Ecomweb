
using ecomweb.Data;

namespace Ecomweb.Data;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }

    public string CategoryID { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; }

    public Categories Category { get; set; } = null!;

}



