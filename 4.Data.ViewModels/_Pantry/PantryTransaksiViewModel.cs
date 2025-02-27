using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

public class PantryTransaksiViewModel : BaseViewModel
{
    public long pantry_id { get; set; }

    public string order_no { get; set; } = null!;

    public string employee_id { get; set; } = null!;

    public string booking_id { get; set; } = null!;

    public int is_blive { get; set; }

    public string room_id { get; set; } = null!;

    public string via { get; set; } = null!;

    public DateTime datetime { get; set; }

    public DateTime order_datetime { get; set; }

    public DateTime order_datetime_before { get; set; }

    public int order_st { get; set; }

    public string order_st_name { get; set; } = null!;

    public int process { get; set; }

    public int complete { get; set; }

    public int failed { get; set; }

    public int done { get; set; }

    public string note { get; set; } = null!;

    public string note_reject { get; set; } = null!;

    public string? note_canceled { get; set; }

    public int is_rejected_pantry { get; set; }

    public string rejected_by { get; set; } = null!;

    public DateTime rejected_at { get; set; }

    public int is_trashpantry { get; set; }

    public int is_canceled { get; set; }

    public int is_expired { get; set; }

    public DateTime expired_at { get; set; }

    public string canceled_by { get; set; } = null!;

    public DateTime canceled_at { get; set; }

    public DateTime completed_at { get; set; }

    public string completed_by { get; set; } = null!;

    public DateTime? process_at { get; set; }

    public string? process_by { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public string updated_by { get; set; } = null!;

    public string canceled_pantry_by { get; set; } = null!;

    public string rejected_pantry_by { get; set; } = null!;

    public string completed_pantry_by { get; set; } = null!;

    public string? process_pantry_by { get; set; }

    public string? timezone { get; set; }

    public string? from_pantry { get; set; }

    public string? to_pantry { get; set; }

    public int? pending { get; set; }

    public DateTime? pending_at { get; set; }

}

public class PantryTransaksiStatusViewModel
{
    public int id { get; set; }

    public string name { get; set; } = null!;
}

public class PantryTransactionDetail
{
    [JsonPropertyName("id")]
    public string? Id { get; set; } = null!;
    [JsonPropertyName("pantry_id")]
    public long PantryId { get; set; }

    [JsonPropertyName("order_no")]
    public string OrderNo { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("date_booking")]
    public DateOnly? DateBooking { get; set; }

    [JsonPropertyName("room_name")]
    public string RoomName { get; set; }

    [JsonPropertyName("room_location")]
    public string RoomLocation { get; set; }

    [JsonPropertyName("booking_start")]
    public DateTime? BookingStart { get; set; }

    [JsonPropertyName("booking_end")]
    public DateTime? BookingEnd { get; set; }

    [JsonPropertyName("employee_name")]
    public string EmployeeName { get; set; }

    [JsonPropertyName("department_name")]
    public string DepartmentName { get; set; }

    [JsonPropertyName("order_status")]
    public string OrderStatus { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("order_datetime")]

    public DateTime OrderDatetime { get; set; }

    [JsonPropertyName("order_st")]
    public int OrderSt { get; set; }
}
