using _4.Data.ViewModels;
using System.Text.Json.Serialization;

public class MenuViewModel : BaseLongViewModel
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("type_icon")]
    public string TypeIcon { get; set; } = null!;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = null!;

    [JsonPropertyName("sort")]
    public int Sort { get; set; }

    [JsonPropertyName("is_child")]
    public int IsChild { get; set; }

    [JsonPropertyName("menu_group_id")]
    public int MenuGroupId { get; set; }

    [JsonPropertyName("module_text")]
    public string ModuleText { get; set; } = null!;

    [JsonPropertyName("created_by")]
    public int? CreatedBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}