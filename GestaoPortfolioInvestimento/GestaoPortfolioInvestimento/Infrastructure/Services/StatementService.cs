using iTextSharp.text.pdf;
using iTextSharp.text;
using InvestmentPortfolioManagement.Application.DTOs;

namespace InvestmentPortfolioManagement.Infrastructure.Services
{
    public class StatementService
    {
        public byte[] GenerateCustomerStatementPdf(CustomerStatementDTO statement)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                writer.CloseStream = false;

                document.Open();

                document.Add(new Paragraph($"Customer Statement: {statement.CustomerName}"));
                document.Add(new Paragraph($"Customer ID: {statement.CustomerId}"));
                document.Add(new Paragraph(" "));

                foreach (var investment in statement.Investments)
                {
                    document.Add(new Paragraph($"Financial Product: {investment.FinancialProductName}"));
                    document.Add(new Paragraph($"Type: {investment.FinancialProductType}"));
                    document.Add(new Paragraph($"Quantity: {investment.Quantity}"));
                    document.Add(new Paragraph($"Total Value: {investment.TotalValue}"));
                    document.Add(new Paragraph($"Subscription Date: {investment.SubscriptionDate.ToShortDateString()}"));
                    document.Add(new Paragraph($"Sale Date: {investment.SaleDate?.ToShortDateString() ?? "N/A"}"));
                    document.Add(new Paragraph($"Maturity Date: {investment.MaturityDate.ToShortDateString()}"));
                    document.Add(new Paragraph($"Yield: {investment.ReturnRate:C}"));
                    document.Add(new Paragraph("Transactions:"));

                    Table table = new Table(4);
                    table.AddCell("Date");
                    table.AddCell("Type");
                    table.AddCell("Quantity");
                    table.AddCell("Value");

                    foreach (var transaction in investment.Transactions)
                    {
                        table.AddCell(transaction.Date.ToShortDateString());
                        table.AddCell(transaction.TransactionType.ToString());
                        table.AddCell(transaction.Quantity.ToString());
                        table.AddCell(transaction.TotalValue.ToString("C"));
                    }

                    document.Add(table);
                    document.Add(new Paragraph(" "));
                }

                document.Close();
                return ms.ToArray();
            }
        }

        public byte[] GenerateFinancialProductStatementPdf(Dictionary<int, List<InvestmentDetailDTO>> investmentsByProduct)
        {
            Document doc = new Document();
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            doc.Add(new Paragraph("Investment Statement by Financial Product\n\n"));

            foreach (var kvp in investmentsByProduct)
            {
                doc.Add(new Paragraph($"Financial Product ID: {kvp.Key}\n\n"));

                foreach (var investment in kvp.Value)
                {
                    doc.Add(new Paragraph(
                        $"Financial Product: {investment.FinancialProductName}\n" +
                        $"Type: {investment.FinancialProductType}\n" +
                        $"Quantity: {investment.Quantity}\n" +
                        $"Total Value: {investment.TotalValue}\n" +
                        $"Subscription Date: {investment.SubscriptionDate}\n" +
                        $"Sale Date: {investment.SaleDate}\n" +
                        $"Maturity Date: {investment.MaturityDate}\n" +
                        $"Return Rate: {investment.ReturnRate}\n" +
                        $"Yield: {investment.Yield}\n\n"
                    ));
                }
                doc.Add(new Chunk("\n\n\n\n"));
            }

            doc.Close();
            writer.Close();

            return stream.ToArray();
        }
    }
}