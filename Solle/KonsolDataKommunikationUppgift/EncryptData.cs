using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureData
{
    internal static class EncryptData
    {
        public static string Encrypt(string text)
        {
            Console.WriteLine(text);
            try
            {
                using (MemoryStream memoryStream = new())
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key =
                        {
                            0x01, 0x02, 0x03, 0x03, 0x03, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x17
                        };
                        aes.Key = key;

                        aes.GenerateIV();
                        byte[] iv = aes.IV;
                        memoryStream.Write(iv, 0, iv.Length);

                        aes.Padding = PaddingMode.PKCS7;

                        using (CryptoStream cryptoStream = new(
                            memoryStream,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            using (StreamWriter encryptWriter = new(cryptoStream))
                            {
                                encryptWriter.WriteLine(text);
                            }
                        }
                    }
                    Console.WriteLine("The data was encrypted.");

                    byte[] encryptedData = memoryStream.ToArray();
                    string encryptedText = Convert.ToBase64String(encryptedData);

                    return encryptedText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption failed. {ex}");
                return string.Empty;
            }
        }
    }
}