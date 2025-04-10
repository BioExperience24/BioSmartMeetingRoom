using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace _5.Helpers.Consumer._AWS;

public class AwsSecretManagerService
{
    private readonly string _secretName = "pama/smr/dev";
    private readonly RegionEndpoint _region = RegionEndpoint.APSoutheast1;
    private readonly IConfiguration _configuration;

    public AwsSecretManagerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task LoadSecretsAsync()
    {
        using var client = new AmazonSecretsManagerClient(_region);
        try
        {
            var request = new GetSecretValueRequest { SecretId = _secretName };
            var response = await client.GetSecretValueAsync(request);

            if (!string.IsNullOrEmpty(response.SecretString))
            {
                JObject secretObject = JObject.Parse(response.SecretString);

                var secrets = new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection",
                  $"Server={secretObject["DATABASE_HOST"]},{secretObject["DATABASE_PORT"]};" +
                  $"Database={secretObject["DATABASE_NAME"]};" +
                  $"User Id={secretObject["DATABASE_USERNAME"]};" +
                  $"Password={secretObject["DATABASE_PASSWORD"]};" +
                  $"TrustServerCertificate=True;"
                },
                { "AwsSetting:AccessId", secretObject["AWS_ACCESS_ID"]?.ToString() ?? "" },
                { "AwsSetting:SecretKey", secretObject["AWS_SECRET_KEY"]?.ToString() ?? "" }
            };

                var configRoot = _configuration as IConfigurationRoot;
                if (configRoot != null)
                {
                    var memoryConfig = new ConfigurationBuilder()
                        .AddInMemoryCollection(secrets)
                        .Build();

                    foreach (var kvp in secrets)
                    {
                        configRoot[kvp.Key] = kvp.Value;
                    }
                }

                Console.WriteLine($"Successfully loaded secrets.");
            }
            else
            {
                Console.WriteLine($"Secret value is empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Error retrieving secret: {ex.Message}");
        }
    }


}