using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BatchUpload
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Time { get; set; }

    public int TotalRow { get; set; }

    public string TotalSize { get; set; } = null!;

    public int IsDeleted { get; set; }
}
