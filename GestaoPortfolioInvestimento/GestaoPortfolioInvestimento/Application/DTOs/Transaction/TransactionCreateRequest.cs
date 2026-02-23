using InvestmentPortfolioManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.Transaction
{
    public class TransactionCreateRequest
    {
        [Required(ErrorMessage = "Transaction Type is required.")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public decimal UnitValue { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Total value cannot be negative.")]
        public Double TotalValue { get; set; }
    }
}
