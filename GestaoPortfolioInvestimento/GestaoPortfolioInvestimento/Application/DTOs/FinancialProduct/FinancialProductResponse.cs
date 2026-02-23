using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProduct
{
    public class FinancialProductResponse
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public FinancialProductTypeResponse Type { get; set; } = null!;
        public decimal UnitValue { get; set; } 
        public decimal ReturnRate { get; set; }
    }
}
