using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class FinancialProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int FinancialProductTypeId { get; set; }

        public FinancialProductTypeEntity Type { get; set; } = null!;

        public decimal UnitValue { get; set; }
        public decimal ReturnRate { get; set; }

        public List<InvestmentEntity> Investments { get; set; } = new List<InvestmentEntity>();
    }
}