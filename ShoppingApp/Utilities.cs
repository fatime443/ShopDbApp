using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    static class Utilities
    {
        public static  bool IsEmpty(string[] array, string text)
        {
            foreach (var item in array)
            {
                if (item == text) return false;
            }
            return true;
        }
        public static string HashCode(this string password)
        {
            byte[] hashByte = new ASCIIEncoding().GetBytes(password);
            byte[] hashPassword = new SHA256Managed().ComputeHash(hashByte);
            return new ASCIIEncoding().GetString(hashPassword);
        }
    }
}
