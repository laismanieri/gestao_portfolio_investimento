using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.DTO
{
    public class InvestmentDetailDTO
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public int FinancialProductId { get; set; }
        public string FinancialProductName { get; set; }

        public FinancialProductType FinancialProductType { get; set; }

        public int Quantity { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public DateTime? SaleDate { get; set; }

        public DateTime MaturityDate { get; set; }

        public decimal ReturnRate { get; set; }

        public decimal Earning { get; set; }

        public List<TransactionDTO> Transactions { get; set; } = new();
    }
}