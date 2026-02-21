using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IFinancialProductService
    {
        FinancialProductEntity AddFinancialProduct(FinancialProductDTO financialProductDto);

        FinancialProductEntity GetFinancialProductById(int id);

        List<FinancialProductEntity> GetAllFinancialProducts(int skip, int take);

        void UpdateFinancialProduct(int id, FinancialProductDTO financialProductDto);

        void RemoveFinancialProduct(int id);
    }
}