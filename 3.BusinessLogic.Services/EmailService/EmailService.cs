using _4.Helpers.Consumer;
using _5.Helpers.Consumer._EmailService;
using _5.Helpers.Consumer.Custom;

namespace _3.BusinessLogic.Services.EmailService
{
    public class EmailService(
        IConfiguration config,
        APICaller apiCaller,
        BookingRepository bookingRepo,
        ModuleBackendRepository moduleBackendRepo,
        CompanyRepository companyRepo,
        PantryTransaksiRepository pantryTransaksiRepo
    ) 
        : IEmailService
    {
        private const string DefaultCompanyName = "PT Pamapersada Nusantara";
        private readonly string LinkApproval = config["EmailService:LinkApproval"] ?? throw new ArgumentNullException("link approval is not defined in environment variables");
        private readonly string emailServiceUrl = config["EmailService:Url"] ?? throw new ArgumentNullException("email service url is not defined in environment variables");

        public async Task SendMailAttendanceConfirmation(string bookingId, string nik, int attendanceStatus)
        {
            var company = await getInformationCompany();
            if (company == null)
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
                SincerelyName = company.Name ?? DefaultCompanyName
            };

            string emailBody = buildEmailAttendanceConfirmationBody(emailBodyData);
            
            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name =  company.Name ?? DefaultCompanyName,
                To = host.Email,
                Subject = "Konfirmasi Kehadiran",
                Body = emailBody,
                IsHtml = true,
                EmailType = attendanceStatus == 1 ? "attend" : "not attend",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Attendees = new SendMailVMAttendees
                {
                    Id = participant.Id ?? 0,
                    Name =  participant.Name ?? string.Empty,
                    Email = participant.Email ?? string.Empty,
                    Type = participant.Internal == 1 ? "internal" : "external"
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            using (var httpClient = new HttpClient())
            {
                await apiCaller.POSTHttpRequest(url, payload, headers);
            }

            return;
        }
    
        public async Task SendMailInvitation(string bookingId, List<string>? emails = null)
        {
            var company = await getInformationCompany();
            if (company == null)
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
                Organizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                // Pin = string.Empty,
                // Qrtattendance = string.Empty,
                TanggalText = emailBodyDataSetting?.DateText ?? "Tanggal",
                LocationText = emailBodyDataSetting?.DetailLocation ?? "Lokasi",
                RoomText = emailBodyDataSetting?.Room ?? "Ruangan",
                Url = emailBodyDataSetting?.MapLinkText ?? "Direction Map",
                AgendaText = emailBodyDataSetting?.TitleAgendaText ?? "Agenda",
                HormatKami = company.Name ?? DefaultCompanyName
            };

            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name = company.Name ?? DefaultCompanyName,
                Subject = $"{emailBodyDataSetting?.TitleOfText ?? "Undangan Meeting"} {data.Agenda}",
                IsHtml = true,
                EmailType = "meeting",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            var participants = data.Participants;

            if (emails != null && emails.Any())
            {
                participants = participants.Where(q => emails.Contains(q.Email)).DistinctBy(p => p.Email).ToList();
            }

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in participants)
                {
                    emailBodyData.Kepada = participant.Name ?? string.Empty;
                    emailBodyData.Pin = participant.PinRoom ?? string.Empty;

                    if (string.IsNullOrEmpty(participant.Email))
                    {
                        continue;
                    }

                    payload.To = participant.Email;
                    payload.Body = buildEmailInvitationBody(emailBodyData);
                    payload.Attendees = new SendMailVMAttendees
                    {
                        Id = participant.Id ?? 0,
                        Name =  participant.Name ?? string.Empty,
                        Email = participant.Email ?? string.Empty,
                        Type = participant.Internal == 1 ? "internal" : "external"
                    };

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailInvitationRecurring(string recurringId, List<string>? emails = null)
        {
            var company = await getInformationCompany();
            if (company == null)
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
                Organizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                // Pin = string.Empty,
                // Qrtattendance = string.Empty,
                TanggalText = emailBodyDataSetting?.DateText ?? "Tanggal",
                LocationText = emailBodyDataSetting?.DetailLocation ?? "Lokasi",
                RoomText = emailBodyDataSetting?.Room ?? "Ruangan",
                Url = emailBodyDataSetting?.MapLinkText ?? "Direction Map",
                AgendaText = emailBodyDataSetting?.TitleAgendaText ?? "Agenda",
                HormatKami = company.Name ?? DefaultCompanyName
            };

            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name = company.Name ?? DefaultCompanyName,
                Subject = $"{emailBodyDataSetting?.TitleOfText ?? "Undangan Meeting"} {data.Agenda}",
                IsHtml = true,
                EmailType = "meeting",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            var participants = data.Participants;

            if (emails != null && emails.Any())
            {
                participants = participants.Where(q => emails.Contains(q.Email)).DistinctBy(p => p.Email).ToList();
            }

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in participants)
                {
                    emailBodyData.Kepada = participant.Name ?? string.Empty;
                    emailBodyData.Pin = participant.PinRoom ?? string.Empty;
                    
                    if (string.IsNullOrEmpty(participant.Email))
                    {
                        continue;
                    }

                    payload.To = participant.Email;
                    payload.Body = buildEmailInvitationBody(emailBodyData);
                    payload.Attendees = new SendMailVMAttendees
                    {
                        Id = participant.Id ?? 0,
                        Name =  participant.Name ?? string.Empty,
                        Email = participant.Email ?? string.Empty,
                        Type = participant.Internal == 1 ? "internal" : "external"
                    };

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailReschedule(string bookingId)
        {
            // var getHttpUrl = await moduleBackendRepo.GetHttpUrlTop();
            // if (getHttpUrl == null)
            // {
            //     return;    
            // }

            var company = await getInformationCompany();
            if (company == null)
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
                Organizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
                HormatKami = company.Name ?? DefaultCompanyName
            };

            // var url = getHttpUrl.Url;
            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name = company.Name ?? DefaultCompanyName,
                // Subject = $"Undangan Meeting {data.Agenda}",
                Subject = $"Perubahan Jadwal Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "reschedule",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            foreach (var participant in data.Participants)
            {
                emailBodyData.Kepada = participant.Name ?? string.Empty;
                emailBodyData.Pin = participant.PinRoom ?? string.Empty;

                if (string.IsNullOrEmpty(participant.Email))
                {
                    continue;
                }

                payload.To = participant.Email;
                payload.Body = buildEmailRescheduleBody(emailBodyData);
                payload.Attendees = new SendMailVMAttendees
                {
                    Id = participant.Id ?? 0,
                    Name =  participant.Name ?? string.Empty,
                    Email = participant.Email ?? string.Empty,
                    Type = participant.Internal == 1 ? "internal" : "external"
                };

                using (var httpClient = new HttpClient())
                {
                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailCancellation(string bookingId)
        {
            var company = await getInformationCompany();
            if (company == null)
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
                Organizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
                HormatKami = company.Name ?? DefaultCompanyName
            };

            // var url = getHttpUrl.Url;
            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name = company.Name ?? DefaultCompanyName,
                Subject = $"Pembatalan Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "cancel",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in data.Participants)
                {
                    emailBodyData.Kepada = participant.Name ?? string.Empty;
                    emailBodyData.Pin = participant.PinRoom ?? string.Empty;

                    if (string.IsNullOrEmpty(participant.Email))
                    {
                        continue;
                    }

                    payload.To = participant.Email;
                    payload.Body = buildEmailCancellationBody(emailBodyData);
                    payload.Attendees = new SendMailVMAttendees
                    {
                        Id = participant.Id ?? 0,
                        Name =  participant.Name ?? string.Empty,
                        Email = participant.Email ?? string.Empty,
                        Type = participant.Internal == 1 ? "internal" : "external"
                    };

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }

        public async Task SendMailCancellationRecurring(string recurringId)
        {
            var company = await getInformationCompany();
            if (company == null)
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
                Organizer = host.Name ?? string.Empty,
                Agenda = data.Agenda,
                TanggalText = "Tanggal",
                LocationText = "Lokasi",
                RoomText = "Ruangan",
                Url = "Direction Map",
                AgendaText = "Agenda",
                HormatKami = company.Name ?? DefaultCompanyName
            };

            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name = company.Name ?? DefaultCompanyName,
                Subject = $"Pembatalan Meeting {data.Agenda}",
                IsHtml = true,
                EmailType = "cancel",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            using (var httpClient = new HttpClient())
            {
                foreach (var participant in data.Participants)
                {
                    emailBodyData.Kepada = participant.Name ?? string.Empty;
                    emailBodyData.Pin = participant.PinRoom ?? string.Empty;

                    if (string.IsNullOrEmpty(participant.Email))
                    {
                        continue;
                    }

                    payload.To = participant.Email;
                    payload.Body = buildEmailCancellationBody(emailBodyData);
                    payload.Attendees = new SendMailVMAttendees
                    {
                        Id = participant.Id ?? 0,
                        Name =  participant.Name ?? string.Empty,
                        Email = participant.Email ?? string.Empty,
                        Type = participant.Internal == 1 ? "internal" : "external"
                    };

                    await apiCaller.POSTHttpRequest(url, payload, headers);
                }
            }

            return;
        }
        
        public async Task SendMailNotifApprovalOrder(string bookingId, string pantryTransaksiId)
        {
            if (string.IsNullOrEmpty(bookingId) || string.IsNullOrEmpty(pantryTransaksiId))
            {
                return;
            }

            var company = await getInformationCompany();
            if (company == null)
            {
                return;
            }

            var data = await bookingRepo.GetMailDataParticipantByBookingId(bookingId);

            if (data == null)
            {
                return;
            }

            var host = data.Participants.Where(q => q.IsPic == 1).FirstOrDefault();
            if (host == null)
            {
                return;
            }

            var pantry = await pantryTransaksiRepo.GetMailDataOrderAsync(bookingId, pantryTransaksiId);
            if (pantry == null || pantry.HeadEmployeeId == null)
            {
                return;
            }

            string pantryDetailList = "";
            if (pantry.Detail.Any())
            {
                int listNo = 1;
                foreach (var item in pantry.Detail)
                {
                    pantryDetailList += $@"
                        <tr><td style='text-align: center; padding: 3px; padding: 4px;'>{listNo}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Name}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Qty}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Note}</td></tr>";

                    listNo++;
                }
            }


            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMNotifApprovalOrder emailBodyData = new _EmailServiceVMNotifApprovalOrder
            {
                MeetingName = data.Agenda,
                MeetingOrganizer = host.EmployeeName ?? string.Empty,
                MeetingDate = data.Date.ToString("dd MMMM yyyy"),
                MeetingTime = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                MeetingHead = pantry.HeadEmployeeName ?? string.Empty,
                MeetingLocation = meetingRoom,
                MeetingOrderId = pantry.Id ?? string.Empty,
                MeetingOrderList = pantryDetailList,
                MeetingLinkApproval = LinkApproval,
                HormatKami = company.Name ?? DefaultCompanyName
            };

