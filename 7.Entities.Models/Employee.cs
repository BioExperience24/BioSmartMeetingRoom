using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class Employee : BaseEntity
{
    [Column("_generate")]
    public int Generate { get; set; }

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


public class EmployeeFilter : Employee
{
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public long BuildingId { get; set; }
    public string? RoomId { get; set; }

}

public class EmployeeTotalParticipant
{
    public string? Nik { get; set; }
    public int? TotalMeeting { get; set; }
    public int? TotalReschedule { get; set; }
    public int? TotalCancel { get; set; }
    public int? TotalApprove { get; set; }
}

public class EmployeeReportOrganizerUsage : EmployeeTotalParticipant
{
    public Employee? Employee { get; set; }
    public string? CompanyName { get; set; }
    public string? DepartmentName { get; set; }
    // public int? TotalMeeting { get; set; }
    // public int? TotalReschedule { get; set; }
    // public int? TotalCancel { get; set; }
    public int? TotalDuration { get; set; }
    public int? TotalAttendees { get; set; }
    public int? TotalAttendeesCheckin { get; set; }
    // public int? TotalApprove { get; set; }
}

public class EmployeeReportOrganizerUsageDataTable
{
    public IEnumerable<EmployeeReportOrganizerUsage>? Collections { get; set; }
    public int RecordsTotal { get; set; } 
    public int RecordsFiltered { get; set; }
}

public class EmployeeReportAttendees : EmployeeTotalParticipant
{
    public Employee? Employee { get; set; }
    public string? CompanyName { get; set; }
    public string? DepartmentName { get; set; }
    public int? TotalPresent { get; set; }
    public int? TotalAbsent { get; set; }
    public int? TotalDuration { get; set; }
    public int? TotalAttendeesCheckin { get; set; }
}

public class EmployeeReportAttendeesDataTable
{
    public IEnumerable<EmployeeReportAttendees>? Collections { get; set; }
    public int RecordsTotal { get; set; } 
    public int RecordsFiltered { get; set; }
}

public class EmployeeWithAccessInfo
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

public class EmployeeWithDetails
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
