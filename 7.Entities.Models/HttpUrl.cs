using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class HttpUrl
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Headers { get; set; } = null!;
    public short IsDeleted { get; set; }
    public short IsEnable { get; set; }
}
