using Microsoft.AspNetCore.Mvc;
using InvestmentPortfolioManagement.Interfaces;

namespace InvestmentPortfolioManagement.Controllers
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
        public IActionResult GetAllTransactions([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var transactions = _transaction.GetAllTransactions(skip, take);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            try
            {
                var transaction = _transaction.GetTransactionById(id);
                return Ok(transaction);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}