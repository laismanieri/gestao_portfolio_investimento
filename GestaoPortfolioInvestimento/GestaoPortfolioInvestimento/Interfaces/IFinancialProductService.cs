using InvestmentPortfolioManagement.DTO;
using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Interfaces
{
    public interface IFinancialProductService
    {
        FinancialProduct AddFinancialProduct(FinancialProductDTO financialProductDto);

        FinancialProduct GetFinancialProductById(int id);

        List<FinancialProduct> GetAllFinancialProducts(int skip, int take);

        void UpdateFinancialProduct(int id, FinancialProductDTO financialProductDto);

        void RemoveFinancialProduct(int id);
    }
}