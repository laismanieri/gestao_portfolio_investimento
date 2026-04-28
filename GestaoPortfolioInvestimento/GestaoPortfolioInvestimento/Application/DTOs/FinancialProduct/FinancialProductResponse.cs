using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;

namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProduct
{
    public class FinancialProductResponse
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public FinancialProductTypeResponse Type { get; set; } = null!;
        public decimal UnitValue { get; set; }
        public int Quantity { get; set; }
        public DateTime MaturityDate { get; set; }
        public int Term { get; set; }
        public decimal ReturnRate { get; set; }
    }
}
