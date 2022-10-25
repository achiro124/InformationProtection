using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace InformationProtection
{
    internal class Crypt
    {
        public static void SaveFile(string path, string key)
        {
            TripleDES cryptoServiceProvider = TripleDES.Create();
            string H = File.ReadAllText(path);
            FileStream FILE = File.Open(path, FileMode.Open);

            byte[] salt = CreateRandomSalt(7);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(key, salt);

            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.KeySize = 192;
            cryptoServiceProvider.Key = passwordDeriveBytes.CryptDeriveKey("TripleDES", "MD4", cryptoServiceProvider.KeySize, cryptoServiceProvider.IV);

            ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor();
            CryptoStream crypto = new CryptoStream(FILE,encryptor,CryptoStreamMode.Write);

            using(StreamWriter sw = new StreamWriter(crypto, Encoding.Unicode))
            {
                sw.Write(H);
                sw.Flush();
            }
            FILE.Close();
        }

        public static string ReadFile(string path, string key)
        {
            TripleDES cryptoServiceProvider = TripleDES.Create();
            FileStream FILE = File.Open(path, FileMode.Open);
            
            //string S = FILE.ToString();
            string plaintext = string.Empty;

            byte[] salt = CreateRandomSalt(7);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(key, salt);

            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.KeySize = 192;
            cryptoServiceProvider.Key = passwordDeriveBytes.CryptDeriveKey("TripleDES", "MD4", cryptoServiceProvider.KeySize, cryptoServiceProvider.IV);

            ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor();
            CryptoStream crypto = new CryptoStream(FILE, decryptor, CryptoStreamMode.Read);

            using (StreamReader sw = new StreamReader(crypto, Encoding.Unicode))
            {
                plaintext = sw.ReadToEnd();
            }
            FILE.Close();
            return plaintext;
        }


        public static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes;
        }
    }
}
