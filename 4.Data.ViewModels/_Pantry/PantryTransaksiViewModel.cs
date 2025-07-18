using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class PantryTransaksiViewModel : BaseViewModel
{

    [JsonPropertyName("pantry_id")]
    public long PantryId { get; set; }

    [JsonPropertyName("order_no")]
    public string OrderNo { get; set; } = null!;

    [JsonPropertyName("employee_id")]
    public string EmployeeId { get; set; } = null!;

    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = null!;

    [JsonPropertyName("is_blive")]
    public int IsBlive { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; } = null!;

    [JsonPropertyName("via")]
    public string Via { get; set; } = null!;

    [JsonPropertyName("datetime")]
    public DateTime Datetime { get; set; }

    [JsonPropertyName("order_datetime")]
    public DateTime OrderDatetime { get; set; }

    [JsonPropertyName("order_datetime_before")]
    public DateTime OrderDatetimeBefore { get; set; }

    [JsonPropertyName("order_st")]
    public int OrderSt { get; set; }

    [JsonPropertyName("order_st_name")]
    public string OrderStName { get; set; } = null!;

    [JsonPropertyName("process")]
    public int Process { get; set; }

    [JsonPropertyName("complete")]
    public int Complete { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }

    [JsonPropertyName("done")]
    public int Done { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; } = null!;

    [JsonPropertyName("note_reject")]
    public string NoteReject { get; set; } = null!;

    [JsonPropertyName("note_canceled")]
    public string? NoteCanceled { get; set; }

    [JsonPropertyName("is_rejected_pantry")]
    public int IsRejectedPantry { get; set; }

    [JsonPropertyName("rejected_by")]
    public string RejectedBy { get; set; } = null!;

    [JsonPropertyName("rejected_at")]
    public DateTime RejectedAt { get; set; }

    [JsonPropertyName("is_trashpantry")]
    public int IsTrashpantry { get; set; }

    [JsonPropertyName("is_canceled")]
    public int IsCanceled { get; set; }

    [JsonPropertyName("is_expired")]
    public int IsExpired { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime ExpiredAt { get; set; }

    [JsonPropertyName("canceled_by")]
    public string CanceledBy { get; set; } = null!;

    [JsonPropertyName("canceled_at")]
    public DateTime CanceledAt { get; set; }

    [JsonPropertyName("completed_at")]
    public DateTime CompletedAt { get; set; }

    [JsonPropertyName("completed_by")]
    public string CompletedBy { get; set; } = null!;

    [JsonPropertyName("process_at")]
    public DateTime? ProcessAt { get; set; }

    [JsonPropertyName("process_by")]
    public string? ProcessBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("updated_by")]
    public string UpdatedBy { get; set; } = null!;

    [JsonPropertyName("canceled_pantry_by")]
    public string CanceledPantryBy { get; set; } = null!;

    [JsonPropertyName("rejected_pantry_by")]
    public string RejectedPantryBy { get; set; } = null!;

    [JsonPropertyName("completed_pantry_by")]
    public string CompletedPantryBy { get; set; } = null!;

    [JsonPropertyName("process_pantry_by")]
    public string? ProcessPantryBy { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("from_pantry")]
    public string? FromPantry { get; set; }

    [JsonPropertyName("to_pantry")]
    public string? ToPantry { get; set; }

    [JsonPropertyName("pending")]
    public int? Pending { get; set; }

    [JsonPropertyName("pending_at")]
    public DateTime? PendingAt { get; set; }

    [JsonPropertyName("package_id")]
    public string? PackageId { get; set; }

    [JsonPropertyName("approved_by")]
    public string? ApprovedBy { get; set; }

    [JsonPropertyName("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [JsonPropertyName("approved_head_by")]
    public string? ApprovedHeadBy { get; set; }

    [JsonPropertyName("approved_head_at")]
    public DateTime? ApprovedHeadAt { get; set; }

    [JsonPropertyName("head_employee_id")]
    public string? HeadEmployeeId { get; set; }

    [JsonPropertyName("approval_head")]
    public int ApprovalHead { get; set; }

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
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("order_datetime")]

    public DateTime OrderDatetime { get; set; }

    [JsonPropertyName("order_st")]
    public int OrderSt { get; set; }
}

public class PantryTransactionMobileHistory
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("employee_id")]
    public string EmployeeId { get; set; }

    [JsonPropertyName("order_user")]
    public string OrderUser { get; set; }

    [JsonPropertyName("order_no")]
    public string OrderNo { get; set; }

    [JsonPropertyName("pantry_id")]
    public long PantryId { get; set; }

    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("process")]
    public int Process { get; set; }

    [JsonPropertyName("complete")]
    public int Complete { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }

    [JsonPropertyName("done")]
    public int Done { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("pantry_name")]
    public string PantryName { get; set; }

    [JsonPropertyName("booking_title")]
    public string BookingTitle { get; set; }

    [JsonPropertyName("booking_date")]
    public DateOnly BookingDate { get; set; }

    [JsonPropertyName("booking_start")]
    public DateTime BookingStart { get; set; }

    [JsonPropertyName("booking_end")]
    public DateTime BookingEnd { get; set; }

    [JsonPropertyName("room_name")]
    public string RoomName { get; set; }

    [JsonPropertyName("status_order")]
    public string StatusOrder { get; set; }

    [JsonPropertyName("order_datetime")]
    public DateTime OrderDatetime { get; set; }

    [JsonPropertyName("order_datetime_before")]
    public DateTime OrderDatetimeBefore { get; set; }

    [JsonPropertyName("datetime")]
    public DateTime Datetime { get; set; }

    [JsonPropertyName("count_item")]
    public int CountItem { get; set; }

    [JsonPropertyName("order_user_name")]
    public string OrderUserName { get; set; }

    [JsonPropertyName("rejected_pantry_by_name")]
    public string RejectedPantryByName { get; set; }

    [JsonPropertyName("completed_pantry_by_name")]
    public string CompletedPantryByName { get; set; }

    [JsonPropertyName("process_pantry_by_name")]
    public string ProcessPantryByName { get; set; }
}

