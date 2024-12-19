

namespace _5.Helpers.Consumer._Encryption
{
    public sealed class _Base64
    {
        public static string Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));

            // Apply ROT13
            string rot13String = ApplyRot13(plainText);

            // Encode to Base64
            string encryptedString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(rot13String));
            return encryptedString;
        }

        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null) throw new ArgumentNullException(nameof(encryptedText));

            // Decode from Base64
            string rot13String = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText));

            // Reverse ROT13
            string plainText = ApplyRot13(rot13String);

            return plainText;
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