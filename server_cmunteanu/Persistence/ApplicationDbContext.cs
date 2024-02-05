using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrdersProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(256);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.PlacedBy).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PlacedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<OrderProduct>(
                l => l.HasOne(e => e.Product)
                    .WithMany(e => e.OrdersProducts)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(e => e.Order)
                    .WithMany(e => e.OrdersProducts)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                );
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey("OrderId", "ProductId");
            entity.ToTable("OrdersProducts");

            entity.Property(e => e.Quantity).IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC07D434BE40");

            entity.Property(e => e.Photo).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Stock).HasDefaultValue(1);
            entity.Property(e => e.Price).HasDefaultValue(1);
        });
    }
}
