using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Zalogowany: " + Session.Email;
        }

        private void btnContractors_Click(object sender, EventArgs e)
        {
            new ContractorForm().ShowDialog();
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            new InvoiceForm().ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Logout();
            MessageBox.Show("Wylogowano");
            this.Close();
        }
    }
}
