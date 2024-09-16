using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Helpers
{
    public static class OTPGenerator
    {
        public static string CreateOTP()
        {
            Random random = new Random();
            string randomno = random.Next(100000, 999999).ToString();
            return randomno;
        }
    }
}
