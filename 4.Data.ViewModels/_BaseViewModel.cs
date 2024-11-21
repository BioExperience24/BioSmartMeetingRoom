using _5.Helpers.Consumer.EnumType;

namespace _4.Data.ViewModels;

public class BaseViewModel
{
    public string Id { get; set; }
    public int? IsDeleted { get; set; }
}
public class ReturnalModel
{
    public string Type { get; set; } = ReturnalType.Success;
    public int Status { get; set; } = 200;
    public string Title { get; set; } = ReturnalType.Success;
    public string Detail { get; set; } = ReturnalType.Success;
    public object Data { get; set; }
}