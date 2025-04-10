using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _5.Helpers.Consumer._Encryption
{
    public class _Aes
    {
        private readonly string _encKey;

        // Constructor to initialize the encryption key
        public _Aes(string encKey)
        {
            _encKey = encKey;
        }

        public string Encrypt(string plainText)
        {
            try
            {
                string key = _encKey; // 32-byte key
                string iv = _encKey.Substring(0, 16); // 16-byte IV

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
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                string encryptedHex = BitConverter.ToString(encryptedBytes).Replace("-", "").ToLower(); // Convert ke HEX
                string base64String = HexToBase64(encryptedHex); // Convert HEX ke Base64
                string customBase64 = ConvertBtoa(base64String); // Custom Base64 Encoding

                return customBase64;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encrypt Error: {ex.Message}");
                return "";
            }
        }

        public string? Decrypt(string cipherText)
        {
            try
            {
                string key = _encKey; // 32-byte key
                string iv = _encKey.Substring(0, 16); // 16-byte IV

                if (key.Length != 32 || iv.Length != 16)
                {
                    throw new Exception("Key and IV must be 32 and 16 characters long, respectively.");
                }

                string base64String = ConvertAtob(cipherText); // Convert Custom Base64 ke normal Base64
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
                return null;
            }
        }

        private string ConvertAtob(string base64Custom)
        {
            try
            {
                // loop for base64Custom
                while (base64Custom.Length % 4 != 0)
                {
                    base64Custom += "=";
                }

                byte[] bytes = Convert.FromBase64String(base64Custom);
                return ApplyRot13(Encoding.UTF8.GetString(bytes));
                // return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConvertAtob Error: {ex.Message}");
                return "";
            }
        }

        private string ConvertBtoa(string input)
        {
            // Apply ROT13
            string rot13String = ApplyRot13(input);

            // Encode to Base64
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(rot13String));
            // return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        private string Base64ToHex(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private string HexToBase64(string hex)
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

        private static string ApplyRot13(string input)
        {
            char[] array = input.ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                char letter = array[i];

                if (letter >= 'a' && letter <= 'z')
                {
                    letter = (char)((letter - 'a' + 13) % 26 + 'a');
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    letter = (char)((letter - 'A' + 13) % 26 + 'A');
                }

                array[i] = letter;
            }

            return new string(array);
        }
    }
}
