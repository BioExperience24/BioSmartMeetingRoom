using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _5.Helpers.Consumer._Encryption
{
    public class _AesObsolete
    {
        private readonly string _encKey;

        // Constructor to initialize the encryption key
        public _AesObsolete(string encKey)
        {
            _encKey = encKey;
        }

        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_encKey)));
                aes.IV = Encoding.UTF8.GetBytes(_encKey.Substring(0, 16));

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public string? Decrypt(string cipherText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_encKey)));
                    aes.IV = Encoding.UTF8.GetBytes(_encKey.Substring(0, 16));

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
