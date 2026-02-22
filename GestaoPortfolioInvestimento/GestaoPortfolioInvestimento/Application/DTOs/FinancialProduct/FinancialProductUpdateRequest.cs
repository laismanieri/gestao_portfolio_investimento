using InvestmentPortfolioManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProduct
{
    public class FinancialProductUpdateRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public FinancialProductTypeEntity Type { get; set; }

        public int FinancialProductTypeId { get; set; }

        public decimal UnitValue { get; set; }

        public decimal ReturnRate { get; set; }
    }
}