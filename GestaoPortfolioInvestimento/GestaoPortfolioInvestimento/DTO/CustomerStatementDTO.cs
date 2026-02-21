namespace InvestmentPortfolioManagement.DTO
{
    public class CustomerStatementDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<InvestmentDetailDTO> Investments { get; set; } = new List<InvestmentDetailDTO>();
    }
}