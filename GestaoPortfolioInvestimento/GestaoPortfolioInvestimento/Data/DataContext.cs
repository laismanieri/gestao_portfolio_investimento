using GestaoPortfolioInvestimento.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolioInvestimento.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Investimento> Investimentos { get; set; }
        public DbSet<ProdutoFinanceiro> ProdutosFinanceiros { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento Cliente -> Investimentos
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Investimentos)
                .WithOne(i => i.Cliente)
                .HasForeignKey(i => i.ClienteID);

            // Configuração do relacionamento ProdutoFinanceiro -> Investimentos
            modelBuilder.Entity<ProdutoFinanceiro>()
                .HasMany(p => p.Investimentos)
                .WithOne(i => i.ProdutoFinanceiro)
                .HasForeignKey(i => i.ProdutoFinanceiroID);

            // Configuração do relacionamento Investimento -> Transacoes
            modelBuilder.Entity<Investimento>()
                .HasMany(i => i.Transacoes)
                .WithOne(t => t.Investimento)
                .HasForeignKey(t => t.InvestimentoID);
        }

    }
}
