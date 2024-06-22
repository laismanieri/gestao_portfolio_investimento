using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentoController : ControllerBase
    {

        private readonly IInvestimento _investimento;

        public InvestimentoController(IInvestimento investimento)
        {
            _investimento = investimento;
        }


        [HttpGet]
        public IActionResult ObterTodosInvestimentos([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var investimentos = _investimento.ObterTodosInvestimentos(skip, take);
            return Ok(investimentos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterInvestimentoPorId(int id)
        {
            try
            {
                var investimento = _investimento.ObterInvestimentoPorId(id);
                return Ok(investimento);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarInvestimento([FromBody] Investimento investimento)
        {
            try
            {
                _investimento.AdicionarInvestimento(investimento);
                return CreatedAtAction(nameof(ObterInvestimentoPorId), new { id = investimento.ID }, investimento);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarInvestimento(int id, [FromBody] Investimento investimento)
        {
            if (id != investimento.ID)
            {
                return BadRequest("ID do investimento não encontrado");
            }

            try
            {
                _investimento.AtualizarInvestimento(investimento);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverInvestimento(int id)
        {
            try
            {
                _investimento.RemoverInvestimento(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}
