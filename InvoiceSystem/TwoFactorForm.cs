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
    public partial class TwoFactorForm : Form
    {
        private readonly int _userId;

        public bool IsVerified { get; private set; }
        public TwoFactorForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
            IsVerified = false;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();

            if (code == "")
            {
                MessageBox.Show("Wpisz kod");
                return;
            }

            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                string sql = @"
SELECT TOP 1 Id, ExpiresAt
FROM TwoFactorCodes
WHERE UserId = @UserId
  AND Code = @Code
  AND Used = 0
  AND ExpiresAt > SYSUTCDATETIME()
ORDER BY Id DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", _userId);
                cmd.Parameters.AddWithValue("@Code", code);

                SqlDataReader r = cmd.ExecuteReader();

                if (!r.Read())
                {
                    MessageBox.Show("Kod nieprawidłowy lub wygasł");
                    conn.Close();
                    return;
                }

                int codeId = r.GetInt32(0);
                r.Close();

                SqlCommand upd = new SqlCommand("UPDATE TwoFactorCodes SET Used = 1 WHERE Id = @Id", conn);
                upd.Parameters.AddWithValue("@Id", codeId);
                upd.ExecuteNonQuery();

                conn.Close();

                IsVerified = true;
                MessageBox.Show("2FA OK");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        
    }
}
