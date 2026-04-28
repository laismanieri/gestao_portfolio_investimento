using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    [Index(nameof(Guid), IsUnique = true)]
    public class CustomerSubscription : BaseEntity
    {
        public int CustomerId { get; set; }
        public int FinancialProductId { get; set; }

        public int Quantity { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Yield { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;

        [ForeignKey("FinancialProductId")]
        public FinancialProduct FinancialProduct { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}