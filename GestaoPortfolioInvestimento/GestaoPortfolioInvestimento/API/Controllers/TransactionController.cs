using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.DTOs.Transaction;
using InvestmentPortfolioManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transaction;

        public TransactionController(ITransactionService transaction)
        {
            _transaction = transaction;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransactions([FromQuery] PaginationQuery query)
        {
            if (query.Skip < 0 || query.Take <= 0)
                return BadRequest("Invalid pagination parameters.");
            var transactions = await _transaction.GetAllTransactionsAsync(query);
            return Ok(transactions);
        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> GetTransactionByGuid(Guid guid)
        {
            var transaction = await _transaction.GetTransactionByGuidAsync(guid);
            return Ok(transaction);
        }
    }
}