using Bogus;
using ecomweb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

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

        // Store monetary values as integer cents in the DB while keeping decimal on the entity.
        var decimalToLongConverter = new ValueConverter<decimal, long>(
            v => (long)Math.Round(v * 100m, MidpointRounding.AwayFromZero), // decimal dollars -> long cents
            v => v / 100m                                                  // long cents -> decimal dollars
        );

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasConversion(decimalToLongConverter)
            .HasColumnType("INTEGER")
            .IsRequired();

        base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;

  public DbSet<Cart> Carts { get; set; }

  public DbSet<User> Users { get; set; }

}

