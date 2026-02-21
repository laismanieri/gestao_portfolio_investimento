using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.DTO
{
    public class TransactionDTO
    {
        public DateTime Date { get; set; }

        public TransactionType TransactionType { get; set; }

        public int Quantity { get; set; }

        public decimal TotalValue { get; set; }
    }
}
