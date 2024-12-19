using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Divisi : BaseEntity
{
    //public string IdDivisi { get; set; } = null!;

    public string IdPerusahaan { get; set; } = null!;

    public string IdDepartment { get; set; } = null!;

    public string DivisiName { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int UpdateAt { get; set; }

    // public int IsDeleted { get; set; }
}
