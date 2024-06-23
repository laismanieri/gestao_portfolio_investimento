using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;
using GestaoPortfolioInvestimento.DTO;


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

        [HttpGet("{clienteId}")]
        public IActionResult ObterInvestimentoPorClienteId(int clienteId)
        {
            try
            {
                var investimentos = _investimento.ObterInvestimentoPorClienteId(clienteId);
                return Ok(investimentos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("extrato/{clienteId}")]
        public IActionResult ObterExtratoPorClienteId(int clienteId)
        {
            try
            {
                var extrato = _investimento.ObterExtratoPorClienteId(clienteId);
                return Ok(extrato);
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

        [HttpPost("compra")]
        public IActionResult AdicionarInvestimento([FromBody] InvestimentoDTO investimentoDto)
        {
            try
            {
                _investimento.AdicionarInvestimento(investimentoDto);
                return CreatedAtAction(nameof(ObterInvestimentoPorId), new { id = investimentoDto.ID }, investimentoDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("compra/{id}")]
        //public IActionResult AtualizarInvestimentoCompra(int id, [FromBody] CompraInvestimentoDTO compraDto)
        //{
        //    if (compraDto == null || compraDto.Quantidade <= 0)
        //    {
        //        return BadRequest("Dados inválidos para compra.");
        //    }

        //    try
        //    {
        //        _investimento.AtualizarInvestimentoCompra(id, compraDto);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("venda/{id}")]
        public IActionResult AtualizarInvestimentoVenda(int id, [FromBody] VendaInvestimentoDTO vendaDto)
        {
            if (vendaDto == null || vendaDto.Quantidade <= 0)
            {
                return BadRequest("Dados inválidos para venda.");
            }

            try
            {
                _investimento.AtualizarInvestimentoVenda(id, vendaDto);
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
