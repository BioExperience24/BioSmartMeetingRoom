﻿using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryTransaksiD : BaseLongEntity
{
    // public int Id { get; set; }

    public string TransaksiId { get; set; } = null!;

    public long? MenuId { get; set; }

    public int Qty { get; set; }

    public string? NoteOrder { get; set; } = null!;

    public string? NoteReject { get; set; } = null!;

    public string? Detailorder { get; set; } = null!;

    public int Status { get; set; }

    public int IsRejected { get; set; }

    public string RejectedBy { get; set; } = null!;

    public DateTime RejectedAt { get; set; }

    // public int IsDeleted { get; set; }
}
public class DetailPantryDto
{
    public string TransaksiId { get; set; } = null!;
    public long? MenuId { get; set; }
    public int Qty { get; set; }
    public string NoteOrder { get; set; } = null!;
    public string NoteReject { get; set; } = null!;
    public string Detailorder { get; set; } = null!;
    public int Status { get; set; }
    public int IsRejected { get; set; }
    public string RejectedBy { get; set; } = null!;
    public DateTime RejectedAt { get; set; }
}

public class PantryTransaksiAndMenu : PantryTransaksiD
{
    public long? ItemId { get; set; }
    public string Name { get; set; } = null!;//menu name
}
