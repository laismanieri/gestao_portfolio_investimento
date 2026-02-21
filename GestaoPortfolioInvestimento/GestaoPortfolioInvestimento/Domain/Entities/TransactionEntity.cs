using InvestmentPortfolioManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class TransactionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InvestmentId { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public decimal UnitValue { get; set; }

        public decimal TotalValue { get; set; }

        public TransactionType TransactionType { get; set; }

        [ForeignKey("InvestmentId")]
        public InvestmentEntity Investment { get; set; } = null!;
    }
}