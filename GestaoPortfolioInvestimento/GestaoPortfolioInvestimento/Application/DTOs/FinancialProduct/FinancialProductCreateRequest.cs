using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class FinancialProductCreateRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required.")]
        public Guid FinancialProductTypeGuid { get; set; }

        [Range(1, 14600, ErrorMessage = "Term must be between 1 day and 40 years.")]
        public int Term { get; set; }

        [Required(ErrorMessage = "Unit Value is required.")]
        public decimal UnitValue { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Maturity date is required.")]
        public DateTime MaturityDate { get; set; }

        [Range(0, 100)]
        public decimal ReturnRate { get; set; }
    }
}