using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class AccessControllerTypeViewModel : BaseViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}