using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class InvestmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int FinancialProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime SubscriptionDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime MaturityDate { get; set; }

        public decimal TotalValue { get; set; }
        public decimal Yield { get; set; }

        public int Term { get; set; }

        [ForeignKey("CustomerId")]
        public CustomerEntity Customer { get; set; } = null!;

        [ForeignKey("FinancialProductId")]
        public FinancialProductEntity FinancialProduct { get; set; } = null!;

        public List<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
    }
}