using InvestmentPortfolioManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<InvestmentEntity> Investments { get; set; }
        public DbSet<FinancialProductEntity> FinancialProducts { get; set; }
        public DbSet<FinancialProductTypeEntity> FinancialProductTypes { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer -> Investments (1:N)
            modelBuilder.Entity<CustomerEntity>()
                .HasMany(c => c.Investments)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // FinancialProduct -> Investments (1:N)
            modelBuilder.Entity<FinancialProductEntity>()
                .HasMany(p => p.Investments)
                .WithOne(i => i.FinancialProduct)
                .HasForeignKey(i => i.FinancialProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // FinancialProductType -> FinancialProducts (1:N)
            modelBuilder.Entity<FinancialProductTypeEntity>()
                .HasMany(t => t.FinancialProducts)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.FinancialProductTypeId);

            // Investment -> Transactions (1:N)
            modelBuilder.Entity<InvestmentEntity>()
                .HasMany(i => i.Transactions)
                .WithOne(t => t.Investment)
                .HasForeignKey(t => t.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure decimals
            modelBuilder.Entity<InvestmentEntity>()
                .Property(i => i.TotalValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<InvestmentEntity>()
                .Property(i => i.Yield)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FinancialProductEntity>()
                .Property(p => p.UnitValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FinancialProductEntity>()
                .Property(p => p.ReturnRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TransactionEntity>()
                .Property(t => t.UnitValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TransactionEntity>()
                .Property(t => t.TotalValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(DataContext).Assembly);
        }
    }
}