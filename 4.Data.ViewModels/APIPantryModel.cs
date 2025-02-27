using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;


public class OrderByDateRequest
{

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "pantry_id")]
    public int PantryId { get; set; }
}

public class PushOrderRequest
{

    [BindProperty(Name = "pantry_id")]
    public long PantryId { get; set; }

    [BindProperty(Name = "transaksi_id")]
    public string? TransaksiId { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "note_reject")]
    public string? NoteReject { get; set; } // Optional, hanya digunakan untuk reject
}

public class MobilePlaceRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }
}

public class MobileMenuRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }

    [BindProperty(Name = "pantry_id")]
    public int PantryId { get; set; }
}

public class MobileMenuDetailRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }

    [BindProperty(Name = "menu_id")]
    public string? MenuId { get; set; }
}

public class MobileSubmitOrderRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public required string Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }

    [BindProperty(Name = "bookingId")]
    public required string BookingId { get; set; }

    [BindProperty(Name = "pantryId")]
    public required string PantryId { get; set; }

    [BindProperty(Name = "packageId")]
    public string? PackageId { get; set; }

    [BindProperty(Name = "pantryOrder")]
    public List<PantryOrder> PantryOrder { get; set; }
}

public class PantryOrder
{

    public int id { get; set; }

    public int qty { get; set; }

    public string? detailorder { get; set; }//bentuk json

    public string? note { get; set; }

    public int status { get; set; }//bentuk json
}

public class DetailOrder
{
    [BindProperty(Name = "variant_detail")]
    public string? VariantDetail { get; set; }
}

public class MobileDetailOrderRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }

    [BindProperty(Name = "id")]
    public string? Id { get; set; }//this is pantry transaksi ID
}

public class MobileCancelOrderRequest
{
    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }

    [BindProperty(Name = "id")]
    public string? Id { get; set; }

    [BindProperty(Name = "note")]
    public string? Note { get; set; }
}

public class MobileHistoryRequest
{
    [BindProperty(Name = "timezone")]
    public string? Timezone { get; set; }

    [BindProperty(Name = "username")]
    public string? Username { get; set; }

    [BindProperty(Name = "nik")]
    public string? Nik { get; set; }

    [BindProperty(Name = "date")]
    public string? Date { get; set; }

    [BindProperty(Name = "date1")]
    public string? Date1 { get; set; }

    [BindProperty(Name = "date2")]
    public string? Date2 { get; set; }

    [BindProperty(Name = "time")]
    public string? Time { get; set; }
}