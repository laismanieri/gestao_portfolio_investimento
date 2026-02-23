using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription
{
    public class CustomerSubscriptionCreateRequest
    {
        [Required(ErrorMessage = "Customer Guid is required.")]
        public Guid CustomerGuid { get; set; }

        [Required(ErrorMessage = "Financial Product Guid is required.")]
        public Guid FinancialProductGuid { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public Double TotalValue { get; set; }
    }
}