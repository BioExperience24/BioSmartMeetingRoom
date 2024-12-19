using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingEmailTemplate : BaseEntity
{
    public long Id { get; set; }

    public int? IsEnabled { get; set; }

    public string? Type { get; set; }

    public string? TitleOfText { get; set; }

    public string? ToText { get; set; }

    public string? TitleAgendaText { get; set; }

    public string? DateText { get; set; }

    public string? Room { get; set; }

    public string? DetailLocation { get; set; }

    public string? GreetingText { get; set; }

    public string? ContentText { get; set; }

    public string? AttendanceText { get; set; }

    public string? AttendanceNoText { get; set; }

    public string? CloseText { get; set; }

    public string? SupportText { get; set; }

    public string? FootText { get; set; }

    public string? Link { get; set; }

    public string? MapLinkText { get; set; }

    public new int? IsDeleted { get; set; }
}
