using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace InvoiceSystem
{
    public partial class InvoiceForm : Form
    {
        private int _lastSavedInvoiceId = 0;
        public InvoiceForm()
        {
            InitializeComponent();
            SetupItemsGrid();
            LoadContractors();
            SetupCurrency();
        }

        private void SetupItemsGrid()
        {
            gridItems.Columns.Clear();

            gridItems.Columns.Add("Name", "Nazwa");
            gridItems.Columns.Add("Qty", "Ilość");
            gridItems.Columns.Add("UnitPrice", "Cena");
            gridItems.Columns.Add("VatRate", "VAT %");

            gridItems.Columns["Qty"].ValueType = typeof(decimal);
            gridItems.Columns["UnitPrice"].ValueType = typeof(decimal);
            gridItems.Columns["VatRate"].ValueType = typeof(decimal);

            gridItems.CellEndEdit += (s, e) => RecalculateTotals();
            gridItems.UserDeletedRow += (s, e) => RecalculateTotals();
        }

        private void LoadContractors()
        {
            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Id, Name FROM Contractors ORDER BY Name",
                    conn
                );

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbContractor.DisplayMember = "Name";
                cmbContractor.ValueMember = "Id";
                cmbContractor.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        private void SetupCurrency()
        {
            cmbCurrency.Items.Clear();
            cmbCurrency.Items.Add("EUR");
            cmbCurrency.Items.Add("USD");
            cmbCurrency.Items.Add("GBP");
            cmbCurrency.SelectedIndex = 0;

            txtRate.Text = "1.0000";
        }

        private void RecalculateTotals()
        {
            decimal grossForeign = 0m;

            for (int i = 0; i < gridItems.Rows.Count; i++)
            {
                DataGridViewRow row = gridItems.Rows[i];
                if (row.IsNewRow) continue;

                string name = Convert.ToString(row.Cells["Name"].Value);
                if (string.IsNullOrWhiteSpace(name)) continue;

                decimal qty = ParseDecimal(row.Cells["Qty"].Value);
                decimal price = ParseDecimal(row.Cells["UnitPrice"].Value);
                decimal vat = ParseDecimal(row.Cells["VatRate"].Value);

                decimal net = qty * price;
                decimal gross = net * (1m + vat / 100m);

                grossForeign += gross;
            }

            decimal rate = ParseDecimal(txtRate.Text);
            decimal grossPLN = grossForeign * rate;

            lblTotalForeign.Text = $"Suma brutto (waluta): {grossForeign:F2} {cmbCurrency.Text}";
            lblTotalPLN.Text = $"Suma brutto (PLN): {grossPLN:F2} PLN";
        }

        private decimal ParseDecimal(object val)
        {
            if (val == null) return 0m;

            string s = val.ToString().Trim();
            if (s == "") return 0m;

            s = s.Replace(",", ".");

            decimal d;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return d;

            return 0m;
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            RecalculateTotals();
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (cmbContractor.SelectedValue == null)
            {
                MessageBox.Show("Wybierz kontrahenta");
                return;
            }

            int contractorId = Convert.ToInt32(cmbContractor.SelectedValue);
            string number = txtNumber.Text.Trim();
            if (number == "")
            {
                MessageBox.Show("Wpisz numer faktury");
                return;
            }

            string currency = cmbCurrency.Text;
            decimal rate = ParseDecimal(txtRate.Text);

            decimal grossForeign = 0m;

            for (int i = 0; i < gridItems.Rows.Count; i++)
            {
                DataGridViewRow row = gridItems.Rows[i];
                if (row.IsNewRow) continue;

                string name = Convert.ToString(row.Cells["Name"].Value);
                if (string.IsNullOrWhiteSpace(name)) continue;

                decimal qty = ParseDecimal(row.Cells["Qty"].Value);
                decimal price = ParseDecimal(row.Cells["UnitPrice"].Value);
                decimal vat = ParseDecimal(row.Cells["VatRate"].Value);

                decimal net = qty * price;
                decimal gross = net * (1m + vat / 100m);

                grossForeign += gross;
            }

            decimal grossPLN = grossForeign * rate;

            decimal totalNetForeign = 0m;
            decimal totalNetPLN = 0m;

            try
            {
                if (!Session.IsLoggedIn)
                {
                    MessageBox.Show("Brak sesji. Zaloguj się ponownie.");
                    return;
                }
                int userId = Session.UserId;

                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                string insInvoice = @"
INSERT INTO Invoices
(Number, IssueDate, CurrencyCode, ExchangeRate,
 TotalNetForeign, TotalGrossForeign, TotalNetPLN, TotalGrossPLN,
 UserId, ContractorId)
VALUES
(@Number, @IssueDate, @Currency, @Rate,
 @NetF, @GrossF, @NetPLN, @GrossPLN,
 @UserId, @ContractorId);
SELECT CAST(SCOPE_IDENTITY() AS int);";

                SqlCommand cmdInv = new SqlCommand(insInvoice, conn);
                cmdInv.Parameters.AddWithValue("@Number", number);
                cmdInv.Parameters.AddWithValue("@IssueDate", dtIssueDate.Value.Date);
                cmdInv.Parameters.AddWithValue("@Currency", currency);
                cmdInv.Parameters.AddWithValue("@Rate", rate);
                cmdInv.Parameters.AddWithValue("@NetF", totalNetForeign);
                cmdInv.Parameters.AddWithValue("@GrossF", grossForeign);
                cmdInv.Parameters.AddWithValue("@NetPLN", totalNetPLN);
                cmdInv.Parameters.AddWithValue("@GrossPLN", grossPLN);
                cmdInv.Parameters.AddWithValue("@UserId", userId);
                cmdInv.Parameters.AddWithValue("@ContractorId", contractorId);

                int invoiceId = (int)cmdInv.ExecuteScalar();

                for (int i = 0; i < gridItems.Rows.Count; i++)
                {
                    DataGridViewRow row = gridItems.Rows[i];
                    if (row.IsNewRow) continue;

                    string name = Convert.ToString(row.Cells["Name"].Value);
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    decimal qty = ParseDecimal(row.Cells["Qty"].Value);
                    decimal price = ParseDecimal(row.Cells["UnitPrice"].Value);
                    decimal vat = ParseDecimal(row.Cells["VatRate"].Value);

                    string insItem = @"
INSERT INTO InvoiceItems (InvoiceId, Name, Quantity, UnitPrice, VatRate)
VALUES (@InvoiceId, @Name, @Qty, @Price, @Vat)";

                    SqlCommand cmdItem = new SqlCommand(insItem, conn);
                    cmdItem.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    cmdItem.Parameters.AddWithValue("@Name", name);
                    cmdItem.Parameters.AddWithValue("@Qty", qty);
                    cmdItem.Parameters.AddWithValue("@Price", price);
                    cmdItem.Parameters.AddWithValue("@Vat", vat);

                    cmdItem.ExecuteNonQuery();
                }

                conn.Close();

                _lastSavedInvoiceId = invoiceId;
                MessageBox.Show("Faktura zapisana");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd DB: " + ex.Message);
            }
        }

        private async void btnGetRate_Click(object sender, EventArgs e)
        {
            string cur = cmbCurrency.Text;

            try
            {
                string url = "https://api.nbp.pl/api/exchangerates/rates/a/" + cur + "/?format=json";

                using (HttpClient http = new HttpClient())
                {
                    string json = await http.GetStringAsync(url);

                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;
                        var rates = root.GetProperty("rates");
                        var mid = rates[0].GetProperty("mid").GetDecimal();

                        txtRate.Text = mid.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się pobrać kursu: " + ex.Message);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (_lastSavedInvoiceId == 0)
            {
                MessageBox.Show("Najpierw zapisz fakturę");
                return;
            }

            PdfService.ExportInvoiceToPdf(_lastSavedInvoiceId);
        }
    }
}
