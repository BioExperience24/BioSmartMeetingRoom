

namespace _5.Helpers.Consumer._Common
{
    public sealed class _String
    {
        public static string RandomAlNum(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                                        .Select(_ => chars[random.Next(chars.Length)])
                                        .ToArray());
        }

        public static long RandomNumeric(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            var result = new string(Enumerable.Range(0, length)
                                        .Select(_ => chars[random.Next(chars.Length)])
                                        .ToArray());
            return Convert.ToInt64(result);
        }        
    }
}