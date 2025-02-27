using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Helpers.Consumer.Custom
{

    public class FastBookLongEntity {

        public string? Id { get; set; } = null!;
        public int? IsDeleted { get; set; }
    }
    public partial class FastBookBookingInvoice
    {

        [Key]
        public long? Id { get; set; } = null!;
        public int? IsDeleted { get; set; }
        public string? InvoiceGenerateNo { get; set; }

        public string? InvoiceNo { get; set; }

        public string? InvoiceFormat { get; set; }

        public string? BookingId { get; set; }

        public long? RentCost { get; set; }

        public string? Alocation { get; set; }

        public string? MemoNo { get; set; }

        public string? ReferensiNo { get; set; }

        public DateTime? TimeBefore { get; set; }

        public DateTime? TimeSend { get; set; }

        public DateTime? TimePaid { get; set; }

        public string? InvoiceStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public partial class FastBookEmployee : FastBookLongEntity
    {

        // public string Id { get; set; } = null!;

        // public string DivisionId { get; set; } = null!;
        public string DivisionId { get; set; } = string.Empty;

        public string CompanyId { get; set; } = null!;

        public string DepartmentId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Nik { get; set; } = null!;

        public string? NikDisplay { get; set; }

        // public string Photo { get; set; } = null!;
        public string Photo { get; set; } = string.Empty;

        public string Email { get; set; } = null!;

        public string? NoPhone { get; set; }

        public string? NoExt { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string Gender { get; set; } = null!;

        // public string Address { get; set; } = null!;
        public string Address { get; set; } = string.Empty;

        // public string CardNumber { get; set; } = null!;
        public string CardNumber { get; set; } = string.Empty;

        // public string CardNumberReal { get; set; } = null!;
        public string CardNumberReal { get; set; } = string.Empty;

        // public string PasswordMobile { get; set; } = null!;
        public string PasswordMobile { get; set; } = string.Empty;

        // public string GbId { get; set; } = null!;
        public string GbId { get; set; } = string.Empty;

        // public string FrId { get; set; } = null!;
        public string FrId { get; set; } = string.Empty;

        public int Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // public int IsDeleted { get; set; }

        public int? IsVip { get; set; }

        public int? VipApproveBypass { get; set; }

        public int? VipLimitCapBypass { get; set; }

        public int? VipLockRoom { get; set; }
    }

    public partial class FastBookBookingInvitation : FastBookLongEntity
    {
        // public int Id { get; set; }

        public string BookingId { get; set; } = null!;

        public string Nik { get; set; } = null!;

        public int Internal { get; set; }

        public int AttendanceStatus { get; set; }

        public string? AttendanceReason { get; set; }

        public int? ExecuteAttendance { get; set; }

        public int ExecuteDoorAccess { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Company { get; set; } = null!;

        public string? Position { get; set; }

        public short IsPic { get; set; }

        public int? IsVip { get; set; }

        public string? PinRoom { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        // public int IsDeleted { get; set; }

        public string? LastUpdate365 { get; set; }

        public int? Checkin { get; set; }

        public int? EndMeeting { get; set; }
    }

    public partial class FastBookBooking : FastBookLongEntity
    {

        public string BookingId { get; set; } = null!;

        public string? BookingId365 { get; set; }

        public string? BookingIdGoogle { get; set; }

        public string? BookingDevices { get; set; }

        public string? NoOrder { get; set; }

        public string Title { get; set; } = null!;

        public DateOnly Date { get; set; }

        public string? RoomId { get; set; } = null!;

        public string? RoomName { get; set; }

        public short? IsMerge { get; set; }

        public string? MergeRoom { get; set; }

        public string? MergeRoomId { get; set; }

        public string? MergeRoomName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public long? CostTotalBooking { get; set; }

        public int? DurationPerMeeting { get; set; }

        public int? TotalDuration { get; set; }

        public int? ExtendedDuration { get; set; }

        public string Pic { get; set; } = null!;

        public string? AlocationId { get; set; }

        public string? AlocationName { get; set; }

        public string Note { get; set; } = null!;

        public string? CanceledNote { get; set; }

        public string Participants { get; set; } = null!;

        public string? ExternalLink { get; set; }

        public string? ExternalLink365 { get; set; }

        public string? ExternalLinkGoogle { get; set; }

        public int EndEarlyMeeting { get; set; }

        public string? TextEarly { get; set; }

        public int? IsDevice { get; set; }

        public short IsMeal { get; set; }

        public int IsEar { get; set; }

        public int IsRescheduled { get; set; }

        public int IsCanceled { get; set; }

        public int IsExpired { get; set; }

        public string CanceledBy { get; set; } = null!;

        public DateTime CanceledAt { get; set; }

        public string ExpiredBy { get; set; } = null!;

        public DateTime ExpiredAt { get; set; }

        public string RescheduledBy { get; set; } = null!;

        public DateTime RescheduledAt { get; set; }

        public string EarlyEndedBy { get; set; } = null!;

        public DateTime EarlyEndedAt { get; set; }

        public int IsAlive { get; set; }

        public string? Timezone { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public int IsNotifEndMeeting { get; set; }

        public int IsNotifBeforeEndMeeting { get; set; }

        public int? IsAccessTrigger { get; set; }

        public int? IsConfigSettingEnable { get; set; }

        public int? IsEnableApproval { get; set; }

        public int? IsEnablePermission { get; set; }

        public int? IsEnableRecurring { get; set; }

        public int? IsEnableCheckin { get; set; }

        public int? IsRealeaseCheckinTimeout { get; set; }

        public int? IsReleased { get; set; }

        public int IsEnableCheckinCount { get; set; }

        public int? Category { get; set; }

        public string? LastModifiedDateTime365 { get; set; }

        public string? PermissionEnd { get; set; }

        public string? PermissionCheckin { get; set; }

        public int? ReleaseRoomCheckinTime { get; set; }

        public int? CheckinCount { get; set; }

        public int? IsVip { get; set; }

        public int? IsApprove { get; set; }

        public string? VipUser { get; set; }

        public string? UserEndMeeting { get; set; }

        public string? UserCheckin { get; set; }

        public string? UserApproval { get; set; }

        public DateTime? UserApprovalDatetime { get; set; }

        public string? RoomMeetingMove { get; set; }

        public string? RoomMeetingOld { get; set; }

        public int? IsMoved { get; set; }

        public int? IsMovedAgree { get; set; }

        public int? MovedDuration { get; set; }

        public string? MeetingEndNote { get; set; }

        public int? VipApproveBypass { get; set; }

        public int? VipLimitCapBypass { get; set; }

        public int? VipLockRoom { get; set; }

        public string? VipForceMoved { get; set; }

        public int? DurationSavedRelease { get; set; }

        public int? IsCleaningNeed { get; set; }

        public int? CleaningTime { get; set; }

        public DateTime? CleaningStart { get; set; }

        public DateTime? CleaningEnd { get; set; }

        public string? UserCleaning { get; set; }

        public DateTime? ServerDate { get; set; }

        public DateTime? ServerStart { get; set; }

        public DateTime? ServerEnd { get; set; }

        public string BookingType { get; set; } = "general";

        public int IsPrivate { get; set; }

        [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
        public long BuildingId { get; set; }
        [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
        public DateOnly? DateStart { get; set; }
        [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
        public DateOnly? DateEnd { get; set; }
    }

    public class FastBookBookingDataDto : FastBookBooking
    {
        public FastBookBooking Booking { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public string RoomLocation { get; set; }
        public int? RoomCapacity { get; set; }
        public string RoomGoogleMap { get; set; }
        public string BuildingName { get; set; }
        public string BuildingDetailAddress { get; set; }
        public string BuildingGoogleMap { get; set; }
        public decimal Price { get; set; }
        public string FormatTimeStart { get; set; }
        public string FormatTimeEnd { get; set; }
        public string FormatDate { get; set; }
        public string PicEmail { get; set; } = null!;
        public string PicName { get; set; } = null!;
        public string PicNik { get; set; } = null!;
        public int? PicVip { get; set; }

    }


}