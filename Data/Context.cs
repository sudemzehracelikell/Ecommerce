using Ecommerce.models;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using WebApp1.models;

namespace ECommerce.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) :base(options){}

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }

        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);
            
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
            
        modelBuilder.Entity<ProductOrder>()
            .HasKey(k => new { k.ProductId, k.OrderId });
            
        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Product)
            .WithMany(p=>p.ProductOrders)
            .HasForeignKey(po => po.ProductId);
                
        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Order)
            .WithMany(o => o.ProductOrders)
            .HasForeignKey(po => po.OrderId);
            
            
    }
}