public class PantryTransaksiVMNeedApprovalDataTableFR : DataTableViewModel
{
    [FromQuery(Name = "start_date")]
    public DateOnly StartDate { get; set; }

    [FromQuery(Name = "end_date")]
    public DateOnly EndDate { get; set; }

    [FromQuery(Name = "package_id")]
    public string PackageId { get; set; } = string.Empty;

    [FromQuery(Name = "head_employee_id")]
    public string HeadEmployeeId { get; set; } = string.Empty;
}

public class PantryTransaksiVMApporval : PantryTransaksiViewModel
{
    [JsonPropertyName("no")]
    public int No { get; set; }

    [JsonPropertyName("booking_room_name")]
    public string? BookingRoomName { get; set; }
    
    [JsonPropertyName("booking_title")]
    public string? BookingTitle { get; set; }

    [JsonPropertyName("booking_is_approve")]
    public int? BookingIsApprove { get; set; }
    
    [JsonPropertyName("booking_is_canceled")]
    public int? BookingIsCanceled { get; set; }

    [JsonPropertyName("booking_date")]
    public DateOnly? BookingDate { get; set; }

    [JsonPropertyName("booking_start")]
    public DateTime? BookingStart { get; set; }

    [JsonPropertyName("booking_end")]
    public DateTime? BookingEnd { get; set; }
}

public class PantryTransaksiVMProcessApproval
{
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [BindProperty(Name = "approval")]
    public int Approval { get; set; }

    [BindProperty(Name = "note")]
    public string Note { get; set; } = string.Empty;
}

public class PantryTransaksiVMProcessCancel
{
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [BindProperty(Name = "note")]
    public string Note { get; set; } = string.Empty;
}

public class PantryTransaksiVMOrderApproval
{
    [JsonPropertyName("pantry_package_name")]
    public string PantryPackageName { get; set; } = string.Empty;
    [JsonPropertyName("pantry_transaksi_id")]
    public string PantryTransaksiId { get; set; } = string.Empty;

    // [JsonPropertyName("booking_id")]
    // public string BookingId { get; set; } = string.Empty;

    // [JsonPropertyName("room_id")]
    // public string RoomId { get; set; } = string.Empty;

    [JsonPropertyName("booking_title")]
    public string BookingTitle { get; set; } = string.Empty;

    [JsonPropertyName("room_name")]
    public string RoomName { get; set; } = string.Empty;

    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; } = string.Empty;

    [JsonPropertyName("building_floor_name")]
    public string BuildingFloorName { get; set; } = string.Empty;

    [JsonPropertyName("employee_organize")]
    public string EmployeeOrganize { get; set; } = string.Empty;

    [JsonPropertyName("employee_approve")]
    public string EmployeeApprove { get; set; } = string.Empty;
    
    [JsonPropertyName("employee_head")]
    public string EmployeeHead { get; set; } = string.Empty;
    
    [JsonPropertyName("order_detail")]
    public List<PantryDetailVMMenus> OrderDetail { get; set; } = new List<PantryDetailVMMenus>();

    [JsonPropertyName("room_image")]
    public string? RoomImage { get; set; }
    [JsonPropertyName("order_status")]
    public int? OrderStatus { get; set; }
    [JsonPropertyName("order_status_name")]
    public string? OrderStatusName { get; set; }
    [JsonPropertyName("booking_date")]
    public DateOnly? BookingDate { get; set; }
    [JsonPropertyName("booking_start")]
    public DateTime? BookingStart { get; set; }
    [JsonPropertyName("booking_end")]
    public DateTime? BookingEnd { get; set; }
    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }
    [JsonPropertyName("update_at")]
    public DateTime? UpdatedAt { get; set; }
}