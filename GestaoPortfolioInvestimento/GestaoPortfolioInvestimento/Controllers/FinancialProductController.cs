using Microsoft.AspNetCore.Mvc;
using InvestmentPortfolioManagement.Interfaces;
using InvestmentPortfolioManagement.DTO;


namespace InvestmentPortfolioManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialProductController : ControllerBase
    {
        private readonly IFinancialProductService _service;

        public FinancialProductController(IFinancialProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return Ok(_service.GetAllFinancialProducts(skip, take));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetFinancialProductById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] FinancialProductDTO dto)
        {
            var created = _service.AddFinancialProduct(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = created.ID },
                created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FinancialProductDTO dto)
        {
            _service.UpdateFinancialProduct(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.RemoveFinancialProduct(id);
            return NoContent();
        }
    }
}
