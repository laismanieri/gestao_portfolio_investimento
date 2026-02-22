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
        public FinancialProductTypeEntity Type { get; set; }

        public int FinancialProductTypeId { get; set; }

        public decimal UnitValue { get; set; }

        public decimal ReturnRate { get; set; }
    }
}