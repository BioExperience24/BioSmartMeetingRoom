using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace _5.Helpers.Consumer._Encryption._Secure
{
    public class SecureService(IConfiguration configuration) : ISecureService
    {
        private readonly IConfiguration _configuration = configuration;

        public string Encrypt(string plaintext)
        {
            try
            {
                string key = _configuration["EncryptSetting:SecureServiceKey"] ?? "89665fed99e19cdedd2785d4a1f94cce"; // 32-byte key
                string iv = _configuration["EncryptSetting:SecureServiceIV"] ?? "afb9a11d48e56bc9"; // 16-byte IV

                if (key.Length != 32 || iv.Length != 16)
                {
                    throw new Exception("Key and IV must be 32 and 16 characters long, respectively.");
                }

                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                string encryptedHex = BitConverter.ToString(encryptedBytes).Replace("-", "").ToLower(); // Convert ke HEX
                string base64String = HexToBase64(encryptedHex); // Convert HEX ke Base64
                string customBase64 = ConvertBtoa(base64String); // Custom Base64 Encoding

                return customBase64;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encrypt Error: {ex.Message}");
                return string.Empty;
            }
        }

        public string Decrypt(string ciphertext)
        {
            try
            {
                string key = _configuration["EncryptSetting:SecureServiceKey"] ?? "89665fed99e19cdedd2785d4a1f94cce"; // 32-byte key
                string iv = _configuration["EncryptSetting:SecureServiceIV"] ?? "afb9a11d48e56bc9"; // 16-byte IV

                if (key.Length != 32 || iv.Length != 16)
                {
                    throw new Exception("Key and IV must be 32 and 16 characters long, respectively.");
                }

                string base64String = ConvertAtob(ciphertext); // Convert Custom Base64 ke normal Base64
                string hexString = Base64ToHex(base64String); // Convert Base64 ke HEX
                byte[] encryptedBytes = HexToBytes(hexString); // Convert HEX ke Byte Array

                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decrypt Error: {ex.Message}");
                return string.Empty;
            }
        }

        public string ConvertAtob(string base64Custom)
        {
            try
            {
                // loop for base64Custom
                while (base64Custom.Length % 4 != 0)
                {
                    base64Custom += "=";
                }

                byte[] bytes = Convert.FromBase64String(base64Custom);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConvertAtob Error: {ex.Message}");
                return string.Empty;
            }
        }

        public string ConvertBtoa(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public string Base64ToHex(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public string HexToBase64(string hex)
        {
            byte[] bytes = HexToBytes(hex);
            return Convert.ToBase64String(bytes, Base64FormattingOptions.None);
        }

        private byte[] HexToBytes(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
