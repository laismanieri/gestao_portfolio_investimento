namespace InvestmentPortfolioManagement.Models
{
    public class FinancialProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation property
        public List<FinancialProduct> FinancialProducts { get; set; } = new();
    }
}
