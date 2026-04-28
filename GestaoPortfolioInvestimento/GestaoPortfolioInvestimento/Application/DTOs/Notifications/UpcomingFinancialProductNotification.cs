namespace InvestmentPortfolioManagement.Application.DTOs.Notifications
{
    public class UpcomingFinancialProductNotification
    {
        public Guid ProductGuid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime MaturityDate { get; set; }
        public decimal ReturnRate { get; set; }

    }
}
