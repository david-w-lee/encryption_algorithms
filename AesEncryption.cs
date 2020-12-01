using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace encryption_algorithms
{
    public class AesEncryption
    {
        private AesCryptoServiceProvider aes;
        public AesEncryption(byte[] key, byte[] iv)
        {
            aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.Key = key;
            aes.IV = iv;
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] encrypted;
            ICryptoTransform encryptor = aes.CreateEncryptor();

            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cstream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cstream))
                    {
                        sw.Write(plainText);
                    }
                    encrypted = stream.ToArray();
                }
            }

            return encrypted;
        }

        public string Decrypt(byte[] encrypted)
        {
            string res = null;
            ICryptoTransform decryptor = aes.CreateDecryptor();

            using (MemoryStream stream = new MemoryStream(encrypted))
            {
                using (CryptoStream cstream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cstream))
                    {
                        res = sr.ReadToEnd();
                    }
                }
            }
            
            return res;
        }
    }
}
