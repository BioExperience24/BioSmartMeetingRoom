using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _7.Entities.Models;
using _8.PAMA.Scheduler.Interface;
using _8.PAMA.Scheduler.Repositories;
using _8.PAMA.Scheduler.ViewModel;

namespace _8.PAMA.Scheduler.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly NotificationConfigRepository _notificationConfigRepository;
        private readonly SettingRuleBookingRepository _settingRuleBookingRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly QueryRepository _queryRepository;
        private readonly IConfiguration _configuration;
        private readonly EntrypassService _entrypassService;

        public SchedulerService(
            BookingRepository bookingRepository,
            IConfiguration configuration,
            SettingRuleBookingRepository settingRuleBookingRepository,
            QueryRepository queryRepository,
            EntrypassService entrypassService,
            NotificationConfigRepository notificationConfigRepository)
        {
            _notificationConfigRepository = notificationConfigRepository;
            _settingRuleBookingRepository = settingRuleBookingRepository;
            _bookingRepository = bookingRepository;
            _queryRepository = queryRepository;
            _entrypassService = entrypassService;
            _configuration = configuration;
        }

        private static string LogCallback(string message)
        {
            var time = DateTime.Now;

            Console.WriteLine($"[{time:yyyy-MM-dd HH:mm:ss}] - {message}");

            return message;
        }

        public async Task<EntryPassResponse> GetTokenEntrypassAsync()
        {
            await _entrypassService.EnsureTokenAsync();
            var response = _entrypassService.GetToken();
            
            return new EntryPassResponse
            {
                Data = new EntryPassData
                {
                    Token = response
                }
            };
        }

        public async Task<bool> CheckReminderBeforeAsync()
        {
            var data = await _bookingRepository.GetBookingReminderBeforeStartAsync();
            var ruleConfig = await _settingRuleBookingRepository.GetData();

            if (data == null)
            {
                LogCallback("Data is null");
                return false;
            }

            if (ruleConfig == null)
            {
                LogCallback("Setting rule is null");
                return false;
            }

            var timenow = DateTime.Now;
            int ruleTime = ruleConfig.MaxEndMeeting == null ? 0 : (int)ruleConfig.MaxEndMeeting;
            int ruleTime5 = (int)ruleTime + 20;
            Dictionary<string, bool> _reminderOfDayBefore = new Dictionary<string, bool>();

            foreach (var row in data)
            {
                if (row.Start == null) continue;

                DateTime timebooking = row.Start.Value.ToUniversalTime();
                var duration = timebooking - timenow;
                var durMinutes = duration.TotalMinutes;

                if (durMinutes >= ruleTime && durMinutes <= ruleTime5)
                {
                    if (row.BookingId != null && row.BookingId != "" && _reminderOfDayBefore.ContainsKey(row.BookingId)) continue;

                    LogCallback("reminder: " + row.Title);
                    _reminderOfDayBefore.Add(row.BookingId, true);

                    LogCallback("berhasil debug :> send reminder meeting " + row.Title);
                }
            }

            return true;
        }

        public async Task<bool> BookingServicesNotifBeforeEndAsync()
        {
            LogCallback("log :> run notif reminder last time of meeting ");

            var ruleConfig = await _settingRuleBookingRepository.GetData();

            if (ruleConfig == null)
            {
                LogCallback("Setting rule is null");
                return false;
            }

            var notif = await _bookingRepository.GetBookingNotifBeforeEndAsync(ruleConfig.MaxEndMeeting == null ? 0 : (int)ruleConfig.MaxEndMeeting);

            if (notif == null)
            {
                LogCallback("Data is null");
                return false;
            }

            var reminderBeforeEnd = new List<string>();

            foreach (var row in notif)
            {
                if (reminderBeforeEnd.Contains(row.BookingId ?? ""))
                {
                    continue;
                }

                var startTime = row.Start;
                var timeBooking = startTime; // Jika butuh pakai DateTime parsing, bisa gunakan DateTime.Parse(row.Start.ToString())

                reminderBeforeEnd.Add(row.BookingId!);
            }

            return true;
        }

        public async Task<bool> BookingServicesExpiresAsync()
        {
            LogCallback("log :> run services expired");
            var past = await _bookingRepository.CheckExpiredMeetingPastAsync();
            var today = await _bookingRepository.CheckExpiredMeetingTodayAsync();

            if (past.Count > 0)
            {
                foreach (var i in past)
                {
                    await _bookingRepository.PostExpiredAsync(i.BookingId);
                }
            }

            if (today.Count > 0)
            {
                foreach (var i in today)
                {
                    await _bookingRepository.PostExpiredAsync(i.BookingId);
                }
            }

            return true;
        }

        public async Task<bool> CheckReminderMeetingUnusedAsync()
        {
            LogCallback("log :> run activity unused meeting");

            var data = await _bookingRepository.GetBookingReminderUnusedAsync();
            var ruleConfig = await _settingRuleBookingRepository.GetData();

            if (data == null || !data.Any() || ruleConfig == null)
            {
                LogCallback("Data, notification config, or rule config is missing");
                return false;
            }

            var timenow = DateTime.UtcNow;

            foreach (var row in data)
            {
                if (string.IsNullOrWhiteSpace(row.BookingId) || row.BookingId == "null")
                    continue;

                if (row.Start == null)
                    continue;

                var ckCheckInConfiguration = CheckAdvanceCheckin(row);
                if (!ckCheckInConfiguration)
                    continue;

                var datacheckinAll = await _bookingRepository.GetCkinRoomBooking(row.BookingId);
                var datacheckinPic = await _bookingRepository.GetCkinRoomBookingPic(row.BookingId);

                int countAll = datacheckinAll.Count;
                int countPic = datacheckinPic.Count;

                var configPermissionCheckin = row.ConfigPermissionCheckinRoom;
                var configReleaseRoomCheckinTimeout = row.ConfigReleaseRoomCheckinTimeout ?? 0;

                var startUtc = row.Start.Value.ToUniversalTime();
                var diffFromStart = (timenow - startUtc).TotalMinutes;

                if (diffFromStart > configReleaseRoomCheckinTimeout)
                {
                    if (configPermissionCheckin == "pic" && countPic <= 0)
                    {
                        await _bookingRepository.PostExpiredUnusedAsync(row.BookingId);
                        LogCallback($"debug :> Unused meeting {row.Title}, organizer/pic/host not checkin. Room auto-released.");
                    }
                    else if (configPermissionCheckin == "all" && countAll <= 0)
                    {
                        await _bookingRepository.PostExpiredUnusedAsync(row.BookingId);
                        LogCallback($"debug :> Unused meeting {row.Title}, attendance not checkin. Room auto-released.");
                    }
                }
            }

            return true;
        }

        public async Task<bool> CheckMeetingTodayAccessAsync()
        {
            // var apiUrl = entrypass.UrlApi + entrypass.PathAddAccess;
            var apiUrl = _configuration["EntryPass:UrlApi"] + _configuration["EntryPass:PathAddAccess"];
            Console.WriteLine($"url: {apiUrl}");
            LogCallback("run checkMeetingToday");

            var datenowUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var waktuAccess = 10; // dalam menit

            var data = await _bookingRepository.OpenDataMeetingAccessAsync();
            if (data == null || !data.Any())
            {
                LogCallback("openDataMeetingAccess is null");
                return false;
            }

            await _entrypassService.EnsureTokenAsync();
            var tokenEntrypass = _entrypassService.GetToken();

            foreach (var row in data)
            {
                // var action = false;

                var datemeetingStartUtc = row.Start;
                var datemeetingAccess = datemeetingStartUtc.AddMinutes(-waktuAccess);
                var datemeetingUnix = new DateTimeOffset(datemeetingAccess).ToUnixTimeSeconds();

                if (datenowUnix >= datemeetingUnix)
                {
                    var bookingId = row.BookingId;
                    if (string.IsNullOrWhiteSpace(bookingId) || bookingId == "null")
                    {
                        LogCallback("bookingId is null");
                    };

                    var invitations = await _queryRepository.SelectBookingInvitation(bookingId);

                    if (invitations == null || !invitations.Any())
                    {
                        LogCallback("invitations is null");
                        continue;
                    }

                    foreach (var inv in invitations)
                    {

                        Console.WriteLine($"inv: {System.Text.Json.JsonSerializer.Serialize(inv)}");

                        var employeeName = inv.Name;
                        var roomName = row.RoomName;
                        var groupAccess = row.AccessGroup;
                        var staffNo = inv.StaffNo;
                        var cardNo = inv.CardNumber;

                        var headers = new Dictionary<string, string>
                        {
                            { "Authorization", $"Bearer {tokenEntrypass}" },
                            { "Content-Type", "application/json" }
                        };

                        var payload = new
                        {
                            staffNo,
                            cardNo,
                            roomAccess = groupAccess
                        };

                        var response = await _entrypassService.SendPostAsync<EntryPassGenericResponse>(apiUrl, payload, headers);

                        // Console.WriteLine($"Result: {System.Text.Json.JsonSerializer.Serialize(response)}");

                        var datetimelog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (response?.Error != null)
                        {
                            var msg = $"ERROR Access Card for {employeeName} to {roomName} at {datetimelog} {response.Error}";
                            LogCallback(msg);
                            continue;
                        }
                        else
                        {
                            var msg = $"Allow Access Card for {employeeName} to {roomName} at {datetimelog}";
                            LogCallback(msg);
                        }

                        // update execute door access
                        await _queryRepository.UpdateExecuteDoorAccess(inv.Id);

                        // update execute door access berdasarkan hari ini
                        await _queryRepository.UpdateExecuteDoorAccesssByBookingDate(today);

                        await Task.Delay(1000);
                    }
                }
            }

            return true;
        }

        public async Task<bool> CheckMeetingAfterTodayAccessAsync()
        {
            var apiUrl = _configuration["EntryPass:UrlApi"] + _configuration["EntryPass:PathDeleteAccess"];
            LogCallback($"run checkMeetingAfterToday {DateTime.Now:yyyy-MM-dd HH:mm}");

            var nowUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var dataSetting = await _settingRuleBookingRepository.GetData();
            int waktuAccess = dataSetting?.NotifUnuseBeforeMeeting == null ? 5 : (int)dataSetting.NotifUnuseBeforeMeeting;

            var data = await _bookingRepository.OpenDataMeetingEndAccessAsync();
            if (data == null || data.Count == 0)
            {
                LogCallback("checkMeetingAfterTodayAccess() null");
                return false;
            }

            await _entrypassService.EnsureTokenAsync();
            var tokenEntrypass = _entrypassService.GetToken();
            if (string.IsNullOrEmpty(tokenEntrypass))
            {
                LogCallback("Token Entrypass is empty");
                return false;
            }

            foreach (var row in data)
            {
                var meetingEnd = (row.EndEarlyMeeting == 1 && row.EarlyEndedAt != null)
                ? row.EarlyEndedAt
                : row.End;

                if (meetingEnd == null)
                {
                    continue;
                }
                
                var meetingWithExtension = meetingEnd.Value.AddMinutes(waktuAccess);
                var accessCutoff = meetingWithExtension.AddMinutes(waktuAccess);
                var accessUnix = new DateTimeOffset(accessCutoff).ToUnixTimeSeconds();

                if (nowUnix >= accessUnix)
                {
                    var invitations = await _queryRepository.SelectBookingInvitation(row.BookingId);
                    var anyFailed = false;

                    foreach (var inv in invitations.Where(i => i.ExecuteDoorAccess == true))
                    {
                        var payload = new
                        {
                            staffNo = inv.StaffNo,
                            cardNo = inv.CardNumber,
                            roomAccess = row.AccessGroup
                        };

                        var headers = new Dictionary<string, string>
                        {
                            { "Authorization", $"Bearer {tokenEntrypass}" },
                            { "Content-Type", "application/json" }
                        };

                        var result = await _entrypassService.SendPostAsync<EntryPassGenericResponse>(apiUrl, payload, headers);
                        string nowLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        var msg = result?.Error != null
                            ? $"ERROR Access Card for {inv.Name} to {row.RoomName} at {nowLog} - {result.Error}"
                            : $"Allow Access Card for {inv.Name} to {row.RoomName} at {nowLog}";

                        LogCallback(msg);

                        await _queryRepository.InsertLogServicesAccessDoorAsync(
                            row.BookingId,
                            row.RoomId ?? "",
                            inv.PinRoom ?? "",
                            inv.Nik,
                            inv.CardNumber,
                            msg,
                            nowLog
                        );

                        if (result?.Error != null)
                        {
                            anyFailed = true;
                            continue;
                        }

                        await _queryRepository.UpdateExecuteDoorAccess(inv.Id);
                        await Task.Delay(1000);
                    }

                    if (!anyFailed)
                    {
                        await _bookingRepository.UpdateAccessTriggerAsync(row.BookingId);
                        LogCallback($"clear access {row.BookingId} {row.RoomName}");
                    }
                }
            }

            return true;
        }

        private bool CheckAdvanceCheckin(BookingReminder row)
        {
            var isConfigSettingEnable = row.IsConfigSettingEnableRoom;
            var isEnableCheckin = row.IsEnableCheckinRoom;
            var isReleaseCheckinTimeout = row.IsRealeaseCheckinTimeoutRoom;

            if (
                isConfigSettingEnable == 0 ||
                isConfigSettingEnable == null
            )
            {
                return false;
            }

            if (isEnableCheckin == 0 || isEnableCheckin == null)
            {
                return false;
            }

            if (
                isReleaseCheckinTimeout == 0 ||
                isReleaseCheckinTimeout == null
            )
            {
                return false;
            }

            return true;
        }

    }
}