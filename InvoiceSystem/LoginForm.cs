using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace InvoiceSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (email == "" || password == "")
            {
                MessageBox.Show("Wpisz email i hasło");
                return;
            }

            try
            {
                SqlConnection conn = Database.CreateConnection();
                conn.Open();

                string sql = @"SELECT Id, PasswordHash, PasswordSalt
                               FROM Users
                               WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show("Nieprawidłowy email lub hasło");
                    conn.Close();
                    return;
                }

                int userId = reader.GetInt32(0);
                byte[] dbHash = (byte[])reader["PasswordHash"];
                byte[] dbSalt = (byte[])reader["PasswordSalt"];

                reader.Close();

                var pbkdf2 = new Rfc2898DeriveBytes(
                    password,
                    dbSalt,
                    100_000,
                    HashAlgorithmName.SHA256
                );

                byte[] inputHash = pbkdf2.GetBytes(32);

                bool same = true;

                for (int i = 0; i < inputHash.Length; i++)
                {
                    if (inputHash[i] != dbHash[i])
                    {
                        same = false;
                        break;
                    }
                }

                if (!same)
                {
                    MessageBox.Show("Nieprawidłowy email lub hasło");
                    conn.Close();
                    return;
                }

                conn.Close();

                Random rnd = new Random();
                string code = rnd.Next(100000, 999999).ToString();

                SqlConnection conn2 = Database.CreateConnection();
                conn2.Open();

                string ins = @"INSERT INTO TwoFactorCodes (UserId, Code, ExpiresAt, Used)
               VALUES (@UserId, @Code, DATEADD(MINUTE, 5, SYSUTCDATETIME()), 0)";

                SqlCommand insCmd = new SqlCommand(ins, conn2);
                insCmd.Parameters.AddWithValue("@UserId", userId);
                insCmd.Parameters.AddWithValue("@Code", code);
                insCmd.ExecuteNonQuery();

                conn2.Close();

                MessageBox.Show("Twój kod 2FA: " + code);

                TwoFactorForm tf = new TwoFactorForm(userId);
                tf.ShowDialog();

                if (tf.IsVerified)
                {
                    Session.Login(userId, email);
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("2FA nieudane");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }
    }
}
