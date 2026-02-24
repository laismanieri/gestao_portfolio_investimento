using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Domain.Entities
{
    [Index(nameof(Guid), IsUnique = true)]
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<CustomerSubscriptionEntity> Investments { get; set; } = new List<CustomerSubscriptionEntity>();
    }
}
