﻿namespace _4.Data.ViewModels;

public class EmployeeViewModel : BaseViewModel
{
    public int Generate { get; set; }

    public string Id { get; set; } = null!;

    public string DivisionId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string DepartmentId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Nik { get; set; } = null!;

    public string? NikDisplay { get; set; }

    public string Photo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? NoPhone { get; set; }

    public string? NoExt { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string CardNumberReal { get; set; } = null!;

    public string PasswordMobile { get; set; } = null!;

    public string GbId { get; set; } = null!;

    public string FrId { get; set; } = null!;

    public int Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int IsDeleted { get; set; }

    public bool IsVip { get; set; } = false;

    public bool VipApproveBypass { get; set; } = false;

    public bool VipLimitCapBypass { get; set; } = false;

    public bool VipLockRoom { get; set; } = false;
}