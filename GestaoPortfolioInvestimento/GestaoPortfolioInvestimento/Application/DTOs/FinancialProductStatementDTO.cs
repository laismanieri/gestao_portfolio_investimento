namespace InvestmentPortfolioManagement.Application.DTOs
{
    public class FinancialProductStatementDTO
    {
        public List<InvestmentDetailDTO> FinancialProducts { get; set; } = new List<InvestmentDetailDTO>();
    }
}