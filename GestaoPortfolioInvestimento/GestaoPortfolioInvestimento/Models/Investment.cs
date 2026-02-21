using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Models
{
    public class Investment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int FinancialProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public DateTime SubscriptionDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime MaturityDate { get; set; }

        public decimal TotalValue { get; set; }
        public decimal Earning { get; set; }

        [Required(ErrorMessage = "Term in days is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Term in days must be greater than zero.")]
        public int Term { get; set; }

        // Relationships
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("FinancialProductId")]
        public FinancialProduct FinancialProduct { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}