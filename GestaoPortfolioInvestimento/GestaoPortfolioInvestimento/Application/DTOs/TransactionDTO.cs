using InvestmentPortfolioManagement.Domain.Enums;

namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class TransactionDTO
    {
        public DateTime Date { get; set; }

        public TransactionType TransactionType { get; set; }

        public int Quantity { get; set; }

        public decimal TotalValue { get; set; }
    }
}
