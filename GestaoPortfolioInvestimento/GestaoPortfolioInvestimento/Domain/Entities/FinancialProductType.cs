using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    [Index(nameof(Guid), IsUnique = true)]
    public class FinancialProductTypeEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public List<FinancialProduct> FinancialProducts { get; set; } = new();
    }
}
