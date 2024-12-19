using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

public class PantryMenuPaketViewModel : BaseViewModel
{

    public long pantry_id { get; set; }
    public string name { get; set; } = null!;

    public int created_by { get; set; }

    public int updated_by { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    //adds
    public string? pantry_name { get; set; } = null;
    public PantryViewModel? pantry { get; set; } = null;
    public List<PantryDetailViewModel>? menu { get; set; }

}

public class PantryPackageDataAndDetail
{
    public PantryMenuPaketViewModel? data { get; set; }
    public List<PantryDetailViewModel>? detail { get; set; }
}