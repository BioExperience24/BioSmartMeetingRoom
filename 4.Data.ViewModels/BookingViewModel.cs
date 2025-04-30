using System.Text.Json.Serialization;
using _5.Helpers.Consumer;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer.Custom;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class BookingViewModel : BaseLongViewModel
{
    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [JsonPropertyName("booking_id_365")]
    public string BookingId365 { get; set; } = string.Empty;

    [JsonPropertyName("booking_id_google")]
    public string BookingIdGoogle { get; set; } = string.Empty;

    [JsonPropertyName("booking_devices")]
    public string BookingDevices { get; set; } = string.Empty;

    [JsonPropertyName("no_order")]
    public string NoOrder { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; } = string.Empty;

    [JsonPropertyName("room_name")]
    public string RoomName { get; set; } = string.Empty;

    [JsonPropertyName("is_merge")]
    public short IsMerge { get; set; }

    [JsonPropertyName("merge_room")]
    public string MergeRoom { get; set; } = string.Empty;

    [JsonPropertyName("merge_room_id")]
    public string MergeRoomId { get; set; } = string.Empty;

    [JsonPropertyName("merge_room_name")]
    public string MergeRoomName { get; set; } = string.Empty;

    [JsonPropertyName("start")]
    public DateTime Start { get; set; }

    [JsonPropertyName("end")]
    public DateTime End { get; set; }

    [JsonPropertyName("cost_total_booking")]
    public long CostTotalBooking { get; set; }

    [JsonPropertyName("duration_per_meeting")]
    public int DurationPerMeeting { get; set; }

    [JsonPropertyName("total_duration")]
    public int TotalDuration { get; set; }

    [JsonPropertyName("extended_duration")]
    public int ExtendedDuration { get; set; }

    [JsonPropertyName("pic")]
    public string Pic { get; set; } = string.Empty;

    [JsonPropertyName("alocation_id")]
    public string AlocationId { get; set; } = string.Empty;

    [JsonPropertyName("alocation_name")]
    public string AlocationName { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("canceled_note")]
    public string CanceledNote { get; set; } = string.Empty;

    [JsonPropertyName("participants")]
    public string Participants { get; set; } = string.Empty;

    [JsonPropertyName("external_link")]
    public string ExternalLink { get; set; } = string.Empty;

    [JsonPropertyName("external_link_365")]
    public string ExternalLink365 { get; set; } = string.Empty;

    [JsonPropertyName("external_link_google")]
    public string ExternalLinkGoogle { get; set; } = string.Empty;

    [JsonPropertyName("end_early_meeting")]
    public int EndEarlyMeeting { get; set; }

    [JsonPropertyName("text_early")]
    public string TextEarly { get; set; } = string.Empty;

    [JsonPropertyName("is_device")]
    public int IsDevice { get; set; }

    [JsonPropertyName("is_meal")]
    public short IsMeal { get; set; }

    [JsonPropertyName("is_ear")]
    public int IsEar { get; set; }

    [JsonPropertyName("is_rescheduled")]
    public int IsRescheduled { get; set; }

    [JsonPropertyName("is_canceled")]
    public int IsCanceled { get; set; }

    [JsonPropertyName("is_expired")]
    public int IsExpired { get; set; }

    [JsonPropertyName("canceled_by")]
    public string CanceledBy { get; set; } = string.Empty;

    [JsonPropertyName("canceled_at")]
    public DateTime CanceledAt { get; set; }

    [JsonPropertyName("expired_by")]
    public string ExpiredBy { get; set; } = string.Empty;

    [JsonPropertyName("expired_at")]
    public DateTime ExpiredAt { get; set; }

    [JsonPropertyName("rescheduled_by")]
    public string RescheduledBy { get; set; } = string.Empty;

    [JsonPropertyName("rescheduled_at")]
    public DateTime RescheduledAt { get; set; }

    [JsonPropertyName("early_ended_by")]
    public string EarlyEndedBy { get; set; } = string.Empty;

    [JsonPropertyName("early_ended_at")]
    public DateTime EarlyEndedAt { get; set; }

    [JsonPropertyName("is_alive")]
    public int IsAlive { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("updated_by")]
    public string UpdatedBy { get; set; } = string.Empty;

    [JsonPropertyName("is_notif_end_meeting")]
    public int IsNotifEndMeeting { get; set; }

    [JsonPropertyName("is_notif_before_end_meeting")]
    public int IsNotifBeforeEndMeeting { get; set; }

    [JsonPropertyName("is_access_trigger")]
    public int IsAccessTrigger { get; set; }

    [JsonPropertyName("is_config_setting_enable")]
    public int IsConfigSettingEnable { get; set; }

    [JsonPropertyName("is_enable_approval")]
    public int IsEnableApproval { get; set; }

    [JsonPropertyName("is_enable_permission")]
    public int IsEnablePermission { get; set; }

    [JsonPropertyName("is_enable_recurring")]
    public int IsEnableRecurring { get; set; }

    [JsonPropertyName("is_enable_checkin")]
    public int IsEnableCheckin { get; set; }

    [JsonPropertyName("is_realease_checkin_timeout")]
    public int IsRealeaseCheckinTimeout { get; set; }

    [JsonPropertyName("is_released")]
    public int IsReleased { get; set; }

    [JsonPropertyName("is_enable_checkin_count")]
    public int IsEnableCheckinCount { get; set; }

    [JsonPropertyName("category")]
    public int Category { get; set; }

    [JsonPropertyName("last_modified_date_time_365")]
    public string LastModifiedDateTime365 { get; set; } = string.Empty;

    [JsonPropertyName("permission_end")]
    public string PermissionEnd { get; set; } = string.Empty;

    [JsonPropertyName("permission_checkin")]
    public string PermissionCheckin { get; set; } = string.Empty;

    [JsonPropertyName("release_room_checkin_time")]
    public int ReleaseRoomCheckinTime { get; set; }

    [JsonPropertyName("checkin_count")]
    public int CheckinCount { get; set; }

    [JsonPropertyName("is_vip")]
    public int IsVip { get; set; }

    [JsonPropertyName("is_approve")]
    public int IsApprove { get; set; }

    [JsonPropertyName("vip_user")]
    public string VipUser { get; set; } = string.Empty;

    [JsonPropertyName("user_end_meeting")]
    public string UserEndMeeting { get; set; } = string.Empty;

    [JsonPropertyName("user_checkin")]
    public string UserCheckin { get; set; } = string.Empty;

    [JsonPropertyName("user_approval")]
    public string UserApproval { get; set; } = string.Empty;

    [JsonPropertyName("user_approval_datetime")]
    public DateTime? UserApprovalDatetime { get; set; }

    [JsonPropertyName("room_meeting_move")]
    public string RoomMeetingMove { get; set; } = string.Empty;

    [JsonPropertyName("room_meeting_old")]
    public string RoomMeetingOld { get; set; } = string.Empty;

    [JsonPropertyName("is_moved")]
    public int IsMoved { get; set; }

    [JsonPropertyName("is_moved_agree")]
    public int IsMovedAgree { get; set; }

    [JsonPropertyName("moved_duration")]
    public int MovedDuration { get; set; }

    [JsonPropertyName("meeting_end_note")]
    public string MeetingEndNote { get; set; } = string.Empty;

    [JsonPropertyName("vip_approve_bypass")]
    public int VipApproveBypass { get; set; }

    [JsonPropertyName("vip_limit_cap_bypass")]
    public int VipLimitCapBypass { get; set; }

    [JsonPropertyName("vip_lock_room")]
    public int VipLockRoom { get; set; }

    [JsonPropertyName("vip_force_moved")]
    public string VipForceMoved { get; set; } = string.Empty;

    [JsonPropertyName("duration_saved_release")]
    public int DurationSavedRelease { get; set; }

    [JsonPropertyName("is_cleaning_need")]
    public int IsCleaningNeed { get; set; }

    [JsonPropertyName("cleaning_time")]
    public int CleaningTime { get; set; }

    [JsonPropertyName("cleaning_start")]
    public DateTime? CleaningStart { get; set; }

    [JsonPropertyName("cleaning_end")]
    public DateTime? CleaningEnd { get; set; }

    [JsonPropertyName("user_cleaning")]
    public string UserCleaning { get; set; } = string.Empty;

    [JsonPropertyName("server_date")]
    public DateTime? ServerDate { get; set; }

    [JsonPropertyName("server_start")]
    public DateTime? ServerStart { get; set; }

    [JsonPropertyName("server_end")]
    public DateTime? ServerEnd { get; set; }

    [JsonPropertyName("booking_type")]
    public string BookingType { get; set; } = string.Empty;

    [JsonPropertyName("is_private")]
    public int IsPrivate { get; set; }

    [JsonPropertyName("recurring_id")]
    public string RecurringId { get; set; } = string.Empty;
    
    [JsonPropertyName("is_recurring")]
    public short IsRecurring { get; set; }

    // ------------------------------------------------

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public double Duration { get; set; }

    [JsonPropertyName("no")]
    public int No { get; set; }

    [JsonPropertyName("booking_date")]
    public string BookingDate { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("attendees")]
    public int Attendees { get; set; }

    [JsonPropertyName("attendees_list")]
    public BookingInvitationVMList? AttendeesList { get; set; }
    
    [JsonPropertyName("package_id")]
    public string PackageId { get; set; } = string.Empty;
    
    [JsonPropertyName("package_menus")]
    public List<PantryDetailVMMenus>? PackageMenus { get; set; }

    [JsonPropertyName("pantry_package")]
    public string PantryPackage { get; set; } = string.Empty;

    [JsonPropertyName("pantry_detail")]
    public List<PantryTransaksiDViewModel> PantryDetail { get; set; }

    [JsonPropertyName("pic_nik")]
    public string? PicNIK { get; set; }

    [JsonPropertyName("room_image")]
    public string? RoomImage { get; set; }
}

