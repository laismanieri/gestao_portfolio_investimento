using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Infrastructure.Services;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;
        private readonly StatementService _statementService;

        public InvestmentController(IInvestmentService investmentService, StatementService statementService)
        {
            _investmentService = investmentService;
            _statementService = statementService;
        }

        [HttpGet]
        public IActionResult GetAllInvestments([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var investments = _investmentService.GetAllInvestments(skip, take);
            return Ok(investments);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvestmentById(int id)
        {
            try
            {
                var investment = _investmentService.GetInvestmentById(id);
                return Ok(investment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetInvestmentsByCustomerId(int customerId)
        {
            try
            {
                var investments = _investmentService.GetInvestmentsByCustomerId(customerId);
                return Ok(investments);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("statement/{customerId}")]
        public IActionResult GetCustomerStatement(int customerId)
        {
            try
            {
                var statement = _investmentService.GetCustomerStatementById(customerId);
                return Ok(statement);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("statement-pdf/{customerId}")]
        public IActionResult GetCustomerStatementPdf(int customerId)
        {
            try
            {
                var statement = _investmentService.GetCustomerStatementById(customerId);
                var pdfBytes = _statementService.GenerateCustomerStatementPdf(statement);
                return File(pdfBytes, "application/pdf", "statement.pdf");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("by-product")]
        public IActionResult ListInvestmentsByProduct()
        {
            try
            {
                var investmentsByProduct = _investmentService.ListInvestmentsByFinancialProduct();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                return Ok(JsonSerializer.Serialize(investmentsByProduct, options));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error listing investments by product: {ex.Message}");
            }
        }

        [HttpGet("by-product-pdf")]
        public IActionResult GenerateInvestmentsByProductPdf()
        {
            try
            {
                var investmentsByProduct = _investmentService.ListInvestmentsByFinancialProduct();
                var pdfBytes = _statementService.GenerateFinancialProductStatementPdf(investmentsByProduct);
                return File(pdfBytes, "application/pdf", "investments_by_product.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating PDF: {ex.Message}");
            }
        }

        [HttpPost("buy")]
        public IActionResult AddInvestment([FromBody] InvestmentDTO investmentDto)
        {
            try
            {
                _investmentService.AddInvestment(investmentDto);
                return CreatedAtAction(nameof(GetInvestmentById), new { id = investmentDto.Id }, investmentDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("sell/{id}")]
        public IActionResult UpdateInvestmentSale(int id, [FromBody] SaleInvestmentDTO sellDto)
        {
            if (sellDto == null || sellDto.Quantity <= 0)
                return BadRequest("Invalid sale data.");

            try
            {
                _investmentService.UpdateInvestmentSale(id, sellDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvestment(int id)
        {
            try
            {
                _investmentService.DeleteInvestment(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}