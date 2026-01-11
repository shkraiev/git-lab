using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    public static class Session
    {
        public static int UserId { get; private set; } = 0;
        public static string Email { get; private set; } = "";

        public static bool IsLoggedIn => UserId > 0;

        public static void Login(int userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public static void Logout()
        {
            UserId = 0;
            Email = "";
        }
    }
}
