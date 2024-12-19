using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

public class BaseLongViewModel
{
    [Key]
    [JsonPropertyName("id")]
    [BindProperty(Name = "id")]
    public long? Id { get; set; } = null;

    [JsonPropertyName("is_deleted")]
    [BindProperty(Name = "is_deleted")]
    public int? IsDeleted { get; set; }
}
