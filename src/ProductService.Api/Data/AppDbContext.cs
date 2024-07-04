using Microsoft.EntityFrameworkCore;
using ProductService.Api.Data.Interceptors;
using ProductService.Api.Domain.Entities;

namespace ProductService.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
       
    }

    public DbSet<Order> Orders { get; init; }
    public DbSet<Product> Products { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var interceptor = new CreatedAtInterceptor();
        optionsBuilder.AddInterceptors(interceptor);
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(s => s.Orders) 
            .WithMany(c => c.Products)
            .UsingEntity(j => j.ToTable("ProductOrder")); 
        base.OnModelCreating(modelBuilder);
    }
}
