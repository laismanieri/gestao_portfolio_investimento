using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponse>>> GetAllCustomers([FromQuery] PaginationQuery query)
        {
            if (query.Skip < 0 || query.Take <= 0)
                return BadRequest("Invalid pagination parameters.");
            var customers = await _customerService.GetAllCustomersAsync(query);
            return Ok(customers);
        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> GetCustomerByGuid(Guid guid)
        {
           var customer = await _customerService.GetCustomerByGuidAsync(guid);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            var createdCustomer = await _customerService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCustomerByGuid), new { guid = createdCustomer.Guid }, createdCustomer);
        }

        [HttpPut("{guid:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid guid, [FromBody] CustomerUpdateRequest request)
        {
            await _customerService.UpdateAsync(guid, request);
            return NoContent();
        }

        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid guid)
        {
            await _customerService.DeleteAsync(guid);
            return NoContent();
        }

    }
}