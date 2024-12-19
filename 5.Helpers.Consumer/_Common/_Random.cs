using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Helpers.Consumer._Common
{
    public sealed class _Random
    {
        public static string AlphabetNumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                                        .Select(_ => chars[random.Next(chars.Length)])
                                        .ToArray());
        }

        public static long Numeric(int length, bool isNoZero = false)
        {
            string chars = !isNoZero ? "0123456789" : "123456789";
            var random = new Random();
            var result = new string(Enumerable.Range(0, length)
                                        .Select(_ => chars[random.Next(chars.Length)])
                                        .ToArray());
            return Convert.ToInt64(result);
        }
    }
}