﻿namespace _5.Helpers.Consumer.EnumType;

public static class Gender
{
    public const string Male = "MALE";
    public const string Female = "FEMALE";
    public const string Other = "OTHER";
}

public static class DefaultPin
{
    public const string Id1 = "667928";
    public const string Id2 = "882915";
    public const string Id3 = "882914";
    public const string Id4 = "882913";
    public const string Id5 = "882912";
    public const string Id6 = "882911";
    public const string Id7 = "528974";

    public static readonly string[] Values =
    {
        Id1, Id2, Id3, Id4, Id5, Id6, Id7
    };
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
    public const string IntAlarm = "module_int_alarm";
    public const string Loker = "module_loker";
    public const string Invoice = "module_invoice";
    public const string Email = "module_email";


}
public static class EnumPantryTransaksiOrderStatus
{
    public const int Entry = 0;
    public const int Process = 1;
}

public static class EnumPantryTransaksiPushStatus
{
    public const int NotYetProcess = 0;
    public const int Process = 1;
    public const int Delivered = 2;
    public const int Complete = 3;
    public const int Canceled = 4;
    public const int Reject = 5;
}

public static class EnumLevelRole
{
    public const int Administrator = 1;
    public const int Employee = 2;
    public const int EmployeeOld = 3;
    public const int PantryDisplay = 4;
    public const int PantryOperator = 5;
    public const int SOHelpdesk = 6;
    public const int SuperAdmin = 7;
    
}

public static class EnumBookingTypeRoom
{
    public const string AllRoom = "allroom";
    public const string Receptionist = "receptionist";
}
public static class EnumAccessControlType
{
    public const string Falco = "falco";
    public const string FalcoId = "falcoid";
    public const string CustId = "custid";
    public const string EntryPass = "entrypass";
    public const string EntryPassId = "entrypassid";
    public const string Custom = "custom";
}


public static class EnumAccessControlModelControl
{
    public const string Reader = "reader";
    public const string FaceReader = "face_reader";
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
    public const string UnAuthorized = "Token must be provided";
}

public static class HelpItGaStatus
{
    public const string Pending = "pending";
    public const string Process = "process";
    public const string Reject = "reject";
    public const string Done = "done";
}

public static class HelpItGaType
{
    public const string IT = "it";
    public const string GA = "ga";
}

public static class HelpItGaProblemReason
{
    public const string Comfort = "comfort";
    public const string Connection = "connection";
    public const string Facility = "facility";
}

public static class ApprovalHead
{
    public const int PENDING = 0;
    public const int ACCEPT = 1;
    public const int REJECT = 2;
}
