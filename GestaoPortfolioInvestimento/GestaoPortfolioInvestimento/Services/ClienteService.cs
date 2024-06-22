using GestaoPortfolioInvestimento.Models;
using GestaoPortfolioInvestimento.Interfaces;

namespace GestaoPortfolioInvestimento.Services

{
    public class ClienteService : ICliente
    {

        private List<Cliente> clientes = new List<Cliente>();

        public void AdicionarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "O cliente não pode ser nulo"); 
            }
            clientes.Add(cliente);
        }

        public Cliente ObterClientePorId(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.ID == id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            }
            return cliente;
        }

        public List<Cliente> ObterTodosClientes()
        {
            return clientes;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            var index = clientes.FindIndex(c => c.ID == cliente.ID);
            if (index == -1)
            {
                throw new KeyNotFoundException($"Cliente com ID {cliente.ID} não encontrado.");
            }

            clientes[index] = cliente;
        }

        public void RemoverCliente(int id)
        {
            var cliente = ObterClientePorId(id);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            }

            clientes.Remove(cliente);
        }
    }
}
