using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class User : BaseLongEntity
{
    public string? SecureQr { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string EmployeeId { get; set; }

    public string Password { get; set; } = null!;

    public string RealPassword { get; set; } = null!;

    public int LevelId { get; set; }

    public string? AccessId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int IsDisactived { get; set; }

    public int? UpdatedBy { get; set; }

    public int? IsVip { get; set; }

    public int? VipApproveBypass { get; set; }

    public int? VipLimitCapBypass { get; set; }

    public int? VipShiftedBypass { get; set; }

    public int? IsApproval { get; set; }
}
