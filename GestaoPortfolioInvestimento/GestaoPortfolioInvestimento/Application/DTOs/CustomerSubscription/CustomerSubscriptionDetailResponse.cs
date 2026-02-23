using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription
{
    public class CustomerSubscriptionDetailResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public CustomerSubscriptionResponse CustomerSubscription { get; set; } = new();

        public List<TransationSummaryResponse> Transactions { get; set; } = new();
    }

    public class TransationSummaryResponse 
    {
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public string TransactionType { get; set; } = string.Empty;
    }
}
