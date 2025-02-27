namespace _4.Data.ViewModels;

using System.Text.Json.Serialization;

public class PantryTransaksiDViewModel : BaseLongViewModel
{
    // public int Id { get; set; }

    [JsonPropertyName("transaksi_id")]
    public string TransaksiId { get; set; } = null!;

    [JsonPropertyName("menu_id")]
    public long? MenuId { get; set; }

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

    // public int IsDeleted { get; set; }
}