            string emailBody = buildEmailNotifApprovalOrderBody(emailBodyData);
            
            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name =  company.Name ?? DefaultCompanyName,
                To = pantry.HeadEmployeeEmail,
                Subject = "Approval Order",
                Body = emailBody,
                IsHtml = true,
                EmailType = "order",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                },
                HeadEmployee = new SendMailHeadEmployee
                {
                    Id = pantry.HeadEmployeeId ?? string.Empty,
                    Name = pantry.HeadEmployeeName ?? string.Empty,
                    Email = pantry.HeadEmployeeEmail ?? string.Empty,
                    Nrp = pantry.HeadEmployeeNRP ?? string.Empty,
                    NikDisplay = pantry.HeadEmployeeNikDisplay ?? string.Empty,
                    Phone = pantry.HeadEmployeePhone ?? string.Empty,
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            using (var httpClient = new HttpClient())
            {
                await apiCaller.POSTHttpRequest(url, payload, headers);
            }

            return;
        }

        
        public async Task SendMailNotifApprovalOrderRecurring(string recurringId)
        {
            if (string.IsNullOrEmpty(recurringId))
            {
                return;
            }

            var company = await getInformationCompany();
            if (company == null)
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

            var pantry = await pantryTransaksiRepo.GetMailDataOrderRecurringAsync(bookingId, recurringId);
            if (pantry == null || pantry.HeadEmployeeId == null)
            {
                return;
            }

            var orderIds = string.Join(", ", pantry.Ids!);

            string pantryDetailList = "";
            if (pantry.Detail.Any())
            {
                int listNo = 1;
                foreach (var item in pantry.Detail)
                {
                    pantryDetailList += $@"
                        <tr><td style='text-align: center; padding: 3px; padding: 4px;'>{listNo}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Name}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Qty}</td> <td style='text-align: center; padding: 3px; padding: 4px;'>{item.Note}</td></tr>";

                    listNo++;
                }
            }

            var meetingRoom = $"{data.BuildingName}, {data.BuildingFloorName}, {data.RoomName}".Replace(", ,", ",").TrimEnd(',', ' ');

            _EmailServiceVMNotifApprovalOrder emailBodyData = new _EmailServiceVMNotifApprovalOrder
            {
                MeetingName = data.Agenda,
                MeetingOrganizer = host.EmployeeName ?? string.Empty,
                MeetingDate = date,
                MeetingTime = $"{data.Start:HH:mm} - {data.End:HH:mm}",
                MeetingHead = pantry.HeadEmployeeName ?? string.Empty,
                MeetingLocation = meetingRoom,
                MeetingOrderId = orderIds ?? string.Empty,
                MeetingOrderList = pantryDetailList,
                MeetingLinkApproval = LinkApproval,
                HormatKami = company.Name ?? DefaultCompanyName
            };

            string emailBody = buildEmailNotifApprovalOrderBody(emailBodyData, true);
            
            var url = emailServiceUrl;

            var payload = new SendMailViewModel
            {
                Name =  company.Name ?? DefaultCompanyName,
                To = pantry.HeadEmployeeEmail,
                Subject = "Approval Order",
                Body = emailBody,
                IsHtml = true,
                EmailType = "order",
                Date = data.Date.ToString("dd MMMM yyyy"),
                StartMeeting = $"{data.Start:HH:mm}",
                EndMeeting = $"{data.End:HH:mm}",
                Location = data.BuildingAddress,
                Organizer = new SendMailVMOrganizer
                {
                    Id = host.EmployeeId ?? string.Empty,
                    Name = host.EmployeeName ?? string.Empty,
                    Email = host.EmployeeEmail ?? string.Empty,
                    Nrp = host.EmployeeNrp ?? string.Empty,
                    NikDisplay = host.EmployeeNikDisplay ?? string.Empty,
                    Phone = host.EmployeeNoPhone ?? string.Empty,
                },
                Place = new SendMailVMPlace
                {
                    Building = data.BuildingName,
                    Floor = data.BuildingFloorName,
                    Room = data.RoomName,
                    RoomId = data.RoomId,
                    KindRoom = data.RoomType
                },
                HeadEmployee = new SendMailHeadEmployee
                {
                    Id = pantry.HeadEmployeeId ?? string.Empty,
                    Name = pantry.HeadEmployeeName ?? string.Empty,
                    Email = pantry.HeadEmployeeEmail ?? string.Empty,
                    Nrp = pantry.HeadEmployeeNRP ?? string.Empty,
                    NikDisplay = pantry.HeadEmployeeNikDisplay ?? string.Empty,
                    Phone = pantry.HeadEmployeePhone ?? string.Empty,
                }
            };
            
            // Ensure headers are not null or empty before deserialization
            Dictionary<string, string> headers = getHeaders();

            using (var httpClient = new HttpClient())
            {
                await apiCaller.POSTHttpRequest(url, payload, headers);
            }

            return;
        }

        private Dictionary<string, string> getHeaders()
        {
            var headersJson = config["EmailService:Headers"];
            if (headersJson == null)
            {
                return new Dictionary<string, string>();
            }
            return deserializeHeaders(headersJson);
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
                { "%orginizer%", emailBodyData.Organizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%pin%", emailBodyData.Pin },
                { "%qrtattendance%", string.Empty },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText },
                { "%hormat_kami%", emailBodyData.HormatKami }
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
                { "%attendance_status%", emailBodyData.AttendanceStatus },
                { "%sincerely_name%", emailBodyData.SincerelyName }
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
                { "%orginizer%", emailBodyData.Organizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%pin%", emailBodyData.Pin },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText },
                { "%hormat_kami%", emailBodyData.HormatKami }
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
                { "%orginizer%", emailBodyData.Organizer },
                { "%agenda%", emailBodyData.Agenda },
                { "%tanggal_text%", emailBodyData.TanggalText },
                { "%location_text%", emailBodyData.LocationText },
                { "%room_text%", emailBodyData.RoomText },
                { "%url%", emailBodyData.Url },
                { "%agenda_text%", emailBodyData.AgendaText },
                { "%hormat_kami%", emailBodyData.HormatKami }
            };

            var sb = new StringBuilder(emailBodyTemplate);
            foreach (var placeholder in placeholders)
            {
                sb.Replace(placeholder.Key, placeholder.Value);
            }

            // return sb.ToString();
            return Regex.Replace(sb.ToString(), @">\s+<", "><");
        }

        private string buildEmailNotifApprovalOrderBody(_EmailServiceVMNotifApprovalOrder emailBodyData, bool isRecurring = false)
        {
            string typeOfMail = _EmailServiceTypeOfMail.ORDER;

            if (isRecurring)
            {
                typeOfMail = _EmailServiceTypeOfMail.ORDER_RECURRING;
            }

            var emailBodyTemplate = _EmailService.LoadTemplate(typeOfMail);
            if (string.IsNullOrEmpty(emailBodyTemplate))
            {
                throw new InvalidOperationException("Email template is empty or null.");
            }
            var placeholders = new Dictionary<string, string>
            {
                { "%meeting_name%", emailBodyData.MeetingName},
                { "%meeting_organizer%", emailBodyData.MeetingOrganizer},
                { "%meeting_date%", emailBodyData.MeetingDate},
                { "%meeting_time%", emailBodyData.MeetingTime},
                { "%meeting_head%", emailBodyData.MeetingHead},
                { "%meeting_location%", emailBodyData.MeetingLocation},
                { "%meeting_orderid%", emailBodyData.MeetingOrderId},
                { "%meeting_order_list%", emailBodyData.MeetingOrderList},
                { "%meeting_link_approval%", emailBodyData.MeetingLinkApproval},
                { "%hormat_kami%", emailBodyData.HormatKami }
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
    
        private async Task<Company?> getInformationCompany()
        {
            var company = await companyRepo.GetOneItemAsync();
            if (company == null)
            {
                return null;
            }
            return company;
        }
    }
}