using System;
using System.Security.Cryptography;
using System.Text;

namespace OrderSystem.Infrastructure {
    public class AuthService : IAuthService {
        // [Refactored: Strong Cryptography (Fixes MD5 Vulnerability)]
        public string HashPassword(string plainText) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}