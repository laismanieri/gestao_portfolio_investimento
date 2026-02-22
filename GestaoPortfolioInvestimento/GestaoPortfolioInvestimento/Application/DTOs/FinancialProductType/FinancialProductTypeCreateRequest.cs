using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProductType
{
    public class FinancialProductTypeCreateRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
