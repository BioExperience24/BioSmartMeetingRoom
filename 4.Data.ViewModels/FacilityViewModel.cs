namespace _4.Data.ViewModels;

public class FacilityViewModel : BaseViewModel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string GoogleIcon { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}