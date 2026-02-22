namespace InvestmentPortfolioManagement.Domain.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<InvestmentEntity> Investments { get; set; } = new List<InvestmentEntity>();
    }
}
