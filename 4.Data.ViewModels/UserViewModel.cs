using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _4.Data.ViewModels
{
    public class UserViewModel : BaseLongViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("level_id")]
        public int LevelId { get; set; }

        [JsonPropertyName("access_id")]
        public string? AccessId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("is_disactived")]
        public int IsDisactived { get; set; }

        [JsonPropertyName("is_vip")]
        public int? IsVip { get; set; }

        [JsonPropertyName("vip_approve_bypass")]
        public int? VipApproveBypass { get; set; }

        [JsonPropertyName("vip_limit_cap_bypass")]
        public int? VipLimitCapBypass { get; set; }

        [JsonPropertyName("vip_shifted_bypass")]
        public int? VipShiftedBypass { get; set; }

        [JsonPropertyName("is_approval")]
        public int? IsApproval { get; set; }

        [JsonPropertyName("group_name")]
        public string? GroupName { get; set; }

        [JsonPropertyName("name_emp")]
        public string? EmployeeName { get; set; }
    }

    public class UserVMDefaultFR
    {
        [BindProperty(Name = "username")]
        public string Username { get; set; } = string.Empty;

        [BindProperty(Name = "password")]
        public string Password { get; set; } = string.Empty;

        [BindProperty(Name = "level_id")]
        public int LevelId { get; set; }

        [BindProperty(Name = "is_disactived")]
        public int IsDisactived { get; set; }

        [BindProperty(Name = "access_id")]
        public string[] AccessId { get; set; } = new string[] { };
    }

    public class UserVMCreateFR : UserVMDefaultFR
    {
        [BindProperty(Name = "employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
    }

    public class UserVMUpdateFR : UserVMDefaultFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }

    public class UserVMDisableFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;

        [BindProperty(Name = "is_disactived")]
        public int IsDisactived { get; set; }
    }

    public class UserVMDeleteFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }
    public class LoginModel
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}