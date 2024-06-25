using GestaoPortfolioInvestimento.Models;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolioInvestimento.Services

{
    public class ClienteService : ICliente
    {
        private readonly DataContext _context;

        public ClienteService(DataContext context)
        {
            _context = context;
        }

        public void AdicionarCliente(ClienteDTO clienteDto)
        {
            if (clienteDto == null)
            {
                throw new ArgumentNullException(nameof(clienteDto), "O cliente não pode ser nulo");
            }

            var novoCliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                DataNascimento = clienteDto.DataNascimento,
                Endereco = clienteDto.Endereco
            };

            _context.Clientes.Add(novoCliente);
            _context.SaveChanges();
        }

        public Cliente ObterClientePorId(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ID == id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            }
            return cliente;
        }

        public List<Cliente> ObterTodosClientes(int skip, int take)
        {
            return _context.Clientes.Skip(skip).Take(take).ToList();
        }

        public void AtualizarCliente(int id, ClienteDTO clienteDto)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado");
            }

            cliente.Nome = clienteDto.Nome;
            cliente.Email = clienteDto.Email;
            cliente.DataNascimento = clienteDto.DataNascimento;
            cliente.Endereco = clienteDto.Endereco;

            _context.SaveChanges();

        }

        public void RemoverCliente(int id)
        {
            var cliente = ObterClientePorId(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }
    }
}
