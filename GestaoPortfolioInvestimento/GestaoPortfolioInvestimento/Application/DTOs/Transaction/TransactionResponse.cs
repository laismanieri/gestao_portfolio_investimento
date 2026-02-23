namespace InvestmentPortfolioManagement.Application.DTOs.Transaction
{
    public class TransactionResponse
    {
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public string TransactionType { get; set; } = string.Empty;
    }
}
