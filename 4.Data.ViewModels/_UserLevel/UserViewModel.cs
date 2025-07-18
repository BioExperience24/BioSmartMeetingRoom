using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

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
    [JsonPropertyName("nik")]
    public string Nik { get; set; } = null!;

    [JsonPropertyName("vip_approve_bypass")]
    public int? VipApproveBypass { get; set; }

    [JsonPropertyName("vip_limit_cap_bypass")]
    public int? VipLimitCapBypass { get; set; }

    [JsonPropertyName("vip_shifted_bypass")]
    public int? VipShiftedBypass { get; set; }

    [JsonPropertyName("is_approval")]
    public int? IsApproval { get; set; }

    [JsonPropertyName("is_protected")]
    public int IsProtected { get; set; }

    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("name_emp")]
    public string? EmployeeName { get; set; }

    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("level")]
    public LevelViewModel? Level { get; set; }
    [JsonPropertyName("secure_qr")]
    public string? SecureQr { get; set; }

    [JsonPropertyName("head_employee_id")]
    public string? HeadEmployeeId { get; set; }

    //[JsonPropertyName("levels")]
    //public List<MenuHeaderLevelVM>? Levels { get; set; }

    // [JsonPropertyName("menu_headers")]
    // public List<MenuHeaderLevelVM>? MenuHeaders { get; set; }

    [JsonPropertyName("side_menu")]
    public List<MenuVM>? SideMenu { get; set; }
}

public class MenuHeaderLevelVM
{
    public string LevelName { get; set; }
    public string MenuName { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
}
public class MenuVM
{
    public string MenuName { get; set; } = string.Empty;

    public string MenuIcon { get; set; } = string.Empty;

    public string MenuUrl { get; set; } = string.Empty;

    public int MenuSort { get; set; }

    public string ModuleText { get; set; } = string.Empty;

    public List<MenuVM> Child { get; set; } = new List<MenuVM>();
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
public class UserVMUpdateUsernameFR
{
    [BindProperty(Name = "username")]
    public string Username { get; set; } = null!;
}
public class UserVMUpdatePasswordFR
{
    [BindProperty(Name = "old_pass")]
    public string Password { get; set; } = null!;
    [BindProperty(Name = "new_pass")]
    public string NewPassword { get; set; } = null!;
    [BindProperty(Name = "con_pass")]
    public string ConfirmationPassword { get; set; } = null!;
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

public class LoginWebviewModel
{
    public required string Username { get; set; }

    public required string Nik { get; set; }
}

public class TokenManagement
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessExpiration { get; set; }
    public int RefreshExpiration { get; set; }
}