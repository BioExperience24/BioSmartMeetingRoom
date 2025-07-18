using System;

namespace _7.Entities.Models;

public partial class Session
{
    public string? Id { get; set; }

    public byte[]? Value { get; set; }

    public DateTimeOffset ExpiresAtTime { get; set; }

    public long? SlidingExpirationInSeconds { get; set; }

    public DateTimeOffset? AbsoluteExpiration { get; set; }
}
