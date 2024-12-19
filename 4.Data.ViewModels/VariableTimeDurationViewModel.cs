using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class VariableTimeDurationViewModel : BaseViewModelId
    {
        //public new long Id { get; set; }
        public int? Time { get; set; }
    }

    public class VariableSettingVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; set; }
    }

    public class VariableSettingVMProp
    {
        public required List<VariableTimeDurationVMProp> Duration { get; set; }
        public required List<VariableTimeExtendVMProp> TimeExtend { get; set; }
    }

    public class VariableTimeDurationVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<VariableTimeDurationVMProp>? Data { get; set; }
    }

    public class VariableTimeDurationVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("time")]
        public int? Time { get; set; }
    }

    public class VariableTimeDurationCreateViewModelFR
    {
        [BindProperty(Name = "time")]
        public int? Time { get; set; }
    }

    public class VariableTimeDurationUpdateViewModelFR : VariableTimeDurationCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public long Id { get; set; }
    }

    public class VariableTimeDurationDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }
}