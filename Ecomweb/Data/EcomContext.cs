using ecomweb.Data;
using Microsoft.EntityFrameworkCore;
namespace Ecomweb.Data;

public class EcomContext : DbContext
{
  public EcomContext(DbContextOptions<EcomContext> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

        //var jsonString = File.ReadAllBytes("MOCK_DATA.json");
        //var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonString);

        //Console.WriteLine(products);

        //modelBuilder.Entity<Product>().HasData(products);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Carts)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Orders)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Cart>()
            .HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasMany(e => e.OrderItems)
            .WithOne()
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        modelBuilder.Entity<OrderItems>()
            .HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
    
        base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;

  public DbSet<Cart> Carts { get; set; }

  public DbSet<User> Users { get; set; }

}

