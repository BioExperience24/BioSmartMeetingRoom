namespace _7.Entities.Models;

public partial class AlarmIntegration
{
    public int Id { get; set; }

    public int? StatusIntegration { get; set; }

    public int? Active { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? UrlAuth { get; set; }

    public string? UrlFeedback { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? ParamAuth { get; set; }

    public string? ParamFeed { get; set; }

    public string? Token { get; set; }

    public int? IsDeleted { get; set; }
}
