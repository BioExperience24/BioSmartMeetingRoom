using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Integration365
{
    public int Id { get; set; }

    public int? Status { get; set; }

    public string? Code { get; set; }

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? UserPrincipalName { get; set; }

    public string? AccountId { get; set; }

    public string? Scope { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? RefreshAt { get; set; }
}