public class BookingVMChart
{
    [JsonPropertyName("month")]
    public string Month { get; set; } = string.Empty;
    
    [JsonPropertyName("md")]
    public string Md { get; set; } = string.Empty;
    
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    [JsonPropertyName("tahun")]
    public int Tahun { get; set; }
}

public class BookingVMCreateReserveFR
{
    [BindProperty(Name = "booking_type")]
    public string BookingType { get; set; } = string.Empty;

    [BindProperty(Name = "date")]
    public DateOnly Date { get; set; }

    [BindProperty(Name = "date_until")]
    public DateOnly DateUntil { get; set; }

    [BindProperty(Name = "room_id")]
    public string RoomId { get; set; } = string.Empty;

    [BindProperty(Name = "title")]
    public string Title { get; set; } = string.Empty;

    [BindProperty(Name = "start")]
    public TimeSpan Start { get; set; }

    [BindProperty(Name = "end")]
    public TimeSpan End { get; set; }
    
    [BindProperty(Name = "pic")]
    public string Pic { get; set; } = string.Empty;

    [BindProperty(Name = "alocation_id")]
    public string AlocationId { get; set; } = string.Empty;

    [BindProperty(Name = "alocation_name")]
    public string AlocationName { get; set; } = string.Empty;

    [BindProperty(Name = "note")]
    public string Note { get; set; } = string.Empty;

