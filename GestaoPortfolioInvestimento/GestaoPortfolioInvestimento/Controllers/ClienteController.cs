using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente)
        {
            _cliente = cliente;
        }


        [HttpGet]
        public IActionResult ObterTodosClientes()
        {
            var clientes = _cliente.ObterTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult ObterClientePorId(int id)
        {      
            try
            {
                var cliente = _cliente.ObterClientePorId(id);                
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarCliente([FromBody] Cliente cliente)
        {
            try
            {
                _cliente.AdicionarCliente(cliente);
                return CreatedAtAction(nameof(ObterClientePorId), new { id = cliente.ID }, cliente);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ID)
            {
                return BadRequest("ID do cliente não encontrado");
            }

            try
            {
                _cliente.AtualizarCliente(cliente);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverCliente(int id)
        {
            try
            {
                _cliente.RemoverCliente(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
