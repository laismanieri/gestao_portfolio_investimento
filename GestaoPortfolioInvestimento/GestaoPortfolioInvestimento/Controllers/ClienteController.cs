using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using GestaoPortfolioInvestimento.DTO;


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
        public IActionResult ObterTodosClientes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var clientes = _cliente.ObterTodosClientes(skip, take);
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
        public IActionResult AdicionarCliente([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                _cliente.AdicionarCliente(clienteDto);
                return CreatedAtAction(nameof(ObterClientePorId), new { id = clienteDto.ID }, clienteDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, [FromBody] ClienteDTO clienteDto)
        {
            try
            {
                _cliente.AtualizarCliente(id, clienteDto);
                return NoContent();
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
