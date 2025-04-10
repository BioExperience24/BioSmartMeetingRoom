using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using WorkerAPICaller.Repository;

namespace WorkerAPICaller;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private Dictionary<string, string> _configList = [];
    private List<SchedulerMaster> _schedulerMaster = [];

    private string lastFileLog = "";

    private async Task LoadConfigFromDatabase()
    {
        using var scope = serviceScopeFactory.CreateScope();  // ? Membuat Scoped DbContext
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        try
        {
            var allManualConfig = await dbContext.ManualConfigs.ToListAsync();
            _configList = allManualConfig.ToDictionary(
                c => c.ConfigName ?? string.Empty,
                c => c.ConfigValue ?? string.Empty
            );

            _schedulerMaster = await dbContext.SchedulerMasters
                .Include(m => m.Endpoints) // Load list endpoint
                .ToListAsync();

            logger.LogInformation("Loaded {count} configurations from DB.", allManualConfig.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error loading config from DB.");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Ambil data dari database saat pertama kali worker berjalan
        await LoadConfigFromDatabase();

        await WriteLogToFile("SYSTEM WORKER STARTTING", "error");//ditulis error type biar selalu ditulis


        // Hitung selisih waktu antara waktu pertama looping dan waktu sekarang.

        if (!_configList.TryGetValue("FirstLoopTime", out var firstLoopTimeStr) || !DateTime.TryParse(firstLoopTimeStr, out var firstLoopTime))
        {
            firstLoopTime = DateTime.Now;
        }

        var loopingSeconds = int.Parse(_configList["LoopingSeconds"]);

        TimeSpan timeUntilFirstLoop = firstLoopTime - DateTime.Now;
        // Jika selisih waktu negatif, maka atur waktu pertama looping menjadi waktu sekarang + interval waktu looping.
        if (timeUntilFirstLoop < TimeSpan.Zero)
        {
            do
            {
                firstLoopTime = firstLoopTime.AddSeconds(loopingSeconds);
            } while (firstLoopTime <= DateTime.Now);

            timeUntilFirstLoop = firstLoopTime - DateTime.Now;
        }

        var nextLoop = firstLoopTime;

        await WriteLogToFile($"First Looping Start at {nextLoop}, will be waiting {timeUntilFirstLoop} Timespan", "info");
        // Tunggu hingga waktu pertama looping.
        await Task.Delay(timeUntilFirstLoop, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            var listTask = new List<Task>();
            foreach (var master in _schedulerMaster)
            {
                if (master.Token == null && !string.IsNullOrEmpty(master.Username))
                {
                    master.Token = await GetAuthToken(master);
                }

                foreach (var endpoint in master.Endpoints)
                {
                    listTask.Add(RunningTask(master, endpoint));
                }
            }
            //var URL_API = "";
            //var task = TaskAPICall(URL_API, nextLoop, timeoutMinutes);

            //next run
            nextLoop = nextLoop.AddSeconds(loopingSeconds);

            // Hitung selisih waktu antara waktu next looping dan waktu sekarang.
            TimeSpan timeUntilNextLoop = nextLoop - DateTime.Now;

            // Tunggu hingga waktu selanjutnya untuk panggilan API.
            await Task.Delay(timeUntilNextLoop, stoppingToken);
            await Task.WhenAll(listTask);
        }//infinite looping
    }

    private async Task<string> GetAuthToken(SchedulerMaster master)
    {
        var APIAuth = master.BaseUrl + master.LoginPath;
        if (string.IsNullOrEmpty(master.Username))
        {
            return "";
        }

        var data = new
        {
            username = master.Username,
            password = master.Password
        };
        var jsonData = JsonSerializer.Serialize(data);
        var jsonResponse = await TaskAPICallPost(APIAuth, jsonData);
        string tokenValue = GetValueFromJson(jsonResponse, master.TokenProperty!);

        return tokenValue;
    }

    static string GetValueFromJson(string json, string propertyPath)
    {
        try
        {
            var jsonObject = JObject.Parse(json);
            return jsonObject.SelectToken(propertyPath)?.ToString() ?? "Property not found";
        }
        catch (Exception ex)
        {
            return $"Error parsing JSON: {ex.Message}";
        }
    }

    private async Task<string> RunningTask(SchedulerMaster master, SchedulerEndpoint endpoint)
    {
        var loopingSeconds = int.Parse(_configList["LoopingSeconds"]);

        var timespan = endpoint.NextRun - DateTime.Now;
        if (timespan < TimeSpan.Zero)
        {
            do
            {
                endpoint.NextRun = CalculateNextRun(endpoint);
            } while (endpoint.NextRun <= DateTime.Now);

            timespan = endpoint.NextRun - DateTime.Now;
        }

        if (timespan.TotalSeconds > loopingSeconds)
        {
            //skipp, blom waktunyee
            return "skip";
        }

        endpoint.Status = "Waiting";
        endpoint.LastRun = DateTime.Now;
        await UpdateSchedulerEndpoint(endpoint);//updatee next run

        //delay sampe next run tiba
        await Task.Delay((int)timespan.TotalMilliseconds);

        //waktunya panggil API
        endpoint.Status = "Running";
        endpoint.LastRun = DateTime.Now;
        await UpdateSchedulerEndpoint(endpoint);//updatee next run

        var res = await TaskAPICall(master.BaseUrl + endpoint.Path, master);

        endpoint.Status = "Active";
        endpoint.LastRun = DateTime.Now;
        endpoint.NextRun = CalculateNextRun(endpoint);
        await UpdateSchedulerEndpoint(endpoint);//updatee next run

        return res;
    }

    private static DateTimeOffset CalculateNextRun(SchedulerEndpoint item)
    {
        var nextTimespan = item.IntervalLooping;
        var timespan = TimeSpan.FromSeconds(nextTimespan);
        return item.NextRun.Add(timespan);
    }


    private async Task<string> TaskAPICall(string URL_API, SchedulerMaster master)
    {
        var TimeoutMinutes = int.Parse(_configList["TimeoutMinutes"]);
        var res = "";
        await WriteLogToFile($"Calling API {URL_API}", "info");
        try
        {
            using HttpClient httpClient = new();
            httpClient.Timeout = TimeSpan.FromMinutes(TimeoutMinutes);

            // Tambahkan token ke header Authorization
            if (!string.IsNullOrEmpty(master.Token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", master.Token);
            }

            // Coba panggil API pertama kali
            HttpResponseMessage response = await httpClient.GetAsync(URL_API);

            // Jika gagal karena Unauthorized (401), refresh token dan coba lagi sekali
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await WriteLogToFile("Token expired, refreshing token...", "info");

                // Ambil token baru
                master.Token ??= await GetAuthToken(master);

                // Set token baru ke header
                if (!string.IsNullOrEmpty(master.Token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", master.Token);
                    response = await httpClient.GetAsync(URL_API); // Coba request ulang
                }
            }

            // Jika berhasil, ambil hasilnya
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsStringAsync();
            }
            else
            {
                await WriteLogToFile($"Request failed. {URL_API} Status Code: {(int)response.StatusCode}, Reason: {response.ReasonPhrase}", "error");
            }
        }
        catch (Exception ex)
        {
            await WriteLogToFile($"{URL_API} Exception: {ex.Message}, Stack Trace: {ex.StackTrace}", "error");
        }

        await WriteLogToFile($"Result API {URL_API} is\n{res}", "info");
        return res;
    }


    private async Task<string> TaskAPICallPost(string URL_API, string jsonData)
    {
        await WriteLogToFile($"Calling API {URL_API}", "info");
        var TimeoutMinutes = int.Parse(_configList["TimeoutMinutes"]);
        var res = "";
        try
        {
            using HttpClient httpClient = new();
            httpClient.Timeout = TimeSpan.FromMinutes(TimeoutMinutes);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Buat konten JSON dari parameter
            using StringContent content = new(jsonData, Encoding.UTF8, "application/json");

            // Mengirim permintaan POST ke API
            HttpResponseMessage response = await httpClient.PostAsync(URL_API, content);

            // Periksa apakah permintaan berhasil atau tidak
            if (response.IsSuccessStatusCode)
            {
                // Ambil isi konten sebagai string
                res = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            await WriteLogToFile($"Terjadi kesalahan: {ex.Message}, Stack Trace: {ex.StackTrace}", "error");
        }

        await WriteLogToFile($"Result API {URL_API} is\n{res}", "info");
        return res;
    }


    private async Task WriteLogToFile(string message, string logLevel)
    {
        var dbLogLevel = _configList["LogLevel"];
        if (dbLogLevel == "error" && logLevel != "error")
            return;

        var pathLogFile = _configList["PathLogFile"];
        if (pathLogFile == "DB")
        {
            await InsertSchedulerLog(logLevel, message);
            return;
        }
        else if (pathLogFile != null)//path file
        {
            try
            {
                var fileLog = $"{DateTime.Today:yyyy-MM-dd}_log.txt";
                if (fileLog != lastFileLog)
                {
                    var pathLog = PrepFolderAndFile(pathLogFile, fileLog);
                    lastFileLog = fileLog;
                }

                var path = Path.Combine(pathLogFile, fileLog);
                using var writer = new StreamWriter(path, true);
                await writer.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menulis log ke file: {ex.Message}");
            }
        }
        else
        {
            logger.LogInformation(message);
        }
    }

    private async Task InsertSchedulerLog(string status, string? message = null)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();  // ? Membuat Scoped DbContext
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var log = new SchedulerLog
            {
                ExecutionTime = DateTimeOffset.UtcNow,
                Status = status,
                Message = message,
                CreatedDate = DateTimeOffset.UtcNow
            };

            dbContext.SchedulerLogs.Add(log);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menulis log ke DB: {ex.Message}");
        }
    }

    private async Task UpdateSchedulerEndpoint(SchedulerEndpoint model)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();  // ? Membuat Scoped DbContext
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var entiti = await dbContext.SchedulerEndpoints.FindAsync(model.Id);
            if (entiti == null)
            {
                return;
            }

            entiti.NextRun = model.NextRun;
            entiti.LastRun = model.LastRun;
            entiti.Status = model.Status;
            entiti.ModifiedBy = "System";
            entiti.ModifiedDate = DateTimeOffset.UtcNow;

            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menulis log ke DB: {ex.Message}");
        }
    }

    public static string PrepFolderAndFile(string folder, string fileName)
    {
        var path = Path.Combine(
            folder, fileName);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Exec("chmod 777 -R " + path);
        }
        return path;
    }

    public static void Exec(string cmd)
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
}