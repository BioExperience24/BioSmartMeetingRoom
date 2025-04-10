using System.ComponentModel.DataAnnotations.Schema;

namespace _7.Entities.Models;

public partial class PantryTransaksi : BaseEntity
{
    //public int Generate { get; set; }

    public long PantryId { get; set; }

    public string OrderNo { get; set; } = null!;

    public string EmployeeId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public int IsBlive { get; set; }

    public string RoomId { get; set; } = null!;

    public string Via { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public DateTime OrderDatetime { get; set; }

    public DateTime OrderDatetimeBefore { get; set; }

    public int OrderSt { get; set; }

    public string OrderStName { get; set; } = null!;

    public int Process { get; set; }

    public int Complete { get; set; }

    public int Failed { get; set; }

    public int Done { get; set; }

    public string Note { get; set; } = null!;

    public string NoteReject { get; set; } = null!;

    public string? NoteCanceled { get; set; }

    public int IsRejectedPantry { get; set; }

    public string RejectedBy { get; set; } = null!;

    public DateTime RejectedAt { get; set; }

    public int IsTrashpantry { get; set; }

    public int IsCanceled { get; set; }

    public int IsExpired { get; set; }

    public DateTime ExpiredAt { get; set; }

    public string CanceledBy { get; set; } = null!;

    public DateTime CanceledAt { get; set; }

    public DateTime CompletedAt { get; set; }

    public string CompletedBy { get; set; } = null!;

    public DateTime? ProcessAt { get; set; }

    public string? ProcessBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string CanceledPantryBy { get; set; } = null!;

    public string RejectedPantryBy { get; set; } = null!;

    public string CompletedPantryBy { get; set; } = null!;

    public string? ProcessPantryBy { get; set; }

    public string? Timezone { get; set; }

    public string? FromPantry { get; set; }

    public string? ToPantry { get; set; }

    public int? Pending { get; set; }

    public DateTime? PendingAt { get; set; }

    public string? PackageId { get; set; }

    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }

    [NotMapped]
    public List<string> BookingIds { get; set; } = new();
}

public class PantryEntryResponse
{
    public string Status { get; set; }
    public string Message { get; set; }
    public List<PantryEntryDto> Data { get; set; }
}

public class PantryEntryDto
{
    public int OrderSt { get; set; }
    public int Process { get; set; }
    public int Complete { get; set; }
    public int Failed { get; set; }
    public int IsRejectedPantry { get; set; }
    public string NoteReject { get; set; }
    public long PantryId { get; set; }
    public string? TransaksiId { get; set; }
    public string OrderNo { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeNik { get; set; }
    public string Title { get; set; }
    public string RoomName { get; set; }
    public DateTime? StartBooking { get; set; }
    public DateTime? EndBooking { get; set; }
    public DateTime OrderDatetime { get; set; }
    public DateTime? OrderDatetimeBefore { get; set; }
    public string OrderStName { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string CompletedBy { get; set; }
    public DateTime? ProcessAt { get; set; }
    public string ProcessBy { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string RejectedBy { get; set; }
    public List<DetailPantryDto> Detail { get; set; }
}

public class PantryDetailDto
{
    public string? TransaksiId { get; set; }
    public long? ItemId { get; set; }
    public int Qty { get; set; }
    public string NoteOrder { get; set; }
    public string NoteReject { get; set; }
    public int IsRejected { get; set; }
    public string Name { get; set; }
    public string Prefix { get; set; }
    public string DetailOrder { get; set; }
}

public class PantryDetailResponse
{
    public string Error { get; set; }
    public List<PantryDetailDto> Data { get; set; }
}

public class PantryTransaksiFilter : PantryTransaksi
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}

public class PantryTransaksiSelect : PantryTransaksi
{
    public string? BookingRoomName { get; set; }
    public string? BookingTitle { get; set; }
    public int? BookingIsApprove { get; set; }
    public int? BookingIsCanceled { get; set; }
    public DateOnly? BookingDate { get; set; }
    public DateTime? BookingStart { get; set; }
    public DateTime? BookingEnd { get; set; }
}

public class PantryTransaksiOrderApproval
{
    public string PantryPackageName { get; set; }
    public string? PantryTransaksiId { get; set; }
    public string? BookingId { get; set; }
    public string? RoomId { get; set; }
    public string? EmployeeId { get; set; }
    public string? ApprovedBy { get; set; }
    public string? BookingTitle { get; set; }
    public string? RoomName { get; set; }
    public string? BuildingName { get; set; }
    public string? BuildingFloorName { get; set; }
    public string? EmployeeOrganize { get; set; }
    public string? EmployeeApprove { get; set; }
    public string? RoomImage { get; set; }
    public int? OrderStatus { get; set; }
    public string? OrderStatusName { get; set; }
    public DateOnly? BookingDate { get; set; }
    public DateTime? BookingStart { get; set; }
    public DateTime? BookingEnd { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

