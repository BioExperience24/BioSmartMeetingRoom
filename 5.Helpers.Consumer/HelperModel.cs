

using System.Text.Json.Serialization;

namespace _5.Helpers.Consumer;

public class EmailSettingModel
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string MailTo { get; set; }
    public string CCTo { get; set; }
}



public class ReportParameterMulti
{
    public string JudulLaporan { get; set; }
    //public string StartDate { get; set; }
    //public string EndDate { get; set; }
    //public string GeneratedBy { get; set; }
    //public string GeneratedOn { get; set; }
    public List<string> ListChartImg { get; set; }
    public List<string> ListSubjudul { get; set; }
    public List<List<string>> ListHeader { get; set; }
    public List<List<string>> ListDatas { get; set; }
}


public class SMTPSettingModel
{
    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool isSecure { get; set; }
    public int secureSocket { get; set; } = 1;  //Auto-detection secure encryption
    public bool isAuth { get; set; }

}

public class EmailModel
{
    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public List<MailInfo> To { get; set; }
    public List<MailInfo> Cc { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public string FileAttach { get; set; }
    public string[] ListFileAttach { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool isSecure { get; set; }
    public int secureSocket { get; set; } = 1;  //Auto-detection secure encryption
    public bool isAuth { get; set; }

}

public class MailInfo
{
    public string Name { get; set; }
    public string Address { get; set; }

}
public class FastBookListlDataInternalFRViewModel
{
    public string Nik { get; set; }
}
public class FastBookListlDataExternalFRViewModel : FastBookListlDataInternalFRViewModel
{
    public string Email { get; set; }
    public string Company { get; set; }
    public string Name { get; set; }
}