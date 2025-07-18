using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class EmployeeViewModel : BaseViewModel
{
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

    [JsonPropertyName("head_employee_id")]
    public string HeadEmployeeId { get; set; } = string.Empty;

    [JsonPropertyName("is_protected")]
    public int IsProtected { get; set; }
}

public class EmployeeVMDefaultFR
{
    [FromForm(Name ="company_id")]
    public string CompanyId { get; set; } = string.Empty;

    [FromForm(Name = "department_id")]
    public string DepartmentId { get; set; } = string.Empty;

    [FromForm(Name = "name")]
    public string Name { get; set; } = string.Empty;

    [FromForm(Name = "nik_display")]
    public string? NikDisplay { get; set; }

    [FromForm(Name = "email")]
    public string Email { get; set; } = string.Empty;

    [FromForm(Name = "no_phone")]
    public string? NoPhone { get; set; }

    [FromForm(Name = "no_ext")]
    public string? NoExt { get; set; }

    [FromForm(Name = "birth_date")]
    public DateOnly? BirthDate { get; set; }

    [FromForm(Name = "gender")]
    public string Gender { get; set; } = string.Empty;

    [FromForm(Name = "address")]
    public string? Address { get; set; } = string.Empty;

    [FromForm(Name = "card_number")]
    public string? CardNumber { get; set; }
    
    [FromForm(Name = "head_employee")]
    public string? HeadEmployeeId { get; set; }
}

public class EmployeeVMCreateFR : EmployeeVMDefaultFR
{
    [FromForm(Name = "is_vip")]
    public string? IsVip { get; set; } 

    [FromForm(Name = "vip_approve_bypass")]
    public string? VipApproveBypass { get; set; }

    [FromForm(Name = "vip_limit_cap_bypass")]
    public string? VipLimitCapBypass { get; set; }

    [FromForm(Name = "vip_lock_room")]
    public string? VipLockRoom { get; set; }

    [FromForm(Name = "photo")]
    public IFormFile? FilePhoto { get; set; }

    public string? Photo { get; set; }
}

public class EmployeeVMUpdateVipFR
{
    [FromForm(Name = "is_vip")]
    public string? IsVip { get; set; } 

    [FromForm(Name = "vip_approve_bypass")]
    public string? VipApproveBypass { get; set; }

    [FromForm(Name = "vip_limit_cap_bypass")]
    public string? VipLimitCapBypass { get; set; }

    [FromForm(Name = "vip_lock_room")]
    public string? VipLockRoom { get; set; }
}

public class EmployeeVMDeleteFR
{
    [FromForm(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [FromForm(Name = "name")]
    public string Name { get; set; } = string.Empty;
}

public class EmployeeVMImportFR
{ 
    [FromForm(Name = "file_import")]
    public IFormFile? FileImport { get; set; }
}

public class EmployeeVMImportData
{
    public string Company { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string HeadEmployeeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nrp { get; set; } = string.Empty;
    public string? BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string? NoPhone { get; set; }
    public string? NoExt { get; set; }
    public int? IsVip { get; set; }
    public int? VipApproveBypass { get; set; }
}

public class EmployeeVMResp : EmployeeViewModel
{
    [JsonPropertyName("company_name")]
    public string? CompanyName { get; set; }
    
    [JsonPropertyName("department_name")]
    public string? DepartmentName { get; set; }
}
public class EmployeeWithAccessInfoViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("employee_id")]
    public string EmployeeId { get; set; }
    [JsonPropertyName("nik")]
    public string Nik { get; set; }
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; }
    [JsonPropertyName("no_phone")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("no_ext")]
    public string ExtensionNumber { get; set; }
    [JsonPropertyName("access_id")]
    public string AccessId { get; set; }
}

public class EmployeeWithDetailsViewModel
{

    [JsonPropertyName("id")]
    public string? Id { get; set; } = null!;
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
    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; }
    [JsonPropertyName("department_name")]
    public string DepartmentName { get; set; }
}

public class EmployeeVMOrganizerFR : DataTableViewModel
{
    [FromQuery(Name = "date")]
    public string? Date { get; set; }
    [FromQuery(Name = "nik")]
    public string? Nik { get; set; }
    [FromQuery(Name = "building_id")]
    public long BuildingId { get; set; }
    [FromQuery(Name = "room_id")]
    public string? RoomId { get; set; }
}

public class EmployeeVMOrganizerCollection : EmployeeViewModel
{
    [JsonPropertyName("no")]
    public int No { get; set; } = 0;

    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; } = string.Empty;
    
    [JsonPropertyName("department_name")]
    public string DepartmentName { get; set; } = string.Empty;

    [JsonPropertyName("total_meeting")]
    public int TotalMeeting { get; set; } = 0;
    
    [JsonPropertyName("total_reschedule")]
    public int TotalReschedule { get; set; } = 0;

    [JsonPropertyName("total_cancel")]
    public int TotalCancel { get; set; } = 0;

    [JsonPropertyName("total_duration")]
    public int TotalDuration { get; set; } = 0;

    [JsonPropertyName("total_attendees")]
    public int TotalAttendees { get; set; } = 0;
    
    [JsonPropertyName("total_attendees_checkin")]
    public int TotalAttendeesCheckin { get; set; } = 0;
    
    [JsonPropertyName("total_approve")]
    public int TotalApprove { get; set; } = 0;
    
}

public class EmployeeVMOrganizerData : DataTableVM
{
    public IEnumerable<EmployeeVMOrganizerCollection>? Collections { get; set; }
}

public class EmployeeVMAttendeesCollection : EmployeeViewModel
{
    [JsonPropertyName("no")]
    public int No { get; set; } = 0;

    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; } = string.Empty;
    
    [JsonPropertyName("department_name")]
    public string DepartmentName { get; set; } = string.Empty;

    [JsonPropertyName("total_meeting")]
    public int TotalMeeting { get; set; } = 0;

    [JsonPropertyName("total_present")]
    public int TotalPresent { get; set; } = 0;
    
    [JsonPropertyName("total_absent")]
    public int TotalAbsent { get; set; } = 0;

    [JsonPropertyName("total_duration")]
    public int TotalDuration { get; set; } = 0;
    
    [JsonPropertyName("total_attendees_checkin")]
    public int TotalAttendeesCheckin { get; set; } = 0;    
}

public class EmployeeVMAttendeesData : DataTableVM
{
    public IEnumerable<EmployeeVMAttendeesCollection>? Collections { get; set; }
}