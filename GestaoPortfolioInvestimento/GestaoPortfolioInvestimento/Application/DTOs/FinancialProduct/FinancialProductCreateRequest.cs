using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class FinancialProductCreateRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public Guid FinancialProductTypeGuid { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal UnitValue { get; set; }

        [Range(0, 100)]
        public decimal ReturnRate { get; set; }
    }
}