using InvestmentPortfolioManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolioManagement.Infrastructure.Persistence.Configurations
{
    public class FinancialProductTypeConfiguration : IEntityTypeConfiguration<FinancialProductType>
    {
        public void Configure(EntityTypeBuilder<FinancialProductType> builder)
        {
            builder.ToTable("FinancialProductTypes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(t => t.Guid)
                   .IsUnique();
        }
    }
}
