

using System.Globalization;
using System.Text.RegularExpressions;

namespace _5.Helpers.Consumer._Common
{
    public sealed class _String
    {
        public static DateTime ToDateTime(string dateString, string dateFormat = "yyyy-MM-dd")
        {
            try
            {
                DateTime dateTime = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
                return dateTime;
            }
            catch (FormatException)
            {
                // Console.WriteLine("Format tanggal tidak valid.");
                throw new FormatException("Format tanggal tidak valid.");
            }
            catch (ArgumentNullException)
            {
                // Console.WriteLine("String tanggal tidak boleh null.");
                throw new FormatException("String tanggal tidak boleh null.");
            }
            catch (Exception)
            {
                // Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                throw;
            }
        }

        public static DateOnly ToDateOnly(string dateString, string dateFormat = "yyyy-MM-dd")
        {
            try
            {
                // Menggunakan TryParseExact untuk mencoba mengonversi string ke DateOnly
                if (DateOnly.TryParseExact(dateString, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateOnly date))
                {
                    return date;
                }
                else
                {
                    throw new FormatException("Format tanggal tidak valid.");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static TimeOnly ToTimeOnly(string timeString, string timeFormat = "HH:mm:ss")
        {
            if (string.IsNullOrEmpty(timeString))
            {
                return TimeOnly.MinValue;
            }

            try
            {
                if (TimeOnly.TryParseExact(timeString, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly time))
                {
                    return time;
                }
                else
                {
                    throw new FormatException("Format waktu tidak valid.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static string ToDayName(string dateString, string dateFormat = "yyyy-MM-dd")
        {
            try
            {
                DateTime date;
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    // Get the name of the day
                    string dayName = date.ToString("dddd", CultureInfo.InvariantCulture);
                    return dayName;
                }
                else
                {
                    throw new FormatException("Invalid date format.");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static string RemoveAllSpace(string input)
        {
            string replacement = string.Empty;
            
            Regex sWhitespace = new Regex(@"\s+");

            return sWhitespace.Replace(input, replacement);
        }

        public static string DecodeUnicode(string input)
        {
            return System.Text.RegularExpressions.Regex.Unescape(input);
        }
    }
}