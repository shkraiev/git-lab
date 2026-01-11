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
using System.Security.Cryptography;

namespace InvoiceSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtPasswordConfirm.Text;

            if (email == "" || password == "" || confirm == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Hasła nie są takie same");
                return;
            }

            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                string sql = @"INSERT INTO Users (Email, PasswordHash, PasswordSalt)
                               VALUES (@Email, @Hash, @Salt)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Hash", hash);
                cmd.Parameters.AddWithValue("@Salt", salt);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Rejestracja zakończona sukcesem");
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                    MessageBox.Show("Taki email już istnieje");
                else
                    MessageBox.Show("Błąd DB: " + ex.Message);
            }

        }
    }
}