    [BindProperty(Name = "external_link")]
    public string ExternalLink { get; set; } = string.Empty;
    
    [BindProperty(Name = "is_private")]
    public string IsPrivate { get; set; } = string.Empty;
    
    [BindProperty(Name = "meeting_category")]
    public string MeetingCategory { get; set; } = string.Empty;
    
    [BindProperty(Name = "external_attendees[]")]
    public string[] ExternalAttendees { get; set; } = new string[]{};
    
    [BindProperty(Name = "internal_attendees[]")]
    public string[] InternalAttendees { get; set; } = new string[]{};
    
    [BindProperty(Name = "menu_items[]")]
    public string[] MenuItems { get; set; } = new string[]{};
    
    [BindProperty(Name = "device")]
    public string Device { get; set; } = string.Empty;
}

public class BookingVMAttendance
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("nik")]
    public string Nik { get; set; } = string.Empty;
    
    [JsonPropertyName("company")]
    public string Company { get; set; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("position")]
    public string Position { get; set; } = string.Empty;
    
    [JsonPropertyName("is_vip")]
    public int IsVip { get; set; }
}

public class BookingVMMenuItem
{
    [JsonPropertyName("menu_id")]
    public int MenuId { get; set; }
    
    [JsonPropertyName("pantry_id")]
    public int PantryId { get; set; }
    
