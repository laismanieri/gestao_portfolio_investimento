namespace InvestmentPortfolioManagement.Application.DTOs.FinancialProductType
{
    public class FinancialProductTypeResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
