using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using _8.PAMA.Scheduler.ViewModel;
using Microsoft.Extensions.Logging;

public class EntrypassService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<EntrypassService> _logger;

    private string? _tokenEntrypass;
    private DateTimeOffset? _tokenExpiredEntrypass;

    public EntrypassService(HttpClient httpClient, IConfiguration config, ILogger<EntrypassService> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }

    public async Task<T?> SendPostAsync<T>(string url, object payload, Dictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };

        if (headers != null)
        {
            foreach (var header in headers)
            {
                if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(header.Value);
                }
                else if (header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
                {
                    request.Headers.Authorization = AuthenticationHeaderValue.Parse(header.Value);
                }
                else
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        try
        {
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Entrypass SendPostAsync failed: {StatusCode} {Reason}", response.StatusCode, response.ReasonPhrase);
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during SendPostAsync to Entrypass");
            return default;
        }
    }

    public async Task EnsureTokenAsync()
    {
        var now = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7));


        if (_tokenExpiredEntrypass != null)
        {
            var minutesRemaining = (_tokenExpiredEntrypass.Value - now).TotalMinutes;
            if (minutesRemaining > 10)
            {
                // Token masih valid, tidak perlu refresh
                // return;
            }
        }

        var requestUri = $"{_config["Entrypass:UrlApi"]}{_config["EntryPass:PathSign"]}";
        var payload = new { key = _config["Entrypass:ApiKey"] };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync(requestUri, content);
        }
        catch (Exception ex)
        {
            _tokenEntrypass = null;
            _tokenExpiredEntrypass = null;
            _logger.LogError(ex, "Token request failed: Entrypass services not active at {Time}", now);
            return;
        }

        if (!response.IsSuccessStatusCode)
        {
            _tokenEntrypass = null;
            _tokenExpiredEntrypass = null;
            _logger.LogWarning("Token request returned non-success status code {StatusCode} at {Time}", response.StatusCode, now);
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<EntryPassResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (result == null || result.Status != "success" || result.Data?.Token == null)
        {
            _tokenEntrypass = null;
            _tokenExpiredEntrypass = null;
            _logger.LogWarning("Token response failed: {Msg} {Error} at {Time}", result?.Msg, result?.Error, now);
            return;
        }

        _tokenEntrypass = result.Data.Token;
        _tokenExpiredEntrypass = result.Data.ExpiredIn;

        _logger.LogInformation("Sign success, token expired in {ExpireTime}", _tokenExpiredEntrypass.Value.ToString("dd MMMM yyyy HH:mm:ss"));
    }

    public string? GetToken() => _tokenEntrypass;
}
