using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class CustomerStatementDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<CustomerSubscriptionDetailResponse> Investments { get; set; } = new List<CustomerSubscriptionDetailResponse>();
    }
}