

using _4.Data.ViewModels;

public class ManualConfigViewModel : BaseViewModel
{
    public string ConfigName { get; set; }
    public string ConfigValue { get; set; }
    public string ConfigUnit { get; set; }
    //public string[] ConfigArray { get; set; }
    //public string Note { get; set; }
}

public class Dropdown
{
    public string label { get; set; }
    public string value { get; set; }
}


public class OptionEnum : Dropdown
{
    public string note { get; set; }
    public string api { get; set; }
}

public class DropdownInt
{
    public string label { get; set; }
    public int value { get; set; }  // Value diganti jadi integer
}
