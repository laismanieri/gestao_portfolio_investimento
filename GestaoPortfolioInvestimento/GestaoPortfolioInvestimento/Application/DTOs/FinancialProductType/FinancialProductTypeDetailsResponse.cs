using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProductType
{
    public class FinancialProductTypeDetailsResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<FinancialProductSummary> FinancialProductEntity { get; set; } = new();
    }

    public class FinancialProductSummary
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
