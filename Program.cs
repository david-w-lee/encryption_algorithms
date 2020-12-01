using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace encryption_algorithms
{
    class Program
    {
        private void AesTest()
        {
            byte[] key = new byte[32]; // 256 bits
            byte[] iv = new byte[16];  // 128 bits
            Random rand = new Random();
            rand.NextBytes(key);
            rand.NextBytes(iv);

            AesEncryption aes = new AesEncryption(key, iv);
            string plainText = "Hello World!";

            byte[] encrypted = aes.Encrypt(plainText);
            string decrypted = aes.Decrypt(encrypted);

            Console.WriteLine(decrypted);
        }

        private void Sha256Test()
        {
            SHA256CryptoServiceProvider hashAlgo = new SHA256CryptoServiceProvider();

            string plainText = "Hello World!";
            byte[] bytesValue = System.Text.Encoding.UTF8.GetBytes(plainText);
            byte[] bytesHash = hashAlgo.ComputeHash(bytesValue);

            Console.WriteLine(Convert.ToBase64String(bytesHash));
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.AesTest();
            p.Sha256Test();

            Console.ReadLine();
        }
    }
}
