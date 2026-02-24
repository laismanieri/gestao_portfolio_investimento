using InvestmentPortfolioManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerSubscription> CustomerSubscriptions { get; set; }
        public DbSet<FinancialProduct> FinancialProducts { get; set; }
        public DbSet<FinancialProductType> FinancialProductTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer -> Investments (1:N)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Investments)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // FinancialProduct -> Investments (1:N)
            modelBuilder.Entity<FinancialProduct>()
                .HasMany(p => p.Investments)
                .WithOne(i => i.FinancialProduct)
                .HasForeignKey(i => i.FinancialProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // FinancialProductType -> FinancialProducts (1:N)
            modelBuilder.Entity<FinancialProductType>()
                .HasMany(t => t.FinancialProducts)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.FinancialProductTypeId);

            // Investment -> Transactions (1:N)
            modelBuilder.Entity<CustomerSubscription>()
                .HasMany(i => i.Transactions)
                .WithOne(t => t.Investment)
                .HasForeignKey(t => t.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure decimals
            modelBuilder.Entity<CustomerSubscription>()
                .Property(i => i.TotalValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CustomerSubscription>()
                .Property(i => i.Yield)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FinancialProduct>()
                .Property(p => p.UnitValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FinancialProduct>()
                .Property(p => p.ReturnRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.UnitValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TotalValue)
                .HasColumnType("decimal(18,2)");
            // Configure indixes
            modelBuilder.Entity<Customer>()
                .HasIndex(t => t.Guid)
                .IsUnique();

            modelBuilder.Entity<FinancialProduct>()
                .HasIndex(t => t.Guid)
                .IsUnique();

            modelBuilder.Entity<FinancialProductType>()
                .HasIndex(t => t.Guid)
                .IsUnique();

            modelBuilder.Entity<CustomerSubscription>()
                .HasIndex(t => t.Guid)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.Guid)
                .IsUnique();

            modelBuilder.Entity<FinancialProduct>()
                .HasQueryFilter(p => p.IsActive);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(DataContext).Assembly);
        }
    }
}