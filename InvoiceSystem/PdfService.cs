using Microsoft.Data.SqlClient;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace InvoiceSystem
{
    public static class PdfService
    {
        public static void ExportInvoiceToPdf(int invoiceId)
        {
            try
            {
                InvoiceData data = LoadInvoice(invoiceId);
                if (data == null)
                {
                    MessageBox.Show("Nie znaleziono faktury");
                    return;
                }

                Document doc = new Document();
                doc.Info.Title = "Faktura " + data.Number;

                Section sec = doc.AddSection();

                Paragraph title = sec.AddParagraph("FAKTURA");
                title.Format.Font.Size = 18;
                title.Format.Font.Bold = true;
                title.Format.SpaceAfter = "0.5cm";

                sec.AddParagraph("Numer: " + data.Number);
                sec.AddParagraph("Data wystawienia: " + data.IssueDate.ToString("yyyy-MM-dd"));
                sec.AddParagraph("Waluta: " + data.Currency + " | Kurs: " + data.Rate.ToString("0.0000"));
                sec.AddParagraph(" ");
                sec.AddParagraph("Nabywca:");
                sec.AddParagraph(data.ContractorName);
                sec.AddParagraph("NIP: " + data.ContractorNip);
                sec.AddParagraph(data.ContractorAddress ?? "");
                sec.AddParagraph(" ");

                Table table = sec.AddTable();
                table.Borders.Width = 0.5;

                table.AddColumn("7cm");  
                table.AddColumn("2cm");  
                table.AddColumn("3cm");  
                table.AddColumn("2cm");  
                table.AddColumn("3cm");  

                Row header = table.AddRow();
                header.Shading.Color = Colors.LightGray;
                header.Cells[0].AddParagraph("Nazwa");
                header.Cells[1].AddParagraph("Ilość");
                header.Cells[2].AddParagraph("Cena");
                header.Cells[3].AddParagraph("VAT %");
                header.Cells[4].AddParagraph("Brutto");

                decimal sumGrossForeign = 0m;

                foreach (var it in data.Items)
                {
                    decimal net = it.Quantity * it.UnitPrice;
                    decimal gross = net * (1m + it.VatRate / 100m);
                    sumGrossForeign += gross;

                    Row row = table.AddRow();
                    row.Cells[0].AddParagraph(it.Name);
                    row.Cells[1].AddParagraph(it.Quantity.ToString("0.##"));
                    row.Cells[2].AddParagraph(it.UnitPrice.ToString("0.00"));
                    row.Cells[3].AddParagraph(it.VatRate.ToString("0.##"));
                    row.Cells[4].AddParagraph(gross.ToString("0.00"));
                }

                decimal sumGrossPLN = sumGrossForeign * data.Rate;

                sec.AddParagraph(" ");
                Paragraph totals = sec.AddParagraph(
                    $"Suma brutto: {sumGrossForeign:0.00} {data.Currency}    |    {sumGrossPLN:0.00} PLN"
                );
                totals.Format.Font.Bold = true;

                string fileName = $"Faktura_{data.Number.Replace("/", "_")}.pdf";
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = doc;
                renderer.RenderDocument();
                renderer.PdfDocument.Save(path);

                MessageBox.Show("Zapisano PDF:\n" + path);

                try { Process.Start(new ProcessStartInfo(path) { UseShellExecute = true }); } catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF error: " + ex.Message);
            }
        }

        private static InvoiceData LoadInvoice(int invoiceId)
        {
            SqlConnection conn = Database.CreateConnection();
            conn.Open();

            string sql = @"
SELECT i.Number, i.IssueDate, i.CurrencyCode, i.ExchangeRate,
       c.Name as ContractorName, c.NIP as ContractorNip, c.Address as ContractorAddress
FROM Invoices i
JOIN Contractors c ON c.Id = i.ContractorId
WHERE i.Id = @Id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", invoiceId);

            SqlDataReader r = cmd.ExecuteReader();
            if (!r.Read())
            {
                conn.Close();
                return null;
            }

            InvoiceData data = new InvoiceData();
            data.Number = r["Number"].ToString();
            data.IssueDate = Convert.ToDateTime(r["IssueDate"]);
            data.Currency = r["CurrencyCode"].ToString();
            data.Rate = Convert.ToDecimal(r["ExchangeRate"]);
            data.ContractorName = r["ContractorName"].ToString();
            data.ContractorNip = r["ContractorNip"].ToString();
            data.ContractorAddress = r["ContractorAddress"] == DBNull.Value ? null : r["ContractorAddress"].ToString();

            r.Close();

            string sqlItems = @"SELECT Name, Quantity, UnitPrice, VatRate FROM InvoiceItems WHERE InvoiceId = @Id";
            SqlCommand cmd2 = new SqlCommand(sqlItems, conn);
            cmd2.Parameters.AddWithValue("@Id", invoiceId);

            SqlDataReader r2 = cmd2.ExecuteReader();
            while (r2.Read())
            {
                data.Items.Add(new InvoiceItemData
                {
                    Name = r2["Name"].ToString(),
                    Quantity = Convert.ToDecimal(r2["Quantity"]),
                    UnitPrice = Convert.ToDecimal(r2["UnitPrice"]),
                    VatRate = Convert.ToDecimal(r2["VatRate"])
                });
            }

            r2.Close();
            conn.Close();

            return data;
        }

        private class InvoiceData
        {
            public string Number;
            public DateTime IssueDate;
            public string Currency;
            public decimal Rate;
            public string ContractorName;
            public string ContractorNip;
            public string ContractorAddress;
            public System.Collections.Generic.List<InvoiceItemData> Items =
                new System.Collections.Generic.List<InvoiceItemData>();
        }

        private class InvoiceItemData
        {
            public string Name;
            public decimal Quantity;
            public decimal UnitPrice;
            public decimal VatRate;
        }
    }
}
