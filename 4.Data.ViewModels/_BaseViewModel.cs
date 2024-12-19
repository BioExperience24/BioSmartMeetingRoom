using _5.Helpers.Consumer.EnumType;
using System.Net;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;
public class BaseViewModelId
{
    [JsonPropertyName("id")]
    public long? Id { get; set; } = null;

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }
}
public class BaseViewModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; } = null;

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }
}

public class ReturnalModel
{

    [JsonPropertyName("status")]
    public string Status { get; set; } = ReturnalType.Success;

    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    [JsonPropertyName("title")]
    public string Title { get; set; } = ReturnalType.Success;

    [JsonPropertyName("msg")]
    public string Message { get; set; } = ReturnalType.Success;

    [JsonPropertyName("collection")]
    public object? Collection { get; set; }
}
