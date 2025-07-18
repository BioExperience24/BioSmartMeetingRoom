
namespace _5.Helpers.Consumer._Common
{
    public sealed class _Random
    {
        // private static readonly Random _random = new Random(); // Static Random instance
        private static readonly ThreadLocal<Random> _random = new(() => new Random());

        public static string AlphabetNumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, length)
                                        // .Select(_ => chars[_random.Next(chars.Length)])
                                        .Select(_ => chars[_random.Value!.Next(chars.Length)])
                                        .ToArray());
        }

        public static long Numeric(int length, bool isNoZero = false)
        {
            string chars = !isNoZero ? "0123456789" : "123456789";
            string result;

            do
            {
                result = new string(Enumerable.Range(0, length)
                                            // .Select(_ => chars[_random.Next(chars.Length)])
                                            .Select(_ => chars[_random.Value!.Next(chars.Length)])
                                            .ToArray());
            // } while (result.Length != length || (isNoZero && result.StartsWith("0")));
            } while (result.Length != length || result.StartsWith("0"));

            return long.Parse(result);
        }
    }
}