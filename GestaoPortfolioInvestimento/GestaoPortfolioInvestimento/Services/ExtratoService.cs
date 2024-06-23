using GestaoPortfolioInvestimento.DTO;
using iTextSharp.text.pdf;
using iTextSharp.text;

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
    }
}
