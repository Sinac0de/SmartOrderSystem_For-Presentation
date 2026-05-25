using System;
using System.Security.Cryptography;
using System.Text;

namespace OrderSystem.Infrastructure {
    public class AuthService {
        // [Security Smell: Weak Cryptographic Algorithm]
        // MD5 is deprecated and highly vulnerable to collision attacks.
        public string HashPassword(string plainText) {
            using (MD5 md5 = MD5.Create()) {
                byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}