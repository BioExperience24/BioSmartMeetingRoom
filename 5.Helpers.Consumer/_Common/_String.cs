

using System.Globalization;

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
    }
}