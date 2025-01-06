using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

public class LicenseSettingViewModel : BaseViewModel
{
    public string? Serial { get; set; }
    public string? Platform { get; set; }
    public string? DeviceId { get; set; }
    public DateTime? CheckedAt { get; set; }
    public int? Status { get; set; }
    public string? DistributorId { get; set; }
    public string? CustomerId { get; set; }
    public string? Ext { get; set; }
    public string? Webhost { get; set; }
    public string? LicenseType { get; set; }
    public string? Pathdownload { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}

public class LicenseSettingVMResponse
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("data")]
    public List<LicenseSettingVMProp>? Data { get; set; }
}
public class LicenseSettingVMProp
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("serial")]
    public string? Serial { get; set; }

    [JsonPropertyName("platform")]
    public string? Platform { get; set; }

    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }

    [JsonPropertyName("checked_at")]
    public DateTime? CheckedAt { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("distributor_id")]
    public string? DistributorId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("ext")]
    public string? Ext { get; set; }

    [JsonPropertyName("webhost")]
    public string? Webhost { get; set; }

    [JsonPropertyName("license_type")]
    public string? LicenseType { get; set; }

    [JsonPropertyName("pathdownload")]
    public string? Pathdownload { get; set; }

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_by")]
    public string? UpdatedBy { get; set; }
}

public class LicenseSettingCreateViewModelFR
{
    [BindProperty(Name = "serial")]
    public string? Serial { get; set; }

    [BindProperty(Name = "platform")]
    public string? Platform { get; set; }

    [BindProperty(Name = "device_id")]
    public string? DeviceId { get; set; }

    [BindProperty(Name = "status")]
    public int? Status { get; set; }

    [BindProperty(Name = "distributor_id")]
    public string? DistributorId { get; set; }

    [BindProperty(Name = "customer_id")]
    public string? CustomerId { get; set; }

    [BindProperty(Name = "ext")]
    public string? Ext { get; set; }

    [BindProperty(Name = "webhost")]
    public string? Webhost { get; set; }

    [BindProperty(Name = "license_type")]
    public string? LicenseType { get; set; }

    [BindProperty(Name = "pathdownload")]
    public string? Pathdownload { get; set; }
}

public class LicenseSettingUpdateViewModelFR : LicenseSettingCreateViewModelFR
{
    [BindProperty(Name = "updated_at", SupportsGet = false)]
    public DateTime? UpdatedAt { get; set; }

    [BindProperty(Name = "is_deleted", SupportsGet = false)]
    public int? IsDeleted { get; set; }
}

public class LicenseSettingDeleteViewModelFR
{
    [BindProperty(Name = "id")]
    public int Id { get; set; }

    [BindProperty(Name = "serial")]
    public string? Serial { get; set; }
}
