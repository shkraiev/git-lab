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

namespace InvoiceSystem
{
    public partial class ContractorForm : Form
    {
        public ContractorForm()
        {
            InitializeComponent();
            LoadContractors();
        }

        private void LoadContractors()
        {
            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Id, NIP, Name, Address, CreatedAt FROM Contractors ORDER BY Id DESC",
                    conn
                );

                DataTable dt = new DataTable();
                da.Fill(dt);

                gridContractors.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nip = txtNip.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (nip == "" || name == "")
            {
                MessageBox.Show("Wpisz NIP i Nazwę");
                return;
            }

            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                string sql = @"INSERT INTO Contractors (NIP, Name, Address)
                       VALUES (@NIP, @Name, @Address)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NIP", nip);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", (object)address ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                conn.Close();

                txtNip.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";

                LoadContractors();
                MessageBox.Show("Dodano kontrahenta");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd DB: " + ex.Message);
            }
        }
    }
}
