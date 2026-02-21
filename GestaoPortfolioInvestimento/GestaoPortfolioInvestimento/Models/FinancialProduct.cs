using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Models
{
    public class FinancialProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "The financial product name is required.")]
        [StringLength(100, ErrorMessage = "The financial product name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public int FinancialProductTypeId { get; set; }

        [Required(ErrorMessage = "The financial product type is required.")]
        public FinancialProductType Type { get; set; }

        [Required(ErrorMessage = "The financial product value is required.")]
        public decimal UnitValue { get; set; }

        [Required(ErrorMessage = "The financial product return rate is required.")]
        public decimal ReturnRate { get; set; }

        // 1:N relationship
        public List<Investment> Investments { get; set; } = new List<Investment>();
    }
}