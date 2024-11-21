using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingPantryConfig
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int PantryExpired { get; set; }

    public int MaxOrderQty { get; set; }

    public int BeforeOrderMeeting { get; set; }
}
