using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class CompanyViewModel : BaseViewModel
{
    [BindProperty(Name = "name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; } = string.Empty;

    [BindProperty(Name = "address")]
    [JsonPropertyName("address")]
    public string? Address { get; set; } = string.Empty;

    [BindProperty(Name = "city")]
    [JsonPropertyName("city")]
    public string? City { get; set; } = string.Empty;

    [BindProperty(Name = "state")]
    [JsonPropertyName("state")]
    public string? State { get; set; } = string.Empty;

    [BindProperty(Name = "phone")]
    [JsonPropertyName("phone")]
    public string? Phone { get; set; } = string.Empty;

    [BindProperty(Name = "email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; } = string.Empty;

    [BindProperty(Name = "picture")]
    [JsonPropertyName("picture")]
    public string Picture { get; set; } = string.Empty;

    [BindProperty(Name = "icon")]
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [BindProperty(Name = "logo")]
    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [BindProperty(Name = "menu_bar")]
    [JsonPropertyName("menu_bar")]
    public string? MenuBar { get; set; }

    [BindProperty(Name = "url_address")]
    [JsonPropertyName("url_address")]
    public string? UrlAddress { get; set; }
}

public class CompanyVMMediaFR
{
    // [BindProperty(Name = "type")]
    public string? Type { get; set; }

    // [BindProperty(Name = "file")]
    public IFormFile? File { get; set; }
}