using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace InvoiceSystem
{
    public static class Database
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=WinFormsInvoiceSystem;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
