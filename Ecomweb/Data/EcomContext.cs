using Microsoft.EntityFrameworkCore;
namespace Ecomweb.Data;

public class EcomContext : DbContext
{
  public EcomContext(DbContextOptions<EcomContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;

  public DbSet<Cart> Carts { get; set; }

  public DbSet<User> Users { get; set; }
}

