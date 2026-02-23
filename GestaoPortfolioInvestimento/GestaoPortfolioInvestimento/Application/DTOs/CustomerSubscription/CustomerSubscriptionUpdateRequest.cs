using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription
{
    public class CustomerSubscriptionUpdateRequest
    {

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int? Quantity { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Total value cannot be negative.")]
        public Double? TotalValue { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Yield cannot be negative.")]
        public Double? Yield { get; set; }

        public DateTime? SaleDate { get; set; }

    }
}
