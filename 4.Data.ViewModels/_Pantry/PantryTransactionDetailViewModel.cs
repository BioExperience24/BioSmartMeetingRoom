using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;
public class PantryTransaksiAndMenuViewModel
{
    [JsonPropertyName("item_id")]
    public long? ItemId { get; set; }

    [JsonPropertyName("transaksi_id")]
    public string TransaksiId { get; set; } = null!;

    [JsonPropertyName("menu_id")]
    public int MenuId { get; set; }

    [JsonPropertyName("qty")]
    public int Qty { get; set; }

    [JsonPropertyName("note_order")]
    public string NoteOrder { get; set; } = null!;

    [JsonPropertyName("note_reject")]
    public string NoteReject { get; set; } = null!;

    [JsonPropertyName("detail_order")]
    public string Detailorder { get; set; } = null!;

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("is_rejected")]
    public int IsRejected { get; set; }

    [JsonPropertyName("rejected_by")]
    public string RejectedBy { get; set; } = null!;

    [JsonPropertyName("rejected_at")]
    public DateTime RejectedAt { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!; // menu name

    [JsonPropertyName("detail_order_object")]
    public Dictionary<string, object>? DetailorderObject { get; set; } = null!;
}
