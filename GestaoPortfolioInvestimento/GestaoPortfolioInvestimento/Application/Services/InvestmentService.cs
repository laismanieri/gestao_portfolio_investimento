using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Domain.Enums;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly DataContext _context;

        public InvestmentService(DataContext context)
        {
            _context = context;
        }

        public void AddInvestment(InvestmentDTO investmentDto)
        {
            if (investmentDto == null)
                throw new ArgumentNullException(nameof(investmentDto), "Investment cannot be null");

            var financialProduct = _context.FinancialProducts.FirstOrDefault(p => p.Id == investmentDto.FinancialProductId);

            if (financialProduct == null)
                throw new KeyNotFoundException($"Financial product with ID {investmentDto.FinancialProductId} not found.");

            // Create new investment
            var newInvestment = new InvestmentEntity
            {
                CustomerId = investmentDto.CustomerId,
                FinancialProductId = investmentDto.FinancialProductId,
                Quantity = investmentDto.Quantity,
                Term = investmentDto.Term,
                SubscriptionDate = investmentDto.SubscriptionDate,
                MaturityDate = investmentDto.SubscriptionDate.AddDays(investmentDto.Term % 365),
                TotalValue = 0,
                Yield = 0
            };

            _context.Investments.Add(newInvestment);
            _context.SaveChanges();

            // Update calculated values
            newInvestment.TotalValue = newInvestment.FinancialProduct.UnitValue * newInvestment.Quantity;
            newInvestment.MaturityDate = newInvestment.SubscriptionDate.AddDays(investmentDto.Term % 365);

            _context.SaveChanges();

            // Add transaction record
            var transaction = new TransactionEntity
            {
                Id = newInvestment.Id,
                Quantity = newInvestment.Quantity,
                Date = DateTime.Now,
                UnitValue = financialProduct.UnitValue,
                TotalValue = newInvestment.TotalValue,
                TransactionType = TransactionType.BUY
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void UpdateInvestmentSale(int id, SaleInvestmentDTO sellDto)
        {
            var investment = _context.Investments
                .Include(i => i.FinancialProduct)
                .FirstOrDefault(i => i.Id == id);

            if (investment == null)
                throw new KeyNotFoundException($"Investment with ID {id} not found.");

            if (sellDto.Quantity > investment.Quantity)
                throw new InvalidOperationException("Sale quantity exceeds available quantity.");

            decimal unitValue = investment.FinancialProduct.UnitValue;
            investment.Quantity -= sellDto.Quantity;
            investment.TotalValue -= unitValue * sellDto.Quantity;

            if (investment.Quantity == 0)
                investment.SaleDate = DateTime.Now;

            var transaction = new TransactionEntity
            {
                Id = investment.Id,
                Date = DateTime.Now,
                Quantity = sellDto.Quantity,
                UnitValue = unitValue,
                TotalValue = unitValue * sellDto.Quantity,
                TransactionType = TransactionType.SELL
            };

            _context.Transactions.Add(transaction);
            _context.Entry(investment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<InvestmentEntity> GetInvestmentsByCustomerId(int customerId)
        {
            var investments = _context.Investments
                .Where(i => i.CustomerId == customerId)
                .ToList();

            if (!investments.Any())
                throw new KeyNotFoundException($"No investments found for customer ID {customerId}.");

            return investments;
        }

        public CustomerStatementDTO GetCustomerStatementById(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Investments)
                    .ThenInclude(i => i.FinancialProduct)
                .Include(c => c.Investments)
                    .ThenInclude(i => i.Transactions)
                .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            var statement = new CustomerStatementDTO
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                Investments = customer.Investments.Select(i => new InvestmentDetailDTO
                {
                    FinancialProductName = i.FinancialProduct.Name,
                    FinancialProductType = i.FinancialProduct.Type,
                    Quantity = i.Quantity,
                    TotalValue = i.TotalValue,
                    SubscriptionDate = i.SubscriptionDate,
                    SaleDate = i.SaleDate,
                    MaturityDate = i.MaturityDate,
                    Yield = i.Yield,
                    Transactions = i.Transactions.Select(t => new TransactionDTO
                    {
                        Date = t.Date,
                        TransactionType = t.TransactionType,
                        Quantity = t.Quantity,
                        TotalValue = t.TotalValue
                    }).ToList()
                }).ToList()
            };

            return statement;
        }

        public InvestmentEntity GetInvestmentById(int id)
        {
            var investment = _context.Investments.FirstOrDefault(i => i.Id == id);

            if (investment == null)
                throw new KeyNotFoundException($"Investment with ID {id} not found.");

            return investment;
        }

        public List<InvestmentEntity> GetAllInvestments(int skip, int take)
        {
            return _context.Investments.Skip(skip).Take(take).ToList();
        }

        public void DeleteInvestment(int id)
        {
            var investment = GetInvestmentById(id);
            _context.Investments.Remove(investment);
            _context.SaveChanges();
        }

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsByFinancialProduct()
        {
            var investments = _context.Investments
                .Include(i => i.FinancialProduct)
                .Select(i => new InvestmentDetailDTO
                {
                    Id = i.Id,
                    FinancialProductId = i.FinancialProductId,
                    FinancialProductName = i.FinancialProduct.Name,
                    FinancialProductType = i.FinancialProduct.Type,
                    Quantity = i.Quantity,
                    TotalValue = i.TotalValue,
                    SubscriptionDate = i.SubscriptionDate,
                    SaleDate = i.SaleDate,
                    MaturityDate = i.MaturityDate,
                    ReturnRate = i.FinancialProduct.ReturnRate,
                    Yield = i.Yield
                })
                .ToList();

            return investments
                .GroupBy(i => i.FinancialProductId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public Dictionary<int, List<InvestmentDetailDTO>> ListInvestmentsNearMaturity(int days)
        {
            DateTime limitDate = DateTime.Now.AddDays(days);

            var investments = _context.Investments
                .Include(i => i.FinancialProduct)
                .Include(i => i.Customer)
                .Where(i => i.MaturityDate <= limitDate)
                .Select(i => new InvestmentDetailDTO
                {
                    Id = i.Id,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer.Name,
                    CustomerEmail = i.Customer.Email,
                    FinancialProductId = i.FinancialProductId,
                    FinancialProductName = i.FinancialProduct.Name,
                    FinancialProductType = i.FinancialProduct.Type,
                    Quantity = i.Quantity,
                    TotalValue = i.TotalValue,
                    SubscriptionDate = i.SubscriptionDate,
                    SaleDate = i.SaleDate,
                    MaturityDate = i.MaturityDate,
                    ReturnRate = i.FinancialProduct.ReturnRate,
                    Yield = i.Yield
                })
                .ToList();

            return investments
                .GroupBy(i => i.CustomerId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}