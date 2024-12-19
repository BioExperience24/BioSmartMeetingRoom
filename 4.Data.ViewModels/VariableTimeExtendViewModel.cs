using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class VariableTimeExtendViewModel : BaseViewModelId
    {
        public new long Id { get; set; }
        public int? Time { get; set; }
    }

    public class VariableTimeExtendVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<VariableTimeExtendVMProp>? Data { get; set; }
    }

    public class VariableTimeExtendVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("time")]
        public int? Time { get; set; }
    }

    public class VariableTimeExtendCreateViewModelFR
    {
        [BindProperty(Name = "time")]
        public int? Time { get; set; }
    }

    public class VariableTimeExtendUpdateViewModelFR : VariableTimeExtendCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public long Id { get; set; }
    }

    public class VariableTimeExtendDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }
}