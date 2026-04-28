using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerSubscriptionController : ControllerBase
    {
        private readonly ICustomerSubscriptionService _customerSubscriptionService;

        public CustomerSubscriptionController(ICustomerSubscriptionService customerSubscriptionService)
        {
            _customerSubscriptionService = customerSubscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerSubscription([FromQuery] PaginationQuery query)
        {
            if (query.Skip < 0 || query.Take <= 0)
                return BadRequest("Invalid pagination parameters.");

            var result = await _customerSubscriptionService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{guid:guid}/detail")]
        public async Task<IActionResult> GetDetailWithTransactions(Guid guid)
        {
            var result = await _customerSubscriptionService.GetDetailWithTransationsByGuidAsync(guid);
            return Ok(result);
        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> GetCustomerSubscriptionByGuid(Guid guid)
        {
            var result = await _customerSubscriptionService.GetCustomerSubscriptionByGuidAsync(guid);
            return Ok(result);
        }

        [HttpGet("by-customer/{customerGuid:guid}")]
        public async Task<IActionResult> GetByCustomerGuid(Guid customerGuid)
        {
            var result = await _customerSubscriptionService.GetCustomerSubscriptionByCustomerGuidAsync(customerGuid);
            return Ok(result);
        }

        [HttpGet("near-maturity")]
        public async Task<IActionResult> GetListNearMaturity([FromQuery] int days = 30)
        {
            if (days <= 0) return BadRequest("days must be > 0");

            var result = await _customerSubscriptionService.GetListNearMaturityAsync(days);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerSubscription([FromBody] CustomerSubscriptionCreateRequest request)
        {
            var created = await _customerSubscriptionService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetCustomerSubscriptionByGuid),
                new { guid = created.Guid },
                created);
        }

        [HttpPut("{guid:guid}")]
        public async Task<IActionResult> UpdateCustomerSubscription(Guid guid, [FromBody] CustomerSubscriptionUpdateRequest request)
        {
            await _customerSubscriptionService.UpdateAsync(guid, request);
            return NoContent();
        }

        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _customerSubscriptionService.DeleteAsync(guid);
            return NoContent();
        }



        //[HttpGet("statement/{customerId}")]
        //public IActionResult GetCustomerStatement(int customerId)
        //{
        //    try
        //    {
        //        var statement = _investmentService.GetCustomerStatementById(customerId);
        //        return Ok(statement);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpGet("statement-pdf/{customerId}")]
        //public IActionResult GetCustomerStatementPdf(int customerId)
        //{
        //    try
        //    {
        //        var statement = _investmentService.GetCustomerStatementById(customerId);
        //        var pdfBytes = _statementService.GenerateCustomerStatementPdf(statement);
        //        return File(pdfBytes, "application/pdf", "statement.pdf");
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}