    [JsonPropertyName("qty")]
    public int Qty { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }
}

public class BookingVMDataTableFR : DataTableViewModel
{
    [FromQuery(Name = "booking_date")]
    public string? BookingDate { get; set; }

    [FromQuery(Name = "booking_organizer")]
    public string? BookingOrganizer { get; set; }

    [FromQuery(Name = "booking_building")]
    public long BookingBuilding { get; set; }

    [FromQuery(Name = "booking_room")]
    public string? BookingRoom { get; set; }
}

public class BookingVMRoomUsageFR : DataTableViewModel
{
    [FromQuery(Name = "date")]
    public string? Date { get; set; }
    [FromQuery(Name = "building_id")]
    public long BuildingId { get; set; }
    [FromQuery(Name = "room_id")]
    public string? RoomId { get; set; }
    [FromQuery(Name = "alocation_id")]
    public string? AlocationId { get; set; }
}

public class BookingVMRoomUsageCollection : BookingViewModel
{
    [JsonPropertyName("room_location")]
    public string RoomLocation { get; set; } = string.Empty;
    
    [JsonPropertyName("memo_no")]
    public string MemoNo { get; set; } = string.Empty;
    
    [JsonPropertyName("referensi_no")]
    public string ReferensiNo { get; set; } = string.Empty;
    
    [JsonPropertyName("invoice_status")]
    public string InvoiceStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("alocation_type")]
    public string AlocationType { get; set; } = string.Empty;
    
    [JsonPropertyName("alocation_invoice_status")]
    public int AlocationInvoiceStatus { get; set; } = 0;
    
    [JsonPropertyName("alocation_type_invoice_status")]
    public int AlocationTypeInvoiceStatus { get; set; } = 0;
    
    [JsonPropertyName("name_employee")]
    public string NameEmployee { get; set; } = string.Empty;
    
    [JsonPropertyName("email_employee")]
    public string EmailEmployee { get; set; } = string.Empty;
    
    [JsonPropertyName("phone_employee")]
    public string PhoneEmployee { get; set; } = string.Empty;
    
    [JsonPropertyName("ext_employee")]
    public string ExtEmployee { get; set; } = string.Empty;
}

public class BookingVMRoomUsageData : DataTableVM
{
    public IEnumerable<BookingVMRoomUsageCollection>? Collection { get; set; }
}

public class BookingVMRescheduleFR
{
    [BindProperty(Name = "date")]
    public DateOnly? Date { get; set; }

    [BindProperty(Name = "timezone")]
    public string Timezone { get; set; } = string.Empty;

    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [BindProperty(Name = "start")]
    public DateTime? Start { get; set; }

    [BindProperty(Name = "end")]
    public DateTime? End { get; set; }
}

public class BookingVMCancelFR
{
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;

    [BindProperty(Name = "reason")]
    public string Reason { get; set; } = string.Empty;
}

public class BookingVMCancelAllFR : BookingVMCancelFR
{
    [BindProperty(Name = "is_all")]
    public bool IsAll { get; set; } = false;
}

public class BookingVMEndMeetingFR
{
    [JsonPropertyName("id")]
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("booking_id")]
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    [BindProperty(Name = "user")]
    public bool User { get; set; } = false;
}

public class BookingVMCheckExtendMeetingFR
{
    [JsonPropertyName("booking_id")]
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    [BindProperty(Name = "time")]
    public TimeOnly Time { get; set; }

    [JsonPropertyName("date")]
    [BindProperty(Name = "date")]
    public DateOnly Date { get; set; }
}

public class BookingVMExtendMeetingFR
{
    [JsonPropertyName("booking_id")]
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [JsonPropertyName("index")]
    [BindProperty(Name = "index")]
    public int Index { get; set; }

    [JsonPropertyName("extend")]
    [BindProperty(Name = "extend")]
    public int Extend { get; set; }

    [JsonPropertyName("name")]
    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;
}

public class BookingModuleDetailsViewModel
{
    [JsonPropertyName("module_id")]
    public string ModuleId { get; set; } = string.Empty;

    [JsonPropertyName("module_text")]
    public string ModuleText { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("module_serial")]
    public string ModuleSerial { get; set; } = string.Empty;

