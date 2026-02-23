using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class FinancialProductStatementDTO
    {
        public List<CustomerSubscriptionDetailResponse> FinancialProducts { get; set; } = new List<CustomerSubscriptionDetailResponse>();
    }
}