using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RawData
{
    class Program
    {
        public static string Decrypt(string encryptedString)
        {
            string encryptionKey = GetEncryptionKey();
            return Decrypt(encryptedString, encryptionKey);
        }

        private static string GetEncryptionKey()
        {
            string text = ConfigurationManager.AppSettings.Get(customEncryptionKey);
            if (text != null)
            {
                return text;
            }
            return defaultEncryptionKey;
        }
        internal static string Decrypt(string encryptedString, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            byte[] rgbSalt = new byte[]
            {
                58,
                84,
                91,
                25,
                10,
                34,
                29,
                68,
                60,
                88,
                44,
                51,
                1
            };
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, rgbSalt);
            byte[] bytes = Decrypt(encryptedBytes, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
            return Encoding.Unicode.GetString(bytes);
        }
        private static byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
        {
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, new AesCryptoServiceProvider
            {
                Key = key,
                IV = iv
            }.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
        private static readonly string defaultEncryptionKey = "PrivateKeyForEncryptionOfRadAsyncUploadConfiguration";
        private static readonly string customEncryptionKey = "Telerik.AsyncUpload.ConfigurationEncryptionKey";
        static void Main(string[] args)
        {
            var type = "6R/cGaqQeHVAzdJ9wTFOyCsrMSTtqcjLe8AHwiPckPDUwecnJyNlkDYwDQpxGYQ9hs6YxhupK310sbCbtXB4H6Dz5rGNL40nkkyo4j2clmRr08jtFsPQ0RpE5BGsulPT3l0MxyAvPFMs8bMybUyAP+9RB9LoHE3Xo8BqDadX3HR9kkDlD5/LMkIK/Qd7CKfDirw0NsEVxbGdsw7duPFUavN/5QyGopfI2mOfyRH+ibBOC12BE4PDNsVMczFTpnMJOvqXORCMpuLAhUJESgPvcCRaJ670SqbLhAGH8YP6WzL7G0k0XwrHrGdgjbpnZN844yCRDgFw7MbAAm1K/egh+eiIT4FkwjU7NqfLWGquKMQ=";
            var obj = "ATTu5i4R+ViNFYO6kst0jC11wM/1iqH+W/isjhaDjNtSL4i0BX6wnHZVtkSpfZ8pOxh5aIKzTO+1TvLT/S6NT0wnqIPU8UuGifJAcamKaU3Hk+TDkENgsXOQTf+k0ytkSn3O02pkVTzDxJOciGfp5NpKMmQVVqaibXVeHsxQNhJWFTz4FNr7XsLdSeeCzxsJiB7GgHaxJCZ+demNqq6VtqfITgK1/Ad/LKpPK823dkdf9zEw2EJ5vJ5Wlcirm6Gnnu0j1M+9Hhb4pj3sxH0qzDyEDZcBbgWPl6ZqoIC4xE7h3u6RFfqTtsJtYqVzoqUVWc3OIfhMtsqz/0gL7qUUOoALgdMspIT3jFYtkOwqhwwfme6nIMpksXMcpSHKrvmMMGYvPBk3N1glhlyhmR2ofiqx4tqL+rCXtJJqTa5JaLRhTm9s2Y+A2x/WFvUfeMe4Nh0ZB1x3Gr7GGU0Hw/aZK9zBJGMB6HhJa0zq7rVCavXGPAtQaWUDDi8fuHWCztQlhtcaRuQh7toOi9D5bCsQgwuc9p3lG0Zmi1BVjMvoodB1W4wEdQDQPGwY26zBmEcBsTJ6Lx2ixGzVvbzgtQIKq6uHNZmm3de0v3gAQSdSI4dhUBqqthXfbKAGzE6TQuvIBUAe5h56tkOXZYtz7lEE1yvPM74ZZOFlICb8eAIlLjqWMuaUBJQ2+sPaUKL6a+emOuXN50YrqZ5+lmIzLPRC1OhiJ78YL565cybAqnE8IR2l3SIX8AotdF5ZdHGqFjhNDP/0/6rgX6doadut/tUCcvvJYJKS/H2BJJ03OdfJWC5WsBdPp7vK350rYH/4ke4tM377kXcviutQCrwhAWdskcEKm723yN9xOrpI/+HNnieLTw8EI0Va8OEma/Z3gMEkOp/SdvWgEqVpZcaIkCB6VdOaHTSFq9H80WKzjq3PfHdlD8bhbi6R0u/kIpwlOTceLq/jLnV7s56ueeYxlMgSF/7NhufeuFopdYRw42WtkUjofOcJcbYMdnNPPLAffibxvUhIfq4YsPNUFX6lMlD6lmf9Mmv9WA/kdSJh0znneAgZf2XFNKdy9RZW8AqqnabOtA1n/FqTveD8rGTNaWj8kQ==";


            var typeDec = Program.Decrypt(type);
            var objDec = Program.Decrypt(obj);

            Console.WriteLine(objDec);
            Console.ReadKey();
        }
    }
}