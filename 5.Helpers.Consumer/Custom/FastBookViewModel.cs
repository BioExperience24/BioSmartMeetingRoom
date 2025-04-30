using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace _5.Helpers.Consumer.Custom
{

    public class FastBookLongViewModel {

        [Key]
        [JsonPropertyName("id")]
        [BindProperty(Name = "id")]
        public long? Id { get; set; } = null;

        [JsonPropertyName("is_deleted")]
        [BindProperty(Name = "is_deleted")]
        public int? IsDeleted { get; set; }
    }

    public class FastBookBaseViewModel
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; } = null;

        [JsonPropertyName("is_deleted")]
        public int? IsDeleted { get; set; }
    }

    public class FastBookBookingViewModel : FastBookLongViewModel
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
        public FastBookBookingInvitationVMList? AttendeesList { get; set; }

        [JsonPropertyName("package_id")]
        public string PackageId { get; set; } = string.Empty;

        [JsonPropertyName("package_menus")]
        public List<FastBookPantryDetailVMMenus>? PackageMenus { get; set; }

        [JsonPropertyName("pantry_package")]
        public string PantryPackage { get; set; } = string.Empty;

        [JsonPropertyName("pantry_detail")]
        public List<FastBookPantryTransaksiDViewModel> PantryDetail { get; set; }
    }
    public class FastBookBookingInvitationVMList
    {
        [JsonPropertyName("external_attendess")]
        public List<FastBookBookingInvitationVMCategory> ExternalAttendess { get; set; } = new List<FastBookBookingInvitationVMCategory>();

        [JsonPropertyName("internal_attendess")]
        public List<FastBookBookingInvitationVMCategory> InternalAttendess { get; set; } = new List<FastBookBookingInvitationVMCategory>();
    }


    public class FastBookPantryDetailVMMenus
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        [JsonPropertyName("pantry_id")]
        public string PantryId { get; set; } = string.Empty;

        [JsonPropertyName("qty")]
        public string Qty { get; set; } = string.Empty;
    }

    public class FastBookPantryTransaksiDViewModel : FastBookLongViewModel
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

    public class FastBookBookingInvitationVMCategory
    {
        [JsonPropertyName("nik")]
        public string? Nik { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("company")]
        public string Company { get; set; } = string.Empty;

        [JsonPropertyName("position")]
        public string Position { get; set; } = string.Empty;

        [JsonPropertyName("employee_name")]
        public string? EmployeeName { get; set; }

        [JsonPropertyName("employee_no_phone")]
        public string? EmployeeNoPhone { get; set; }

        [JsonPropertyName("employee_email")]
        public string? EmployeeEmail { get; set; }
    }


    public class FastBookAlocationVMDefaultFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public string Id { get; set; } = string.Empty;

        [BindProperty(Name = "name", SupportsGet = false)]
        public string Name { get; set; } = string.Empty;
    }

    public class FastBookRoomViewModel : FastBookLongViewModel
    {
        [JsonPropertyName("radid")]
        public string Radid { get; set; } = string.Empty;

        [JsonPropertyName("building_id")]
        public long? BuildingId { get; set; }

        [JsonPropertyName("floor_id")]
        public long? FloorId { get; set; }

        [JsonPropertyName("type_room")]
        public string? TypeRoom { get; set; }

        [JsonPropertyName("kind_room")]
        public string? KindRoom { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("google_map")]
        public string? GoogleMap { get; set; }

        [JsonPropertyName("is_automation")]
        public short IsAutomation { get; set; }

        [JsonPropertyName("automation_id")]
        public int AutomationId { get; set; }

        [JsonPropertyName("facility_room")]
        public List<string> FacilityRoom { get; set; } = new List<string>();

        [JsonPropertyName("work_day")]
        public List<string>? WorkDay { get; set; }

        [JsonPropertyName("work_time")]
        public string WorkTime { get; set; } = string.Empty;

        [JsonPropertyName("work_start")]
        public string? WorkStart { get; set; }

        [JsonPropertyName("work_end")]
        public string? WorkEnd { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        public string? Image2 { get; set; }

        [JsonPropertyName("multiple_image")]
        public string MultipleImage { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public long Price { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("is_disabled")]
        public short? IsDisabled { get; set; }

        [JsonPropertyName("is_beacon")]
        public short? IsBeacon { get; set; }

        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; set; }

        [JsonPropertyName("is_config_setting_enable")]
        public int? IsConfigSettingEnable { get; set; }

        [JsonPropertyName("config_room_for_usage")]
        public List<string>? ConfigRoomForUsage { get; set; }

        [JsonPropertyName("is_enable_approval")]
        public int? IsEnableApproval { get; set; }

        [JsonPropertyName("config_approval_user")]
        public List<string>? ConfigApprovalUser { get; set; }

        [JsonPropertyName("is_enable_permission")]
        public int? IsEnablePermission { get; set; }

        [JsonPropertyName("config_permission_user")]
        public List<string>? ConfigPermissionUser { get; set; }

        [JsonPropertyName("config_permission_checkin")]
        public string? ConfigPermissionCheckin { get; set; }

        [JsonPropertyName("config_permission_end")]
        public string? ConfigPermissionEnd { get; set; }

        [JsonPropertyName("config_min_duration")]
        public int? ConfigMinDuration { get; set; }

        [JsonPropertyName("config_max_duration")]
        public int? ConfigMaxDuration { get; set; }

        [JsonPropertyName("config_advance_booking")]
        public int? ConfigAdvanceBooking { get; set; }

        [JsonPropertyName("is_enable_recurring")]
        public int? IsEnableRecurring { get; set; }

        [JsonPropertyName("is_enable_checkin")]
        public int? IsEnableCheckin { get; set; }

        [JsonPropertyName("config_advance_checkin")]
        public int? ConfigAdvanceCheckin { get; set; }

        [JsonPropertyName("is_realease_checkin_timeout")]
        public int? IsRealeaseCheckinTimeout { get; set; }

        [JsonPropertyName("config_release_room_checkin_timeout")]
        public int? ConfigReleaseRoomCheckinTimeout { get; set; }

        [JsonPropertyName("config_participant_checkin_count")]
        public int ConfigParticipantCheckinCount { get; set; }

        [JsonPropertyName("is_enable_checkin_count")]
        public int? IsEnableCheckinCount { get; set; }

        [JsonPropertyName("config_google")]
        public string? ConfigGoogle { get; set; }

        [JsonPropertyName("config_microsoft")]
        public string? ConfigMicrosoft { get; set; }
    }

    public class FastBookBookingInvitationViewModel : FastBookLongViewModel
    {
        [JsonPropertyName("booking_id")]
        public string BookingId { get; set; } = null!;

        [JsonPropertyName("nik")]
        public string Nik { get; set; } = null!;

        [JsonPropertyName("internal")]
        public int Internal { get; set; }

        [JsonPropertyName("attendance_status")]
        public int AttendanceStatus { get; set; }

        [JsonPropertyName("attendance_reason")]
        public string? AttendanceReason { get; set; }

        [JsonPropertyName("execute_attendance")]
        public int? ExecuteAttendance { get; set; }

        [JsonPropertyName("execute_door_access")]
        public int ExecuteDoorAccess { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("company")]
        public string Company { get; set; } = null!;

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("is_pic")]
        public short IsPic { get; set; }

        [JsonPropertyName("is_vip")]
        public int? IsVip { get; set; }

        [JsonPropertyName("pin_room")]
        public string? PinRoom { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("last_update_365")]
        public string? LastUpdate365 { get; set; }

        [JsonPropertyName("checkin")]
        public int? Checkin { get; set; }

        [JsonPropertyName("end_meeting")]
        public int? EndMeeting { get; set; }
    }


    public class FastBookEmployeeViewModel : FastBookBaseViewModel
    {

        [JsonPropertyName("company")]
        public string Company { get; set; } = null!;
        [JsonPropertyName("booking_id")]
        public string BookingId { get; set; } = null!;
        [JsonPropertyName("division_id")]
        public string DivisionId { get; set; } = string.Empty;

        [JsonPropertyName("company_id")]
        public string CompanyId { get; set; } = string.Empty;

        [JsonPropertyName("department_id")]
        public string DepartmentId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("nik")]
        public string Nik { get; set; } = string.Empty;

        [JsonPropertyName("nik_display")]
        public string? NikDisplay { get; set; }

        [JsonPropertyName("photo")]
        public string Photo { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("no_phone")]
        public string? NoPhone { get; set; }

        [JsonPropertyName("no_ext")]
        public string? NoExt { get; set; }

        [JsonPropertyName("birth_date")]
        public DateOnly? BirthDate { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("card_number")]
        public string CardNumber { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("is_vip")]
        public int? IsVip { get; set; }

        [JsonPropertyName("vip_approve_bypass")]
        public int? VipApproveBypass { get; set; }

        [JsonPropertyName("vip_limit_cap_bypass")]
        public int? VipLimitCapBypass { get; set; }

        [JsonPropertyName("vip_lock_room")]
        public int? VipLockRoom { get; set; }
        [JsonPropertyName("pin")]
        public string Pin { get; set; } = string.Empty;
        [JsonPropertyName("is_pic")]
        public short IsPic { get; set; }
    }

    public class FastBookInternalBatchViewModel
    {
        [JsonPropertyName("internal_batch")]
        public List<FastBookBookingInvitationViewModel> InternalBatch { get; set; }
        [JsonPropertyName("data_email_internal")]
        public List<FastBookEmployeeViewModel> DataEmailInternal { get; set; }
    }

    public class SendMailViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("isHtml")]
        public bool IsHtml { get; set; }
        [JsonPropertyName("emailType")]
        public string EmailType { get; set; } = string.Empty;
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;
        [JsonPropertyName("startMeeting")]
        public string StartMeeting { get; set; } = string.Empty;
        [JsonPropertyName("endMeeting")]
        public string EndMeeting { get; set; } = string.Empty;
        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;
        [JsonPropertyName("organizer")]
        public SendMailVMOrganizer Organizer { get; set; } = new SendMailVMOrganizer();
        [JsonPropertyName("attendees")]
        public SendMailVMAttendees Attendees { get; set; } = new SendMailVMAttendees();
        [JsonPropertyName("place")]
        public SendMailVMPlace Place { get; set; } = new SendMailVMPlace();
    }

    public class SendMailVMOrganizer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("nrp")]
        public string Nrp { get; set; } = string.Empty;

        [JsonPropertyName("nik_display")]
        public string NikDisplay { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;
    }

    public class SendMailVMAttendees
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty; // external or internal
    }

    public class SendMailVMPlace
    {
        [JsonPropertyName("building")]
        public string Building { get; set; } = string.Empty;

        [JsonPropertyName("floor")]
        public string Floor { get; set; } = string.Empty;

        [JsonPropertyName("room")]
        public string Room { get; set; } = string.Empty;

        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("kind_room")]
        public string KindRoom { get; set; } = string.Empty;
    }
}