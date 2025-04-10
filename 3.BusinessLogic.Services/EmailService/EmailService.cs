using _4.Helpers.Consumer;
using _5.Helpers.Consumer._EmailService;
using _5.Helpers.Consumer.Custom;

namespace _3.BusinessLogic.Services.EmailService
{
    public class EmailService(
        APICaller apiCaller,
        BookingRepository bookingRepo,
        ModuleBackendRepository moduleBackendRepo
    ) 
        : IEmailService
    {
        public async Task SendMailAttendanceConfirmation(string bookingId, string nik, int attendanceStatus)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId, nik);

            if (data == null)
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var participant = data.Participants.Where(q => q.Nik == nik).FirstOrDefault();
            if (participant == null)
            {
                return;
            }

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMAttendance emailBodyData = new _EmailServiceVMAttendance
            {
                ParticipantName = participant.Name ?? string.Empty,
                MeetingDate = data.Date.ToString("dd MMMM yyyy"),
                MeetingTime = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                MeetingLocation = data.BuildingAddress,
                MeetingRoom = meetingRoom,
                MeetingBuilding = generateLinkMeetingLocation(data.BuildingMapLink),
                MeetingHost = host.Name ?? string.Empty,
                MeetingAgenda = data.Agenda,
                AttendanceStatus = attendanceStatus == 1 ? "bersedia" : "tidak bersedia",
            };

            string emailBody = buildEmailAttendanceConfirmationBody(emailBodyData);
            
            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                To = host.Email,
                Subject = "Konfirmasi Kehadiran",
                Body = emailBody,
                IsHtml = true,
                EmailType = attendanceStatus == 1 ? "attend" : "not attend",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            using (var httpClient = new HttpClient())
            {
                await apiCaller.POSTHttpRequest(url, payload, headers);
            }

