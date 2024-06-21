using GestaoPortfolioInvestimento.Models;
using GestaoPortfolioInvestimento.Interfaces;

namespace GestaoPortfolioInvestimento.Services

{
    public class ClienteService 
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

        public Cliente ObterClientePorID(int id)
        {
            return clientes.FirstOrDefault(c => c.ID == id);
        }

        public List<Cliente> ObterTodosClientes()
        {
            return clientes;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            var index = clientes.FindIndex(c => c.ID == cliente.ID);
            if (index != -1)
            {
                clientes[index] = cliente;
            }
        }

        public void RemoverCliente(int id)
        {
            clientes.RemoveAll(c => c.ID == id);
        }
    }
}
