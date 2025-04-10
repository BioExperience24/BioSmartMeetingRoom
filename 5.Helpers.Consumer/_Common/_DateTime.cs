

namespace _5.Helpers.Consumer._Common
{
    public sealed class _DateTime
    {
        public static List<string> GenerateTimeSlots(DateTime from, DateTime to, TimeSpan interval, string format = "HH:mm")
        {
            var result = new List<string>();

            for (var time = from; time <= to; time = time.Add(interval))
            {
                result.Add(time.ToString(format));
            }

            return result.Distinct().ToList();
        }

        public static DateTime Combine(DateOnly date, TimeOnly time)
        {
            return date.ToDateTime(time);
        }

        public static string Format(DateTime dateTime, string format = "dd MMMM yyyy HH:mm:ss")
        {
            return dateTime != DateTime.MinValue ? dateTime.ToString("dd MMMM yyyy HH:mm:ss") : "";
        }
    }
}