using GestaoPortfolioInvestimento.DTO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Services
{
    public class ExtratoService
    {
        public byte[] GerarExtratoPdf(ExtratoClienteDTO extrato)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                writer.CloseStream = false;

                document.Open();

                document.Add(new Paragraph($"Extrato do Cliente: {extrato.ClienteNome}"));
                document.Add(new Paragraph($"ID do Cliente: {extrato.ClienteID}"));
                document.Add(new Paragraph(" ")); 

                foreach (var investimento in extrato.Investimentos)
                {
                    document.Add(new Paragraph($"Produto Financeiro: {investimento.ProdutoFinanceiroNome}"));
                    document.Add(new Paragraph($"Tipo do Financeiro: {investimento.Tipo}"));
                    document.Add(new Paragraph($"Quantidade: {investimento.Quantidade}"));
                    document.Add(new Paragraph($"Valor Total: {investimento.ValorTotal:C}"));
                    document.Add(new Paragraph($"Data de Adesão: {investimento.DataAdesao.ToShortDateString()}"));
                    document.Add(new Paragraph($"Data de Venda: {investimento.DataVenda?.ToShortDateString() ?? "N/A"}"));
                    document.Add(new Paragraph($"Data de Vencimento: {investimento.Vencimento.ToShortDateString()}"));
                    document.Add(new Paragraph($"Rendimento: {investimento.Rendimento:C}"));
                    document.Add(new Paragraph("Transações:"));

                    Table table = new Table(4);
                    table.AddCell("Data");
                    table.AddCell("Tipo");
                    table.AddCell("Quantidade");
                    table.AddCell("Valor");

                    foreach (var transacao in investimento.Transacoes)
                    {
                        table.AddCell(transacao.Data.ToShortDateString());
                        table.AddCell(transacao.TipoTransacao.ToString());
                        table.AddCell(transacao.Quantidade.ToString());
                        table.AddCell(transacao.ValorTotal.ToString("C"));
                    }

                    document.Add(table);
                    document.Add(new Paragraph(" ")); 
                }

                document.Close();
                return ms.ToArray();
            }
        }

        public byte[] GerarExtratoProdutoFinanceiroPDF(Dictionary<int, List<InvestimentoDetalheDTO>> investimentosPorProdutoFinanceiro)
        {

            Document doc = new Document();
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            Paragraph title = new Paragraph("Extrato de Investimentos por Produto Financeiro\n\n");
            doc.Add(title);

            foreach (var kvp in investimentosPorProdutoFinanceiro)
            {

                Paragraph produtoFinanceiroTitle = new Paragraph($"Produto Financeiro ID: {kvp.Key}\n\n");
                doc.Add(produtoFinanceiroTitle);

                foreach (var investimento in kvp.Value)
                {
                    Paragraph investimentoInfo = new Paragraph($"Produto Financeiro: {investimento.ProdutoFinanceiroNome}\n" +
                                                                $"Tipo: {investimento.Tipo}\n" +
                                                                $"Quantidade: {investimento.Quantidade}\n" +
                                                                $"Valor Total: {investimento.ValorTotal}\n" +
                                                                $"Data de Adesão: {investimento.DataAdesao}\n" +
                                                                $"Data de Venda: {investimento.DataVenda}\n" +
                                                                $"Vencimento: {investimento.Vencimento}\n" +
                                                                $"Taxa de Retorno: {investimento.TaxaRetorno}\n" +
                                                                $"Rendimento: {investimento.Rendimento}\n\n");
                    doc.Add(investimentoInfo);
                }
                doc.Add(new Chunk("\n\n\n\n"));
            }

            doc.Close();
            writer.Close();

            return stream.ToArray();
        }

    }
}
