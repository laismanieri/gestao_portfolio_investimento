using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IFinancialProductService
    {
        FinancialProductEntity AddFinancialProduct(FinancialProductUpdateRequest financialProductDto);

        FinancialProductEntity GetFinancialProductById(int id);

        List<FinancialProductEntity> GetAllFinancialProducts(int skip, int take);

        void UpdateFinancialProduct(int id, FinancialProductUpdateRequest financialProductDto);

        void RemoveFinancialProduct(int id);
    }
}