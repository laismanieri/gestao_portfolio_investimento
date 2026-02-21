using InvestmentPortfolioManagement.DTO;
using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Interfaces
{
    public interface IInvestmentService
    {
        public void AddInvestment(InvestmentDTO investmentDto);

        public Investment GetInvestmentById(int id);

        public List<Investment> GetInvestmentsByCustomerId(int customerId);

        public CustomerStatementDTO GetCustomerStatementById(int customerId);

        public List<Investment> GetAllInvestments(int skip, int take);

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsByFinancialProduct();        

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsNearMaturity(int days);

        // public void UpdateInvestmentPurchase(int id, PurchaseInvestmentDTO purchaseInvestmentDto);

        public void UpdateInvestmentSale(int id, SaleInvestmentDTO saleInvestmentDto);

        public void DeleteInvestment(int id);
    }
}
