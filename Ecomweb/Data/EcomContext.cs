using Microsoft.EntityFrameworkCore;

namespace Ecomweb.Data 
{
  public class EcomContext : DbContext
  {
    public EcomContext(DbContextOptions<EcomContext> options) : base(options) 
    {
      
    }

    public DbSet<Product> Products {get; set;} = null!;
  }
}

