using System.Security.Cryptography;

namespace TemperatureAPI.Helpers
{
    /// <summary>
    /// Denna klass har en metod för att dekryptera en Base64-kodad textsträng med AES-algoritmen.
    /// Den ursprungliga okrypterade texten returneras om dekryptering lyckades.
    /// </summary>

    public static class DecryptData
    {
        public static string Decrypt(string encryptedText)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                using (Aes aes = Aes.Create())
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x03, 0x03, 0x06, 0x07, 0x08,
                        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x17
                    };
                    aes.Key = key;

                  
                    byte[] iv = new byte[16];
                    memoryStream.Read(iv, 0, iv.Length);

                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (CryptoStream cryptoStream = new CryptoStream(
                        memoryStream,
                        aes.CreateDecryptor(),
                        CryptoStreamMode.Read))
                    using (StreamReader decryptReader = new StreamReader(cryptoStream))
                    {
            
                        string decryptedText = decryptReader.ReadToEnd();
                        return decryptedText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed. {ex}");
                return string.Empty;
            }
        }
    }
}