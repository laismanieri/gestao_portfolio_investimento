using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    [Index(nameof(Guid), IsUnique = true)]
    public class FinancialProductEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int FinancialProductTypeId { get; set; }

        public FinancialProductTypeEntity Type { get; set; } = null!;

        public decimal UnitValue { get; set; }

        public int Quantity { get; set; }

        public DateTime MaturityDate { get; set; }

        public int Term { get; set; }

        public decimal ReturnRate { get; set; }

        public List<CustomerSubscription> Investments { get; set; } = new List<CustomerSubscription>();

        public bool IsActive { get; set; } = true;
    }
}