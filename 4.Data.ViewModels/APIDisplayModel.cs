using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;
public class DisplayLoginRequest
{
    [BindProperty(Name = "username")]
    public string Username { get; set; }

    [BindProperty(Name = "password")]
    public string Password { get; set; }

    [BindProperty(Name = "date")]
    public string Date { get; set; }
}

public class RoomRequest
{
    [BindProperty(Name = "username")]
    public string Username { get; set; }

    [BindProperty(Name = "room_id")]
    public string RoomId { get; set; }

    [BindProperty(Name = "date")]
    public string Date { get; set; }

    [BindProperty(Name = "time")]
    public string Time { get; set; }

    [BindProperty(Name = "nik")]
    public string Nik { get; set; }

    [BindProperty(Name = "timezone")]
    public string Timezone { get; set; }

    [BindProperty(Name = "room_select")]
    public string RoomSelect { get; set; } // Digunakan untuk method yang memerlukan list room
}

public class FastBookedRequest : RoomRequest
{
    [BindProperty(Name = "is_merge")]
    public string IsMerge { get; set; }

    [BindProperty(Name = "merge_room")]
    public List<string> MergeRoom { get; set; }

    [BindProperty(Name = "duration")]
    public int Duration { get; set; }

    [BindProperty(Name = "title")]
    public string Title { get; set; }

    [BindProperty(Name = "notif")]
    public int Notif { get; set; }

    [BindProperty(Name = "serial")]
    public string Serial { get; set; }

    [BindProperty(Name = "type")]
    public string Type { get; set; }
}

public class SerialRequest
{
    [BindProperty(Name = "serial")]
    public string Serial { get; set; }
}