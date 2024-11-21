using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LicenseSetting
{
    public int Id { get; set; }

    public string? Serial { get; set; }

    public string? Platform { get; set; }

    public string? DeviceId { get; set; }

    public DateTime? CheckedAt { get; set; }

    public int? Status { get; set; }

    public string? DistributorId { get; set; }

    public string? CustomerId { get; set; }

    public string? Ext { get; set; }

    public string? Webhost { get; set; }

    public string? LicenseType { get; set; }

    public string? Pathdownload { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
