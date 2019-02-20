using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class CryptographyHelper
    {
        public string EncryptPassword(string password)
        {
            HashAlgorithm hash = HashAlgorithm.Create("SHA512");

            var encoded = Encoding.UTF8.GetBytes(password);
            var encrypted = hash.ComputeHash(encoded);

            var sb = new StringBuilder();
            foreach (var caracter in encrypted)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerifyPassword(string password, string passwordVerify)
        {
            return EncryptPassword(password) == passwordVerify;
        }
    }
}
