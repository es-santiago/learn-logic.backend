using System;
using System.Security.Cryptography;
using System.Text;

namespace LearnLogic.Infra.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateHash(this string str)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hash = sha256Hash.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
