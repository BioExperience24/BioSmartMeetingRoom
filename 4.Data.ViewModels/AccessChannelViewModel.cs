

using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class AccessChannelViewModel : BaseLongViewModel
    {
        [JsonPropertyName("channel")]
        public int Channel { get; set; }
    }
}