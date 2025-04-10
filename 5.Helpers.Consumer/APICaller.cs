using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace _4.Helpers.Consumer
{
    public class APICaller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _jsonSerializerOptions;

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

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }

        public HttpClient GetHttpClient()
        {
            var client = _clientFactory.CreateClient("MyClient");
            client.Timeout = TimeSpan.FromSeconds(int.Parse(TimeoutLimit));
            return client;
        }

        public async Task<string> TryGETRequest(string urlWebRequest)
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


        public async Task<string> POSTHttpRequest(
            string urlWebRequest,
            object body = null,
            Dictionary<string, string> headers = null,
            string authorization = null, string contentType = "application/json")
        {
            string result = null;
            int countTry = 0;
            bool isSuccess;
            var random = new Random();

            do
            {
                countTry++;
                try
                {
                    result = await TryCallPOST(urlWebRequest, body, authorization, headers, contentType);
                    isSuccess = true;
                }
                catch (Exception)
                {
                    isSuccess = false;
                    int delay = random.Next(100, 1000);
                    await Task.Delay(delay);
                }
            } while (!isSuccess && countTry < 3);

            return result;
        }

        private async Task<string> TryCallPOST(
            string url,
            object body = null, // Can be a JSON object or raw XML string
            string authorization = null,
            Dictionary<string, string> headers = null,
            string contentType = "application/json") // Default to JSON
        {
            var client = _clientFactory.CreateClient("MyClient");

            // Reset headers to avoid old ones persisting
            client.DefaultRequestHeaders.Clear();

            // Set timeout
            client.Timeout = TimeSpan.FromSeconds(int.Parse(TimeoutLimit));

            // Ensure headers dictionary is not null
            headers ??= new Dictionary<string, string>();

            // Add headers
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Add Authorization if provided
            if (!string.IsNullOrEmpty(authorization))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization);
            }

            // Prepare content based on the content type
            StringContent content;
            string rawBody = body?.ToString() ?? "";
            string mediaType = contentType;

            switch (contentType)
            {
                case "application/json":
                    rawBody = body != null ? JsonSerializer.Serialize(body, _jsonSerializerOptions) : "{}";
                    break;
                case "text/xml":
                    rawBody = string.IsNullOrEmpty(rawBody) ? "<root></root>" : rawBody;
                    break;
            }

            content = new StringContent(rawBody, Encoding.UTF8, mediaType);

            // Make the request
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }



        // Helper method to serialize an object to XML
        private string SerializeToXml(object obj)
        {
            if (obj == null) return "";

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using var stringWriter = new StringWriter();
            using var xmlWriter = System.Xml.XmlWriter.Create(stringWriter, new System.Xml.XmlWriterSettings { OmitXmlDeclaration = true });

            xmlSerializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }


    }
}