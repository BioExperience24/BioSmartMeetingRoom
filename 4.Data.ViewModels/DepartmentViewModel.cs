using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _4.Data.ViewModels;

public class DepartmentViewModel : BaseViewModel
{
    //public string IdDepartment { get; set; } = null!;

    public string IdPerusahaan { get; set; } = null!;

    public string NamaPerusahaan { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int UpdateAt { get; set; }
    public string CompanyName { get; set; } = null!;
    public CompanyViewModel Company { get; set; } = null!;

}