            return;
        }
    
        public async Task SendMailInvitation(string bookingId, List<string>? emails = null)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null || !data.Participants.Any())
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var emailBodyDataSetting = await moduleBackendRepo.GetTemplateEmailSettingByType(_EmailServiceTypeOfMail.INVITATION);

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMInvitation emailBodyData = new _EmailServiceVMInvitation
            {
                // Kepada = string.Empty,
                Tanggal = data.Date.ToString("dd MMMM yyyy"),
                Waktu = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                Location = data.BuildingAddress,
                Room = meetingRoom,
                LinkMap = generateLinkMeetingLocation(data.BuildingMapLink),
                Orginizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                // Pin = string.Empty,
                // Qrtattendance = string.Empty,
                TanggalText = emailBodyDataSetting?.DateText ?? "Tanggal",
                LocationText = emailBodyDataSetting?.DetailLocation ?? "Lokasi",
                RoomText = emailBodyDataSetting?.Room ?? "Ruangan",
                Url = emailBodyDataSetting?.MapLinkText ?? "Direction Map",
                AgendaText = emailBodyDataSetting?.TitleAgendaText ?? "Agenda",
            };

            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                Subject = $"{emailBodyDataSetting?.TitleOfText ?? "Undangan Meeting"} {data.Agenda}",
                IsHtml = true,
                EmailType = "meeting",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            var participants = data.Participants;

            if (emails != null && emails.Any())
            {
                participants = participants.Where(q => emails.Contains(q.Email)).DistinctBy(p => p.Email).ToList();
            }

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in participants)
                {
                    emailBodyData.Kepada = participant.Name;
                    emailBodyData.Pin = participant.PinRoom ?? "";

                    payload.To = participant.Email;
                    payload.Body = buildEmailInvitationBody(emailBodyData);

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailInvitationRecurring(string recurringId, List<string>? emails = null)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var bookRecurring = await bookingRepo.GetFirstAndLastByRecurringId(recurringId);

            if (!bookRecurring.Any())
            {
                return;
            }

            Booking bookFirst = bookRecurring.First();
            
            Booking bookLast = bookRecurring.Last();

            string date = bookFirst.Date.ToString("dd MMMM yyyy");
            if (bookFirst.Date != bookLast.Date)
            {
                date = $"{bookFirst.Date.ToString("dd MMMM yyyy")} - {bookLast.Date.ToString("dd MMMM yyyy")}";
            }

            string bookingId = bookFirst.BookingId;

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null || !data.Participants.Any())
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var emailBodyDataSetting = await moduleBackendRepo.GetTemplateEmailSettingByType(_EmailServiceTypeOfMail.INVITATION);

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMInvitation emailBodyData = new _EmailServiceVMInvitation
            {
                // Kepada = string.Empty,
                Tanggal = date,
                Waktu = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                Location = data.BuildingAddress,
                Room = meetingRoom,
                LinkMap = generateLinkMeetingLocation(data.BuildingMapLink),
                Orginizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                // Pin = string.Empty,
                // Qrtattendance = string.Empty,
                TanggalText = emailBodyDataSetting?.DateText ?? "Tanggal",
                LocationText = emailBodyDataSetting?.DetailLocation ?? "Lokasi",
                RoomText = emailBodyDataSetting?.Room ?? "Ruangan",
                Url = emailBodyDataSetting?.MapLinkText ?? "Direction Map",
                AgendaText = emailBodyDataSetting?.TitleAgendaText ?? "Agenda",
            };

            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                Subject = $"{emailBodyDataSetting?.TitleOfText ?? "Undangan Meeting"} {data.Agenda}",
                IsHtml = true,
                EmailType = "meeting",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            var participants = data.Participants;

            if (emails != null && emails.Any())
            {
                participants = participants.Where(q => emails.Contains(q.Email)).DistinctBy(p => p.Email).ToList();
            }

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in participants)
                {
                    emailBodyData.Kepada = participant.Name;
                    emailBodyData.Pin = participant.PinRoom ?? "";

                    payload.To = participant.Email;
                    payload.Body = buildEmailInvitationBody(emailBodyData);

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailReschedule(string bookingId)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null || !data.Participants.Any())
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMInvitation emailBodyData = new _EmailServiceVMInvitation
            {
                Tanggal = data.Date.ToString("dd MMMM yyyy"),
                Waktu = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                Location = data.BuildingAddress,
                Room = meetingRoom,
                LinkMap = generateLinkMeetingLocation(data.BuildingMapLink),
                Orginizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
            };

            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                // Subject = $"Undangan Meeting {data.Agenda}",
                Subject = $"Perubahan Jadwal Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "reschedule",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            foreach (var participant in data.Participants)
            {
                emailBodyData.Kepada = participant.Name;
                emailBodyData.Pin = participant.PinRoom ?? "";

                payload.To = participant.Email;
                payload.Body = buildEmailRescheduleBody(emailBodyData);

                using (var httpClient = new HttpClient())
                {
                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailCancellation(string bookingId)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null || !data.Participants.Any())
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMInvitation emailBodyData = new _EmailServiceVMInvitation
            {
                Tanggal = data.Date.ToString("dd MMMM yyyy"),
                Waktu = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                Location = data.BuildingAddress,
                Room = meetingRoom,
                LinkMap = generateLinkMeetingLocation(data.BuildingMapLink),
                Orginizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
            };

            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                Subject = $"Pembatalan Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "cancel",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in data.Participants)
                {
                    emailBodyData.Kepada = participant.Name;
                    emailBodyData.Pin = participant.PinRoom ?? "";

                    payload.To = participant.Email;
                    payload.Body = buildEmailCancellationBody(emailBodyData);

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailCancellationRecurring(string recurringId)
        {
            var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            if (getHttpUrl == null)
            {
                return;    
            }

            var bookRecurring = await bookingRepo.GetFirstAndLastByRecurringId(recurringId);

            if (!bookRecurring.Any())
            {
                return;
            }

            Booking bookFirst = bookRecurring.First();
            
            Booking bookLast = bookRecurring.Last();

            string date = bookFirst.Date.ToString("dd MMMM yyyy");
            if (bookFirst.Date != bookLast.Date)
            {
                date = $"{bookFirst.Date.ToString("dd MMMM yyyy")} - {bookLast.Date.ToString("dd MMMM yyyy")}";
            }

            string bookingId = bookFirst.BookingId;

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null || !data.Participants.Any())
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMInvitation emailBodyData = new _EmailServiceVMInvitation
            {
                Tanggal = date,
                Waktu = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                Location = data.BuildingAddress,
                Room = meetingRoom,
                LinkMap = generateLinkMeetingLocation(data.BuildingMapLink),
                Orginizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
            };

            var url = getHttpUrl.Url;

            var payload = new SendMailViewModel
            {
                Name = "Bio Experience",
                Subject = $"Pembatalan Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "cancel",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = deserializeHeaders(getHttpUrl.Headers);

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in data.Participants)
                {
                    emailBodyData.Kepada = participant.Name;
                    emailBodyData.Pin = participant.PinRoom ?? "";

                    payload.To = participant.Email;
                    payload.Body = buildEmailCancellationBody(emailBodyData);

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }
        
        private Dictionary<string, string> deserializeHeaders(string headersJson)
        {
            if (string.IsNullOrEmpty(headersJson))
            {
                return new Dictionary<string, string>();
            }

            try
            {
                var headersDeserialize = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(headersJson);
                return headersDeserialize?.SelectMany(dict => dict).ToDictionary(kv => kv.Key, kv => kv.Value) ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing headers: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }
    
        private string buildEmailInvitationBody(_EmailServiceVMInvitation emailBodyData)
        {
            var emailBodyTemplate = _EmailService.LoadTemplate(_EmailServiceTypeOfMail.INVITATION);
            if (string.IsNullOrEmpty(emailBodyTemplate))
            {
                throw new InvalidOperationException("Email template is empty or null.");
            }
            var placeholders = new Dictionary<string, string>
            {
                { "%kepada%", emailBodyData.Kepada },
                { "%tanggal%", emailBodyData.Tanggal },
                { "%waktu%", emailBodyData.Waktu },
                { "%location%", emailBodyData.Location },
                { "%room%", emailBodyData.Room },
                { "%link_map%", emailBodyData.LinkMap },
                { "%orginizer%", emailBodyData.Orginizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%pin%", emailBodyData.Pin },
                { "%qrtattendance%", string.Empty },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText }
            };

            var sb = new StringBuilder(emailBodyTemplate);
            foreach (var placeholder in placeholders)
            {
                sb.Replace(placeholder.Key, placeholder.Value);
            }

            // return sb.ToString();
            return Regex.Replace(sb.ToString(), @">\s+<", "><");
        }
    
        private string buildEmailAttendanceConfirmationBody(_EmailServiceVMAttendance emailBodyData)
        {
            var emailBodyTemplate = _EmailService.LoadTemplate(_EmailServiceTypeOfMail.ATTENDANCE);
            if (string.IsNullOrEmpty(emailBodyTemplate))
            {
                throw new InvalidOperationException("Email template is empty or null.");
            }
            var placeholders = new Dictionary<string, string>
            {
                { "%participant_name%", emailBodyData.ParticipantName },
                { "%meeting_date%", emailBodyData.MeetingDate },
                { "%meeting_time%", emailBodyData.MeetingTime },
                { "%meeting_location%", emailBodyData.MeetingLocation },
                { "%meeting_room%", emailBodyData.MeetingRoom },
                { "%meeting_building%", emailBodyData.MeetingBuilding },
                { "%meeting_host%", emailBodyData.MeetingHost },
                { "%meeting_agenda%", emailBodyData.MeetingAgenda },
                { "%attendance_status%", emailBodyData.AttendanceStatus }
            };

            var sb = new StringBuilder(emailBodyTemplate);
            foreach (var placeholder in placeholders)
            {
                sb.Replace(placeholder.Key, placeholder.Value);
            }

            // return sb.ToString();
            return Regex.Replace(sb.ToString(), @">\s+<", "><");
        }
    
        private string buildEmailRescheduleBody(_EmailServiceVMInvitation emailBodyData)
        {
            var emailBodyTemplate = _EmailService.LoadTemplate(_EmailServiceTypeOfMail.RESCHEDULE);
            if (string.IsNullOrEmpty(emailBodyTemplate))
            {
                throw new InvalidOperationException("Email template is empty or null.");
            }
            var placeholders = new Dictionary<string, string>
            {
                { "%kepada%", emailBodyData.Kepada },
                { "%tanggal%", emailBodyData.Tanggal },
                { "%waktu%", emailBodyData.Waktu },
                { "%location%", emailBodyData.Location },
                { "%room%", emailBodyData.Room },
                { "%link_map%", emailBodyData.LinkMap },
                { "%orginizer%", emailBodyData.Orginizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%pin%", emailBodyData.Pin },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText }
            };

            var sb = new StringBuilder(emailBodyTemplate);
            foreach (var placeholder in placeholders)
            {
                sb.Replace(placeholder.Key, placeholder.Value);
            }

            // return sb.ToString();
            return Regex.Replace(sb.ToString(), @">\s+<", "><");
        }

        private string buildEmailCancellationBody(_EmailServiceVMInvitation emailBodyData)
        {
            var emailBodyTemplate = _EmailService.LoadTemplate(_EmailServiceTypeOfMail.CANCELLATION);
            if (string.IsNullOrEmpty(emailBodyTemplate))
            {
                throw new InvalidOperationException("Email template is empty or null.");
            }
            var placeholders = new Dictionary<string, string>
            {
                { "%kepada%", emailBodyData.Kepada },
                { "%tanggal%", emailBodyData.Tanggal },
                { "%waktu%", emailBodyData.Waktu },
                { "%location%", emailBodyData.Location },
                { "%room%", emailBodyData.Room },
                { "%link_map%", emailBodyData.LinkMap },
                { "%orginizer%", emailBodyData.Orginizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText }
            };

            var sb = new StringBuilder(emailBodyTemplate);
            foreach (var placeholder in placeholders)
            {
                sb.Replace(placeholder.Key, placeholder.Value);
            }

            // return sb.ToString();
            return Regex.Replace(sb.ToString(), @">\s+<", "><");
        }

        private string generateLinkMeetingLocation(string linkMap)
        {
            return !string.IsNullOrEmpty(linkMap) ? $"<a href='{linkMap}' target='_blank'>Lokasi meeting</a>" : "-";
        }
    }
}