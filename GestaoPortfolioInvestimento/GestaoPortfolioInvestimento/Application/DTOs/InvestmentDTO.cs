using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class InvestmentDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FinancialProductId { get; set; }

        public String FinancialProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public DateTime SubscriptionDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Yield { get; set; }

        [Required(ErrorMessage = "Term in days is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Term in days must be greater than zero.")]
        public int Term { get; set; }
    }
}