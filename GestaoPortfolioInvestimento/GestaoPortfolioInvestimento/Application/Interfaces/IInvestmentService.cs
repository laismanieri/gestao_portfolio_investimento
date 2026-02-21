using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IInvestmentService
    {
        public void AddInvestment(InvestmentDTO investmentDto);

        public InvestmentEntity GetInvestmentById(int id);

        public List<InvestmentEntity> GetInvestmentsByCustomerId(int customerId);

        public CustomerStatementDTO GetCustomerStatementById(int customerId);

        public List<InvestmentEntity> GetAllInvestments(int skip, int take);

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsByFinancialProduct();        

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsNearMaturity(int days);

        // public void UpdateInvestmentPurchase(int id, PurchaseInvestmentDTO purchaseInvestmentDto);

        public void UpdateInvestmentSale(int id, SaleInvestmentDTO saleInvestmentDto);

        public void DeleteInvestment(int id);
    }
}
