using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;

namespace InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription
{
    public class CustomerSubscriptionResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public CustomerSummaryResponse Customer { get; set; } = new();
        public FinancialProductSummaryResponse FinancialProduct { get; set; } = new();

        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Yield { get; set; }

        public DateTime? SaleDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CustomerSummaryResponse
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class FinancialProductSummaryResponse
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal ReturnRate { get; set; }

        public FinancialProductTypeResponse Type { get; set; } = new();
    }
}
