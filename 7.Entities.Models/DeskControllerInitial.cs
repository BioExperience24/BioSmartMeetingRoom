using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskControllerInitial
{
    public int Id { get; set; }

    public int? Socket { get; set; }

    public string? ControllerId { get; set; }

    public string? DeskRoomId { get; set; }

    public string? DeskId { get; set; }
}
