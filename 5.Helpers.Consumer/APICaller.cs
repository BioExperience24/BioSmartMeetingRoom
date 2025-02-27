using Microsoft.Extensions.Configuration;

namespace _4.Helpers.Consumer
{
    public class APICaller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public string baseurl;
        public string LimitParallel;
        public string TimeoutLimit;

        private static readonly int MaxRetryAttempts = 3; // Maximum retries
        private static readonly TimeSpan InitialDelay = TimeSpan.FromSeconds(2); // Initial delay before retry

        public APICaller(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
            baseurl = _config["App:BaseUrl"] ?? throw new ArgumentNullException(nameof(baseurl));
            TimeoutLimit = _config["App:TimeoutLimit"] ?? "30"; // Default timeout
            LimitParallel = _config["App:LimitParallel"] ?? "5"; // Default limit parallel
        }

        public HttpClient GetHttpClient()
        {
            var client = _clientFactory.CreateClient("MyClient");
            client.Timeout = TimeSpan.FromSeconds(int.Parse(TimeoutLimit));
            return client;
        }

        public async Task<string> TryGETITERequest(string urlWebRequest)
        {
            var url = baseurl + urlWebRequest;
            var client = GetHttpClient();
            int attempt = 0;

            while (attempt < MaxRetryAttempts)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }

                    Console.WriteLine($"Request failed with status code: {response.StatusCode}. Retrying...");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request exception: {ex.Message}. Retrying...");
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Request timed out. Retrying...");
                }

                attempt++;
                if (attempt < MaxRetryAttempts)
                {
                    await Task.Delay(InitialDelay * attempt);
                }
            }

            throw new Exception("API request failed after multiple attempts.");
        }
    }
}
