using System;
using System.Security.Cryptography;

namespace HashHelper
{
    public static class HashingHelper
    {

        /* https://stackoverflow.com/a/10402129 */
        public static string GetHash(string pass, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public static byte[] GetSaltFromPassword(string pass)
        {
            byte[] hashBytes = Convert.FromBase64String(pass);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            return salt;
        }

        public static byte[] GenerateNewSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }

    }
}