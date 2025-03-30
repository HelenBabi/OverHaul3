using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Overhaul.Application.Utilitty
{
    public class CryptoService : ICryptoService
    {
        public bool Check(string value, string hashedValue)
        {
            return Hash(value) == hashedValue;
        }
        public string Hash(string value)
        {
            var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteHash = sha256.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
