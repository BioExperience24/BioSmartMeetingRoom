using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace _7.Entities.Models;

public partial class Department : BaseEntity
{
    //public string IdDepartment { get; set; } = null!;//ganti jd id aja

    public string IdPerusahaan { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int UpdateAt { get; set; }

    // Navigation Property
    public virtual Company Company { get; set; }
}
