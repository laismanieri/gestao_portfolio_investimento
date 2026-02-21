using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;

        public List<InvestmentEntity> Investments { get; set; } = new List<InvestmentEntity>();
    }
}
