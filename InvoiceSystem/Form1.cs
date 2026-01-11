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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().ShowDialog();
        }

        private void btnOpenLogin_Click(object sender, EventArgs e)
        {
            var lf = new LoginForm();
            lf.ShowDialog();

            if (Session.IsLoggedIn)
            {
                
                this.Hide();
                var mf = new MainForm();
                mf.FormClosed += (s, args) => this.Close(); 
                mf.Show();
            }
        }

        
    }
}
