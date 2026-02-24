using InvestmentPortfolioManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public int InvestmentId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitValue { get; set; }

        public decimal TotalValue { get; set; }

        public TransactionType TransactionType { get; set; }

        [ForeignKey("InvestmentId")]
        public CustomerSubscription Investment { get; set; } = null!;
    }
}