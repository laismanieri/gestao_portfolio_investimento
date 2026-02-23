using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    [Index(nameof(Guid), IsUnique = true)]
    public class CustomerSubscriptionEntity : BaseEntity
    {
        public int CustomerId { get; set; }
        public int FinancialProductId { get; set; }

        public int Quantity { get; set; }
        public DateTime? SaleDate { get; set; }
        public Double TotalValue { get; set; }
        public Double Yield { get; set; }

        [ForeignKey("CustomerId")]
        public CustomerEntity Customer { get; set; } = null!;

        [ForeignKey("FinancialProductId")]
        public FinancialProductEntity FinancialProduct { get; set; } = null!;

        public List<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
    }
}