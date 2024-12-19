
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class UserConfigViewModel
    {
        
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("default_password")]
        public string DefaultPassword { get; set; } = string.Empty;

        [JsonPropertyName("password_length")]
        public int PasswordLength { get; set; }
    }
}