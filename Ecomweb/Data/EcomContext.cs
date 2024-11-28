using Microsoft.EntityFrameworkCore;
namespace Ecomweb.Data;

public class EcomContext : DbContext
{
  public EcomContext(DbContextOptions<EcomContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

        var jsonString = File.ReadAllBytes("MOCK_DATA.json");
        var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonString);

        Console.WriteLine(products);

        modelBuilder.Entity<Product>().HasData(products);

        modelBuilder.Entity<Cart>()
      .HasMany(e => e.CartItems)
      .WithOne(e => e.Cart)
      .HasForeignKey(e => e.CartId)
      .IsRequired();

    
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;

  public DbSet<Cart> Carts { get; set; }

  public DbSet<User> Users { get; set; }

  public DbSet<CartItem> CartItems { get; set; }
}

