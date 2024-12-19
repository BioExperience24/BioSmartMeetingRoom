using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace _5.Helpers.Consumer._Response;
public class MethodHelperService
{
    // Method untuk menggabungkan dua properti bertipe Dictionary<string, object>
    public Dictionary<string, object> FusionProperties(Dictionary<string, object> properti1, Dictionary<string, object> properti2)
    {
        Dictionary<string, object> propertiGabungan = new Dictionary<string, object>();

        if (properti1 != null)
        {
            // Tambahkan semua elemen dari properti1 ke properti gabungan
            foreach (var kvp in properti1)
            {
                propertiGabungan[kvp.Key] = kvp.Value;
            }
        }

        if (properti2 != null)
        {
            // Tambahkan atau ganti nilai semua elemen dari properti2 ke properti gabungan
            foreach (var kvp in properti2)
            {
                propertiGabungan[kvp.Key] = kvp.Value;
            }
        }


        return propertiGabungan;
    }

    public object GetValObjDy(object obj, string propertyName)
    {
        if (obj != null && obj.GetType() != null && obj.GetType().GetProperty(propertyName) != null)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        return null;
    }

    public string separator()
    {
        var separator = "\\";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            separator = "/";
        }

        return separator;
    }

    public void GrantLinuxPermission(string folderPath)
    {
        var allPathFolder = folderPath.Split(separator());
        var currentPathFolder = "";
        var lastPathFolder = "";
        var first = "";
        foreach (var pathFolder in allPathFolder)
        {
            currentPathFolder += first + pathFolder;
            if (Directory.Exists(currentPathFolder))
            {
                lastPathFolder = currentPathFolder;
            }
            else
            {
                if (lastPathFolder != "")
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Exec("chmod 777 -R " + lastPathFolder);
                    }

                    Directory.CreateDirectory(currentPathFolder);
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Exec("chmod 777 -R " + currentPathFolder);
                    }
                }
            }
            first = separator();
        }
    }

    public async Task<bool> SaveStringToLocalFilePath(string text, string folderName, string fileName)
    {
        GrantLinuxPermission(folderName);
        //var crypto = new AESCrypto();
        //var encryptedText = crypto.Encrypt(text);
        //File.WriteAllBytes(fileName, encryptedText);
        await File.WriteAllTextAsync(@$"{folderName}{separator()}{fileName}", text);
        return true;
    }

    public string Stringify(object value)
    {
        try
        {
            return value == null ? "" : value.ToString();
        }
        catch
        {
            return "";
        }
    }

    public MethodBase GetRealMethodFromAsyncMethod(MethodBase asyncMethod)
    {
        try
        {
            var generatedType = asyncMethod.DeclaringType;
            var originalType = generatedType?.DeclaringType;
            var matchingMethods =
                from methodInfo in originalType?.GetMethods()
                let attr = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>()
                where attr != null && attr.StateMachineType == generatedType
                select methodInfo;

            // If this throws, the async method scanning failed.
            var foundMethod = matchingMethods.Single();
            return foundMethod;
        }
        catch (Exception e)
        {
            return asyncMethod;
        }
    }

    public string ChangeToTitleCase(string value)
    {
        if (value == null)
            return value;
        //throw new ArgumentNullException("value");
        if (value.Length == 0)
            return value;

        value = value.Replace('-', ' ').Replace('_', ' ');

        StringBuilder result = new StringBuilder(value);
        result[0] = char.ToUpper(result[0]);
        for (int i = 1; i < result.Length; ++i)
        {
            if (char.IsWhiteSpace(result[i - 1]))
                result[i] = char.ToUpper(result[i]);
        }

        return result.ToString();
    }

    public IEnumerable<string> SplitByLength(string str, int maxLength)
    {
        int index = 0;
        while (true)
        {
            if (index + maxLength >= str.Length)
            {
                yield return str.Substring(index);
                yield break;
            }
            yield return str.Substring(index, maxLength);
            index += maxLength;
        }
    }

    public string CreateUrlString(string input)
    {
        // Penanganan untuk string yang null atau kosong
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        // Ubah string menjadi lower case
        string lowerCase = input.ToLower();

        // Ganti spasi dengan "-"
        string replaceSpace = lowerCase.Replace(' ', '-');

        // Bersihkan string dari karakter spesial
        string urlFriendly = Regex.Replace(replaceSpace, @"[^0-9a-zA-Z\-]", string.Empty);

        return urlFriendly;
    }

    public string Kamus(string jsonString, string key)
    {
        if (string.IsNullOrEmpty(jsonString))
        {
            return "";
        }
        var objectJson = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
        return Kamus(objectJson, key);
    }

    public string Kamus(Dictionary<string, object> model, string key)
    {
        var result = "";
        if (model == null || string.IsNullOrEmpty(key))
        {
            return "";
        }
        var qStrings = new Dictionary<string, object>(model, StringComparer.OrdinalIgnoreCase);

        if (qStrings.ContainsKey(key) && qStrings[key] != null)
        {
            result = qStrings[key].ToString();
        }

        return result;
    }

    public string KamusLike(Dictionary<string, object> model, string key)
    {
        var result = "";
        if (model == null || string.IsNullOrEmpty(key))
        {
            return "";
        }
        var qStrings = new Dictionary<string, object>(model, StringComparer.OrdinalIgnoreCase);
        foreach (var item in qStrings)
        {
            if (item.Key.Contains(key))
            {
                result = item.Value.ToString();
            }
        }

        return result;
    }

    public object GetObjectFromDictionary(Dictionary<string, object> dicObj, string key)
    {
        object valueToNewRecord;
        var detail = string.Empty;
        if (key.Contains("."))
        {
            var split = key.Split(".");
            key = split.First();
            detail = split.Last().Trim();
        }
        if (dicObj.ContainsKey(key))
        {
            valueToNewRecord = dicObj[key];
            DateTime dateTime;
            if (!string.IsNullOrEmpty(detail) && DateTime.TryParse(valueToNewRecord.ToString(), out dateTime))
            {
                if (detail == "year")
                {
                    valueToNewRecord = (decimal)dateTime.Year;
                }
                else if (detail == "month")
                {
                    valueToNewRecord = (decimal)dateTime.Month;
                }
                else if (detail == "day")
                {
                    valueToNewRecord = (decimal)dateTime.Day;
                }
            }
        }
        else
        {
            // Handle kasus ketika indeks item.id tidak ada di result.ObjectRecord
            valueToNewRecord = null; // Atau Anda dapat memberikan nilai default lain sesuai kebutuhan
        }

        return valueToNewRecord;
    }

    public byte[] Decode(byte[] bytes)
    {
        if (bytes.Length == 0) return bytes;
        var i = bytes.Length - 1;
        while (bytes[i] == 0 || bytes[i] == 144)
        {
            i--;
        }
        byte[] copy = new byte[i + 1];
        Array.Copy(bytes, copy, i + 1);
        return copy;
    }

    public string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public byte[] ExtractByte(string base64)
    {
        var cleanbase64 = base64.Trim('\'').Replace("data:image/png;base64,", string.Empty);
        cleanbase64 = cleanbase64.Replace("data:image/jpg;base64,", string.Empty);
        cleanbase64 = cleanbase64.Replace("data:image/jpeg;base64,", string.Empty);

        byte[] ImageData = Convert.FromBase64String(cleanbase64);

        return ImageData;
    }

    public string GenerateRandomString(int length, bool isSpecial = true)
    {
        const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
        const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
        const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMERIC_CHARACTERS = "0123456789";
        string SPECIAL_CHARACTERS = @"!#$%&*@\";
        if (!isSpecial)
        {
            SPECIAL_CHARACTERS = "";
        }

        string characterSet =
            LOWERCASE_CHARACTERS + UPPERCASE_CHARACTERS +
            NUMERIC_CHARACTERS + SPECIAL_CHARACTERS;

        char[] password = new char[length];
        int characterSetLength = characterSet.Length;

        Random random = new Random();
        for (int characterPosition = 0; characterPosition < length; characterPosition++)
        {
            password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

            bool moreThanTwoIdenticalInARow =
                characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                && password[characterPosition] == password[characterPosition - 1]
                && password[characterPosition - 1] == password[characterPosition - 2];

            if (moreThanTwoIdenticalInARow)
            {
                characterPosition--;
            }
        }

        return string.Join(null, password);
    }

    public string CalculationSharedRack(string[] input)
    {
        string result = "";
        if (input.Length > 0)
        {
            var dicSharedRack = new Dictionary<string, int>();
            foreach (var item in input)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                foreach (var perChar in item.Trim().Split(" "))
                {
                    var noRack = Regex.Match(perChar, @"\d+").Value;
                    if (string.IsNullOrEmpty(noRack))
                    {
                        continue;
                    }
                    var codeRack = perChar.Replace(noRack, string.Empty);
                    if (!dicSharedRack.ContainsKey(codeRack))
                    {
                        dicSharedRack.Add(codeRack, 0);
                    }
                    var numb = int.Parse(noRack);
                    dicSharedRack[codeRack] += numb;
                }
            }

            foreach (var count in dicSharedRack)
            {
                if (count.Value > 0)
                {
                    result += " " + count.Value + count.Key;
                }
            }
        }

        if (string.IsNullOrEmpty(result))
        {
            result = "0R";
        }

        return result;
    }

    public async Task IFormFileToPhysical(IFormFile uploadedFile, string folder, string fileName)
    {
        var path = Path.Combine(
            folder, fileName);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Exec("chmod 777 -R " + path);
        }

        using var stream = new FileStream(path, FileMode.Create);
        await uploadedFile.CopyToAsync(stream);
    }

    public async Task IByteToPhysical(byte[] uploadedFile, string folder, string fileName)
    {
        var path = Path.Combine(
            folder, fileName);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Exec("chmod 777 -R " + path);
        }

        await File.WriteAllBytesAsync(path, uploadedFile);
    }

    public async Task IDeleteFile(string folder, string fileName)
    {
        var path = Path.Combine(
            folder, fileName);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Exec("chmod 777 -R " + path);
        }

        await Task.Run(() => { File.Delete(path); });
    }

    public async Task<MemoryStream> GetMemoryStreamFile(string path)
    {
        MemoryStream dataStream = new();

        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(dataStream);
        }
        dataStream.Position = 0;

        return dataStream;
    }

    public async Task<MemoryStream> GetMemoryStreamFile(string folder, string fileName)
    {
        MemoryStream dataStream = new MemoryStream();

        var path = Path.Combine(folder, fileName);

        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(dataStream);
        }
        dataStream.Position = 0;

        return dataStream;
    }

    public async Task<bool> CopyFileToRoot(string sourceX, string destinationX, string fileName)
    {
        var separator = "\\";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            separator = "/";
        }
        var source = sourceX + separator + fileName;
        var destination = destinationX + separator + fileName;

        try
        {
            GrantLinuxPermission(destinationX);

            if (File.Exists(source))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Exec("chmod 777 -R " + source);
                    Exec("chmod 777 -R " + destination);
                }

                File.Copy(source, destination, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return true;
    }

    public void Exec(string cmd)
    {
        var escapedArgs = cmd.Replace("\\", "/");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\""
            }
        };

        process.Start();
        process.WaitForExit();
    }

    public string ChangeSpearator(string locationExcel)
    {
        return locationExcel.Replace("\\", separator()).Replace("/", separator());
    }

    public decimal? ParseToDecimalOrDefault(object value)
    {
        if (value != null && decimal.TryParse(value.ToString(), out decimal result))
        {
            return result;
        }
        return null;
    }

    public int? ParseToIntOrDefault(object value)
    {
        if (value != null && int.TryParse(value.ToString(), out int result))
        {
            return result;
        }
        return null;
    }

    public bool IsEmailValid(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public int ExtractNumberFromString(string input)
    {
        string currentNumber = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                currentNumber += c;
            }
            else if (!string.IsNullOrEmpty(currentNumber))
            {
                return int.Parse(currentNumber);
            }
        }

        if (!string.IsNullOrEmpty(currentNumber))
        {
            return int.Parse(currentNumber);
        }

        return 0; // Nilai default jika tidak ditemukan angka
    }
}
