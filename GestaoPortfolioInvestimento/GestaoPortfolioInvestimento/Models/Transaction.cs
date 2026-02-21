
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Models
{
    public class Transaction
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

        // Relationship
        [ForeignKey("InvestmentId")]
        public Investment Investment { get; set; }
    }
}