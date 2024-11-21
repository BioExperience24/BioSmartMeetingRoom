using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomGoogle
{
    public int Generate { get; set; }

    public string? Id { get; set; }

    public string? EmailAddress { get; set; }

    public string? DisplayName { get; set; }

    public string? GeoCoordinates { get; set; }

    public string? Phone { get; set; }

    public string? Nickname { get; set; }

    public string? Building { get; set; }

    public int? FloorNumber { get; set; }

    public string? FloorLabel { get; set; }

    public string? Label { get; set; }

    public string? Capacity { get; set; }

    public string? BookingType { get; set; }

    public string? AudioDeviceName { get; set; }

    public string? VideoDeviceName { get; set; }

    public string? DisplayDeviceName { get; set; }

    public short? IsWheelChairAccessible { get; set; }

    public string? Tags { get; set; }

    public string? Address { get; set; }

    public int? Initial { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }
}
