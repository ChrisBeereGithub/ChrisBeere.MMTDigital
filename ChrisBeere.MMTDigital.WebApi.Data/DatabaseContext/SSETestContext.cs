using ChrisBeere.MMTDigital.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChrisBeere.MMTDigital.WebApi.Data.DatabaseContext
{
    /// <summary>
    /// Defines the database context that will be used to control the data access to [dbo.SSE_Test].
    /// </summary>
    public class SSETestContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Constructor to enable passing options to base DBContext.
        /// </summary>
        /// <param name="options">The options to be used by the database context.</param>
        public SSETestContext(DbContextOptions<SSETestContext> options) : base(options)
        {

        }

        /// <summary>
        /// Fluent mapping of physical to logical entities for Entity Framework.
        /// </summary>
        /// <param name="modelBuilder">Api surface for configuring Entity Framework.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("PRODUCTS")
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Order>()
                .ToTable("ORDERS")
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .ToTable("ORDERITEMS")
                .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .IsRequired();
        }
    }
}
