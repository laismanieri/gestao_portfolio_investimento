using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription
{
    public class CustomerSubscriptionUpdateRequest
    {

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int? Quantity { get; set; }

        public decimal? TotalValue { get; set; }

        public decimal? Yield { get; set; }

        public DateTime? SaleDate { get; set; }

    }
}
