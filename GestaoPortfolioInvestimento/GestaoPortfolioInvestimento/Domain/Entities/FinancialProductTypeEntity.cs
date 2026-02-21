namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class FinancialProductTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<FinancialProductEntity> FinancialProducts { get; set; } = new();
    }
}
