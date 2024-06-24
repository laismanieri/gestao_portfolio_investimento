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
    public class TransacaoController : ControllerBase
    {

        private readonly ITransacao _transacao;

        public TransacaoController(ITransacao transacao)
        {
            _transacao = transacao;
        }


        [HttpGet]
        public IActionResult ObterTodosTransacaos([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var transacoes = _transacao.ObterTodasTransacoes(skip, take);
            return Ok(transacoes);
        }

        [HttpGet("{id}")]
        public IActionResult ObtertransacaoPorId(int id)
        {
            try
            {
                var transacao = _transacao.ObterTransacaoPorId(id);
                return Ok(transacao);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
