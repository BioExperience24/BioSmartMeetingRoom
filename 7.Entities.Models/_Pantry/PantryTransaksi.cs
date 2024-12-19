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
}
