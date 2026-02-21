using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class FinancialProductService : IFinancialProductService
    {
        private readonly DataContext _context;

        public FinancialProductService(DataContext context)
        {
            _context = context;
        }

        public FinancialProductEntity AddFinancialProduct(FinancialProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "The financial product cannot be null");
            }

            var productType = _context.FinancialProductTypes
                .FirstOrDefault(t => t.Id == productDto.FinancialProductTypeId);

            if (productType == null)
            {
                throw new KeyNotFoundException("Financial product type not found.");
            }

            var newProduct = new FinancialProductEntity
            {
                Name = productDto.Name,
                Type = productDto.Type,
                UnitValue = productDto.UnitValue,
                ReturnRate = productDto.ReturnRate
            };

            _context.FinancialProducts.Add(newProduct);
            _context.SaveChanges();

            return newProduct;

        }

        public void UpdateFinancialProduct(int id, FinancialProductDTO productDto)
        {
            var product = _context.FinancialProducts.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Financial product not found");
            }

            product.Name = productDto.Name;
            product.Type = productDto.Type;
            product.UnitValue = productDto.UnitValue;
            product.ReturnRate = productDto.ReturnRate;

            _context.SaveChanges();
        }

        public FinancialProductEntity GetFinancialProductById(int id)
        {
            var product = _context.FinancialProducts.FirstOrDefault(p => p.ID == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Financial product with ID {id} not found.");
            }

            return product;
        }

        public List<FinancialProductEntity> GetAllFinancialProducts(int skip, int take)
        {
            return _context.FinancialProducts.Skip(skip).Take(take).ToList();
        }

        public void RemoveFinancialProduct(int id)
        {
            var product = GetFinancialProductById(id);
            _context.FinancialProducts.Remove(product);
            _context.SaveChanges();
        }
    }
}