    [JsonPropertyName("is_enabled")]
    public int IsEnabled { get; set; }
}

public class BookingModulesViewModel
{
    [JsonPropertyName("automation")]
    public BookingModuleDetailsViewModel? Automation { get; set; }

    [JsonPropertyName("vip")]
    public BookingModuleDetailsViewModel? Vip { get; set; }

    [JsonPropertyName("price")]
    public BookingModuleDetailsViewModel? Price { get; set; }

    [JsonPropertyName("int_365")]
    public BookingModuleDetailsViewModel? Int365 { get; set; }

    [JsonPropertyName("int_google")]
    public BookingModuleDetailsViewModel? IntGoogle { get; set; }

    [JsonPropertyName("room_adv")]
    public BookingModuleDetailsViewModel? RoomAdv { get; set; }
}

public class BookingVMConfirmAttendanceFR
{

    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [BindProperty(Name = "nik")]
    public string Nik { get; set; } = string.Empty;

    [BindProperty(Name = "status")]
    public int AttendanceStatus { get; set; }

    [BindProperty(Name = "attendance_reason")]
    public string AttendanceReason { get; set; } = string.Empty;

}

public class BookingVMCreateNewOrderFR
{
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;
    
    [BindProperty(Name = "meeting_category")]
    public string MeetingCategory { get; set; } = string.Empty;
    
    [BindProperty(Name = "menu_items[]")]
    public string[] MenuItems { get; set; } = new string[]{};
}

public class BookingMenuDetailViewModel
{
    [JsonPropertyName("pagename")]
    public string Pagename { get; set; } = string.Empty;

    [JsonPropertyName("menu")]
    public object Menu { get; set; } = default!;

    [JsonPropertyName("building")]
    public object Building { get; set; } = default!;

    [JsonPropertyName("invoice")]
    public object Invoice { get; set; } = default!;

    [JsonPropertyName("category")]
    public object Category { get; set; } = default!;

    [JsonPropertyName("setting_general")]
    public object SettingGeneral { get; set; } = default!;

    [JsonPropertyName("room")]
    public object Room { get; set; } = default!;

    [JsonPropertyName("organizer")]
    public object Organizer { get; set; } = default!;

    [JsonPropertyName("modules")]
    public BookingModulesViewModel? Modules { get; set; }

    [JsonPropertyName("facility")]
    public object Facility { get; set; } = default!;
}

public class BookingVMDuration
{
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("book")]
    public int Book { get; set; }

    [JsonPropertyName("time_data")]
    public TimeSpan TimeData { get; set; }
}


public class ListBookingByDateFR
{
    [JsonPropertyName("room_id")]
    public string RadId { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;
}

public class BookedTimeViewModel
{
    [JsonPropertyName("time_array")]
    public string TimeArray { get; set; } = string.Empty;

    [JsonPropertyName("booked_count")]
    public int BookedCount { get; set; }

    [JsonPropertyName("canceled")]
    public int Canceled { get; set; }

    [JsonPropertyName("expired")]
    public int Expired { get; set; }

    [JsonPropertyName("end_early")]
    public int EndEarly { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}

public class ListBookingByDateNikFRViewModel
{
    [JsonPropertyName("room_id")]
    public string RadId { get; set; } = string.Empty;


    [JsonPropertyName("nik")]
    public string Nik { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty; // Consider using DateTime if necessary

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty; // Consider using TimeSpan if necessary
}

public class TimeBookingListViewModel
{
    [JsonPropertyName("room_id")]
    public string? RadId { get; set; }

    [JsonPropertyName("type_room")]
    public string? TypeRoom { get; set; }

    [JsonPropertyName("datetime")]
    public List<TimeBookingDTOViewModel>? Datetime { get; set; }
    
}

public class TimeBookingDTOViewModel
{
    [JsonPropertyName("room_id")]
    public string? RoomId { get; set; }

    [JsonPropertyName("time_array")]
    public string? TimeArray { get; set; }

    [JsonPropertyName("booked_count")]
    public int BookedCount { get; set; }

    [JsonPropertyName("canceled")]
    public int Canceled { get; set; }

    [JsonPropertyName("expired")]
    public int Expired { get; set; }

