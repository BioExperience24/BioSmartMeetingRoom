using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7.Entities.Models;

public partial class Alocation : BaseEntity
{
    public int Generate { get; set; }

    // public string Id { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? InvoiceType { get; set; }

    public int? InvoiceStatus { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int IsPermanent { get; set; }

    // public int? IsDeleted { get; set; }

    public int? ShowInInvitation { get; set; }
}
