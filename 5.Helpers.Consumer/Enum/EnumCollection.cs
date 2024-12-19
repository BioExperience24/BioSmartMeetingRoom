namespace _5.Helpers.Consumer.EnumType;

public static class Gender
{
    public const string Male = "MALE";
    public const string Female = "FEMALE";
    public const string Other = "OTHER";
}
public static class EnumStatus
{
    public const string Failed = "FAILED";
    public const string Success = "SUCCESS";
    public const string NotRunning = "NOT RUNNING";
}

public static class EnumMessage
{
    public const string Auto = "Automated by System";
    public const string Manual = "Manual Pull";
}
public static class ModuleBackendTextModule
{
    
    public const string Automation = "module_automation";
    public const string Price = "module_price";
    public const string Int365 = "module_int_365";
    public const string IntGoogle = "module_int_google";
    public const string UserVIP = "module_user_vip";
    public const string RoomAdvance = "module_room_advance";
    public const string Pantry = "module_pantry";
    

}



public enum AlarmLevel : int
{
    None,
    Medium,
    High
}


public enum NotificationType : int
{
    NeedApproval,
    AfterApproval,
    Rejection,
    FinishApproved
}



public static class TaskType
{
    public const string Initiate = "initiate";
    public const string Report = "report";
    public const string Email = "email";
    public const string UserAct = "user_act";
    public const string Nihil = "nihil";
    public const string ChangeRecord = "change_record";
    public const string APICall = "apiCall";
    public const string Rejected = "rejected";
    public const string Finish = "finish";
    public const string InitiateCopy = "initiateCopy";
}

public static class CustomAlarmProperty
{
    public const string CurrentData = "curr_val";
    public const string TotalAlarm = "total";
    public const string WarningAlarm = "warning";
    public const string InformationalAlarm = "informational";
    public const string FatalAlarm = "fatal";
}

public static class SchedulerStatus
{
    public const string Active = "active";
    public const string Waiting = "waiting";
    public const string Running = "running";
    public const string Success = "success";
    public const string Error = "error";
    public const string ErrorRunMessage = "Error run schedule";
    public const string NotActive = "notactive";
    public const string LOG = "LOG";
}

public static class SchedulerIdentifier
{
    public const string Test = "test";
    public const string PUSH_NOTIF_DASHBOARD = "PUSH_NOTIF_DASHBOARD";
    public const string Report = "report";
    public const string Purge = "purge";
    public const string ITA = "ita";
    public const string DCE = "dce";
    public const string Formula = "formula";
    public const string Alarm = "alarm";
    public const string HistorySensor = "history_sensor";
    public const string HistoryTemporary = "history_temporary";
    public const string HistoryUSpace = "history_uspace";
    public const string JobMonitoring = "job_monitoring";
    public const string RunningScheduler = "RUNNING_SCHEDULER";
}

public static class ReportUspaceType
{
    public const string PowerPerCustomerType = "Power per Customer Type";
    public const string PowerBilling = "Power Billing";
    public const string PowerByCustomerPerLocation = "Power by Customer per Location";
    public const string PowerByCustomer = "Power by Customer";
    public const string CustomerTrend = "Customer Trend";
    public const string AvailableRacks = "Available Racks";
    public const string RackUtilizationByCustomer = "Rack Utilization by Customer";
    public const string InternalRacksPerBU = "Internal Racks per BU";
    public const string CustomerEquipmentDetails = "Customer Equipment Details";
}


public enum TimeZoneEnum
{
    Dateline_Standard_Time, // (UTC-12:00) International Date Line West
    Samoa_Standard_Time, // (UTC-11:00) Coordinated Universal Time-11
    Hawaiian_Standard_Time, // (UTC-10:00) Hawaii
    Alaskan_Standard_Time, // (UTC-09:00) Alaska
    Pacific_Standard_Time, // (UTC-08:00) Pacific Time (US & Canada)
    Mountain_Standard_Time, // (UTC-07:00) Mountain Time (US & Canada)
    Central_Standard_Time, // (UTC-06:00) Central Time (US & Canada)
    Eastern_Standard_Time, // (UTC-05:00) Eastern Time (US & Canada)
    Atlantic_Standard_Time, // (UTC-04:00) Atlantic Time (Canada)
    Newfoundland_Standard_Time, // (UTC-03:30) Newfoundland
    Argentina_Standard_Time, // (UTC-03:00) City of Buenos Aires
    Greenwich_Standard_Time, // (UTC+00:00) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London
    Central_Europe_Standard_Time, // (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague
    W_Europe_Standard_Time, // (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna
    E_Europe_Standard_Time, // (UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius
    Russian_Standard_Time, // (UTC+03:00) Moscow, St. Petersburg
    Arabian_Standard_Time, // (UTC+04:00) Abu Dhabi, Muscat
    West_Asia_Standard_Time, // (UTC+05:00) Tashkent
    India_Standard_Time, // (UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi
    SE_Asia_Standard_Time, // (UTC+07:00) Bangkok, Hanoi, Jakarta
    China_Standard_Time, // (UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi
    Tokyo_Standard_Time, // (UTC+09:00) Osaka, Sapporo, Tokyo
    AUS_Central_Standard_Time, // (UTC+09:30) Darwin
    AUS_Eastern_Standard_Time, // (UTC+10:00) Canberra, Melbourne, Sydney
    New_Zealand_Standard_Time // (UTC+12:00) Auckland, Wellington
}

enum WeekDays
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

public static class SeverityPeriorityInt
{
    public const int None = 0;
    public const int CRITICAL = 1;
    public const int WARNING = 2;
    public const int INFORMATIONAL = 3;
    public const int ERROR = 0;
    public const int FAILURE = 0;
    public const int FATAL = 0;
}

public static class ReturnalType
{
    public const string Success = "success";
    public const string Failed = "fail";
    public const string Error = "error";
    public const string BadRequest = "bad_request";
    public const string NotFound = "not_found";
    public const string Forbidden = "forbidden";
    public const string UnderConstruction = "under_construction";
}