    [JsonPropertyName("end_early")]
    public int EndEarly { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}

public class BookingDisplayScheduleFastBookedFRViewModel
{
    [JsonPropertyName("serial")]
    public string Serial { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("room_id")]
    public string RadId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("nik")]
    public string Nik { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; } 

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("notif")]
    public int Notif { get; set; }
    [JsonPropertyName("is_merge")]
    public short? IsMerge { get; set; }

    [JsonPropertyName("MergeRoom")]
    public List<string>? MergeRoom { get; set; }

    [JsonPropertyName("internal_data")]
    public List<FastBookListlDataInternalFRViewModel>? InternalData { get; set; }
    [JsonPropertyName("external_data")]
    public List<FastBookListlDataExternalFRViewModel>? ExternalData { get; set; }
}


public class ListlDataNikFRViewModel
{
    [JsonPropertyName("nik")]
    public List<string> Nik { get; set; }
}


public class ListInternalDataFRViewModel
{
    [JsonPropertyName("nik")]
    public string Nik { get; set; } = string.Empty;

}

public class ListRoomMergeFRViewModel
{
    [JsonPropertyName("room_id")]
    public string RadId { get; set; } = string.Empty;

}


public class ListScheduleOccupiedFRViewModel : ListRoomMergeFRViewModel
{
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

}

public class ListDisplaySerialFRViewModel
{
    [JsonPropertyName("serial")]
    public string? Serial { get; set; }

}

public class ListDisplayMeetingScheduleTodayFRViewModel 
{

    [JsonPropertyName("serial")]
    public string Serial { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("room_id")]
    public string RadId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("nik")]
    public string Nik { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("notif")]
    public int Notif { get; set; }

    [JsonPropertyName("is_merge")]
    public short? IsMerge { get; set; }

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }
    [JsonPropertyName("link")]
    public string? Link { get; set; }
    [JsonPropertyName("note")]
    public string? Note { get; set; }
    [JsonPropertyName("room_select")]
    public string? RoomSelect { get; set; }

}


public class MeetingDisplayCollection
{
    public DateTime ServerDateTime { get; set; }
    public List<string> RoomSelect { get; set; } = new List<string>();
}

public class DateSimpleRoomViewModel
{
    [JsonPropertyName("room_id")]
    public string RadId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("location")]
    public string Location { get; set; }
    [JsonPropertyName("link")]
    public string? Link { get; set; }
}

public class CheckDoorOpenMeetingPinFRViewModel
{
    [JsonPropertyName("room_id")]
    public string RadId { get; set; } = string.Empty;
    [JsonPropertyName("date")]
    // [JsonConverter(typeof(DateOnlyJsonConverter))] // Force "yyyy-MM-dd" format
    public DateOnly Date { get; set; }
    [JsonPropertyName("time")]
    // [JsonConverter(typeof(TimeOnlyJsonConverter))] // Correctly applies TimeOnly converter
    public string? Time { get; set; }
    [JsonPropertyName("pin")]
    public string Pin { get; set; } = string.Empty;
}


public class BookingVMNeedApprovalDataTableFR : DataTableViewModel
{
    [FromQuery(Name = "start_date")]
    public DateOnly StartDate { get; set; }

    [FromQuery(Name = "end_date")]
    public DateOnly EndDate { get; set; }

    [FromQuery(Name = "room_id")]
    public string RoomId { get; set; } = string.Empty;
}

public class BookingVMApprovalFR 
{
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [BindProperty(Name = "approval")]
    public int Approval { get; set; }
}

public class BookingVMAdditionalAttendeesFR
{
    [BindProperty(Name = "booking_id")]
    public string BookingId { get; set; } = string.Empty;
    
    [BindProperty(Name = "external_attendees[]")]
    public string[] ExternalAttendees { get; set; } = new string[]{};
    
    [BindProperty(Name = "internal_attendees[]")]
    public string[] InternalAttendees { get; set; } = new string[]{};
}


// public class SetExtendBookingDisplayFRViewModel : BookingVMExtendMeetingFR
// {

//     [JsonPropertyName("npk")]
//     public string Npk { get; set; } = string.Empty;

//     [JsonPropertyName("password")]
//     public string Password { get; set; } = string.Empty;
// }