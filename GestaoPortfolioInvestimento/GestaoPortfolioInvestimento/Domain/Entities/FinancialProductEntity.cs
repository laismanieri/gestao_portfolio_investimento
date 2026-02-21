namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class FinancialProductEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int FinancialProductTypeId { get; set; }

        public FinancialProductTypeEntity Type { get; set; } = null!;

        public decimal UnitValue { get; set; }
        public decimal ReturnRate { get; set; }

        public List<InvestmentEntity> Investments { get; set; } = new List<InvestmentEntity>();
    }
}