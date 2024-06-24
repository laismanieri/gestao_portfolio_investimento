using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;
using GestaoPortfolioInvestimento.DTO;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentoController : ControllerBase
    {

        private readonly IInvestimento _investimento;
        private readonly ExtratoService _extratoPdf;

        public InvestimentoController(IInvestimento investimento, ExtratoService extratoPdf)
        {
            _investimento = investimento;
            _extratoPdf = extratoPdf;
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

        [HttpGet("extrato/{clienteId}/pdf")]
        public IActionResult ObterExtratoPorClienteIdPdf(int clienteId)
        {
            try
            {
                var extrato = _investimento.ObterExtratoPorClienteId(clienteId);
                var pdfBytes = _extratoPdf.GerarExtratoPdf(extrato);
                return File(pdfBytes, "application/pdf", "extrato.pdf");
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

        [HttpGet("extrato /{produtoId}")]
        public IActionResult ListarInvestimentosPorProdutoFinanceiro()
        {
            try
            {
                var investimentosPorProdutoFinanceiro = _investimento.ListarInvestimentosPorProdutoFinanceiro();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    // outras opções, se necessário
                };

                return Ok(JsonSerializer.Serialize(investimentosPorProdutoFinanceiro, options));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar os investimentos por produto financeiro: {ex.Message}");
            }
        }

        [HttpGet("extrato/produto-financeiro-pdf")]
        public IActionResult GerarExtratoProdutoFinanceiroPDF()
        {
            try
            {
                // Obter os investimentos por produto financeiro
                Dictionary<int, List<InvestimentoDetalheDTO>> investimentosPorProdutoFinanceiro = _investimento.ListarInvestimentosPorProdutoFinanceiro();

                // Gerar o PDF do extrato
                byte[] pdfBytes = _extratoPdf.GerarExtratoProdutoFinanceiroPDF(investimentosPorProdutoFinanceiro);

                // Retornar o arquivo PDF como resposta HTTP
                return File(pdfBytes, "application/pdf", "extrato_investimentos.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar o extrato em PDF: {ex.Message}");
            }
        }

        [HttpGet("vencimento-proximo")]
        public IActionResult ListarInvestimentosVencimentoProximo([FromQuery] int dias = 10)
        {
            try
            {
                var investimentosAgrupados = _investimento.ListarInvestimentosVencimentoProximo(dias);

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    // outras opções, se necessário
                };

                return Ok(JsonSerializer.Serialize(investimentosAgrupados, options));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar os investimentos com vencimento próximo: {ex.Message}");
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
