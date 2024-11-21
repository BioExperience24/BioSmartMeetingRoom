namespace _4.Data.ViewModels;

public class DivisiViewModel : BaseViewModel
{
    //public string Id { get; set; } = null!;

    public string IdPerusahaan { get; set; } = null!;

    public string IdDepartment { get; set; } = null!;

    public string DivisiName { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int UpdateAt { get; set; }
}