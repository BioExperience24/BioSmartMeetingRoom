using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer._QRCodeGenerator;

namespace _5.Helpers.Consumer.Custom
{
    public class FastBook
    {


        public static DateTime ParseDateTime(DateTime date, string time)
        {
            // Ensure time uses ":" instead of "."
            time = time.Replace(".", ":");

            // Validate time format
            if (!TimeSpan.TryParseExact(time, @"hh\:mm", CultureInfo.InvariantCulture, out _))
            {
                throw new FormatException($"Invalid time format: {time}. Expected format is HH:mm.");
            }

            return DateTime.ParseExact($"{date:yyyy-MM-dd} {time}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        public static DateTime ConvertTimezoneId(DateTime date, string time)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, time!);
        }



        public  static int CalculateDuration(FastBookBookingViewModel databook)
        {
            DateTime startTime = ParseDateTime(databook.Date.ToDateTime(TimeOnly.MinValue), databook.Start.ToString("HH:mm"));
            DateTime endTime = ParseDateTime(databook.Date.ToDateTime(TimeOnly.MinValue), databook.End.ToString("HH:mm"));

            return (int)(endTime - startTime).TotalMinutes;
        }


        public static string GenerateInvoiceNumber(string existingInvoice)
        {
            if (string.IsNullOrEmpty(existingInvoice))
                return "001";

            string[] parts = existingInvoice.Split('/');
            int number = int.Parse(parts[0]) + 1;
            return number.ToString("D3");
        }


        // public static FastBookBookingInvoice CreateBookingInvoice(FastBookBookingViewModel databook, FastBookAlocationVMDefaultFR alocation, long reservationCost, string invoiceFormat)
        // {
        //     return new FastBookBookingInvoice
        //     {
        //         InvoiceNo = _Random.Numeric(10, true).ToString(),
        //         InvoiceFormat = invoiceFormat,
        //         BookingId = databook.BookingId,
        //         RentCost = reservationCost,
        //         Alocation = alocation.Id,
        //         TimeBefore = DateTime.Now,
        //         CreatedAt = DateTime.Now,
        //         CreatedBy = databook.Pic,
        //         InvoiceStatus = "0"// Pending status
        //     };
        // }

        public static FastBookBookingViewModel AdjustAdvanceMeeting(FastBookBookingViewModel databooking, FastBookRoomViewModel dataroom, int? isModuleEnable)
        {

            bool isRoomAdvEnabled = isModuleEnable.GetValueOrDefault(0) == 1;
            if (!isRoomAdvEnabled)
            {
                databooking.IsConfigSettingEnable = 0;
                databooking.IsEnableApproval = 0;
                databooking.IsEnablePermission = 0;
                databooking.IsEnableRecurring = 0;
                databooking.IsEnableCheckin = 0;
            }

            // Handling the room settings with default value of 0 if not set
            databooking.IsConfigSettingEnable = dataroom.IsConfigSettingEnable ?? 0;
            databooking.IsEnableApproval = dataroom.IsEnableApproval ?? 0;
            databooking.IsEnablePermission = dataroom.IsEnablePermission ?? 0;
            databooking.IsEnableRecurring = dataroom.IsEnableRecurring ?? 0;
            databooking.IsEnableCheckin = dataroom.IsEnableCheckin ?? 0;
            databooking.IsRealeaseCheckinTimeout = dataroom.IsRealeaseCheckinTimeout ?? 0;
            databooking.IsEnableCheckinCount = dataroom.IsEnableCheckinCount ?? 0;

            return databooking;
        }

        public static FastBookBookingViewModel CheckMeetingVipAccess(FastBookBookingViewModel databooking, FastBookRoomViewModel dataroom, FastBookEmployee resVIP, int? isModuleAdvanceEnable, int? isModuleVipEnable)
        {

            bool isRoomAdvEnabled = (isModuleAdvanceEnable ?? 0) == 1;
            bool isVipEnabled = (isModuleVipEnable ?? 0) == 1;

            string vipUser = databooking.VipUser;

            if (resVIP?.Id == null)
            {
                databooking.IsVip = 0;
                databooking.VipUser = string.Empty;
                return databooking;
            }


            if (!isRoomAdvEnabled || !isVipEnabled || (dataroom.IsConfigSettingEnable ?? 0) == 0 || databooking.IsVip == 0)
            {
                databooking.IsVip = 0;
                databooking.VipUser = string.Empty;
                return databooking; // VIP access not correct
            }

            databooking.VipApproveBypass = resVIP.VipApproveBypass ?? 0;
            databooking.VipLimitCapBypass = resVIP.VipLimitCapBypass ?? 0;
            databooking.VipLockRoom = resVIP.VipLockRoom ?? 0;
            databooking.VipUser = vipUser;

            return databooking;
        }


        public static FastBookBookingViewModel CheckApprovalMeetingAccess(FastBookBookingViewModel databooking, FastBookRoomViewModel dataroom, int? moduleVipEnable)
        {

            bool isVipEnabled = (moduleVipEnable ?? 0) == 1;

            string vipUser = databooking.VipUser;

            if ((dataroom.IsConfigSettingEnable ?? 0) == 0 || (dataroom.IsEnableApproval ?? 0) == 0)
            {
                databooking.IsApprove = 3;
                databooking.UserApproval = string.Empty;
                return databooking;
            }

            if (!isVipEnabled || databooking.IsVip == 0)
            {
                databooking.IsApprove = 0;
                databooking.IsAlive = 0;
                databooking.UserApproval = string.Empty;
                return databooking;
            }

            if (databooking.VipApproveBypass == 0)
            {
                databooking.IsApprove = 0;
                databooking.IsAlive = 0;
                databooking.UserApproval = string.Empty;
                return databooking;
            }

            databooking.IsApprove = 1;
            databooking.UserApproval = vipUser;

            return databooking;
        }



        public static FastBookBookingInvitation CreateInvitationPic(FastBookBookingViewModel data, string? nikPic = null, dynamic? getDataPIC = null)
        {
            return new FastBookBookingInvitation
            {
                BookingId = data.BookingId,
                Nik = getDataPIC?.Nik,
                Name = getDataPIC?.Name,
                Internal = 1,
                AttendanceStatus = 0,
                Email = getDataPIC?.Email,
                IsPic = 1,
                Company = "",
                PinRoom = GenerateRandomPin(),
                CreatedAt = DateTime.Now,
                CreatedBy = nikPic,
                UpdatedAt = DateTime.Now,
                IsDeleted = 0
            };
        }

        public static FastBookInternalBatchViewModel CreateInternalBatch(FastBookBookingViewModel data, List<string> listPinRoom, List<FastBookEmployeeViewModel> dataEmailInternal = null, string nikPic = null, bool createQr = false)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var internalBatch = new List<FastBookBookingInvitationViewModel>();
            int nn0 = 0;
            string id = data.BookingId;

            for (int i = 0; i < dataEmailInternal.Count; i++)
            {
                var val = dataEmailInternal[i];

                for (int j = 0; j < listPinRoom.Count; j++)
                {
                    var pin = listPinRoom[j];

                    var ibatch = new FastBookBookingInvitationViewModel
                    {
                        BookingId = id,
                        Nik = val.Nik,  // Employee ID
                        Name = val.Name,
                        Internal = 1,
                        AttendanceStatus = 0,
                        Email = val.Email,
                        IsPic = 0,
                        Company = "",
                        PinRoom = pin,
                        CreatedAt = DateTime.Parse(datetime),
                        CreatedBy = nikPic,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = 0
                    };

                    ibatch.PinRoom = pin;
                    ibatch.IsPic = 0;
                    internalBatch.Add(ibatch);

                    nn0++;
                    dataEmailInternal[j].Pin = pin;
                    dataEmailInternal[j].IsPic = 0;
                }
            }

            return new FastBookInternalBatchViewModel
            {
                InternalBatch = internalBatch,
                DataEmailInternal = dataEmailInternal
            };
        }

        public static (List<FastBookBookingInvitationViewModel> eksternalBatch, List<FastBookEmployeeViewModel> dataEmailEksternal) CreateExternalBatch(
         FastBookBookingViewModel data,
          List<string> listPinRoom, 
         List<FastBookListlDataExternalFRViewModel> externallist,
         string nikPic = null)
        {
            var eksternalBatch = new List<FastBookBookingInvitationViewModel>();
            var dataEmailEksternal = new List<FastBookEmployeeViewModel>();

            foreach (var val in externallist)
            {
                foreach (var pin in listPinRoom)
                {
                    var ibatch = new FastBookBookingInvitationViewModel
                    {
                        BookingId = data.BookingId.ToString(),
                        Email = val.Email,
                        Company = val.Company,
                        Name = val.Name,
                        IsPic = 0,
                        PinRoom = pin,
                        Internal = 0,
                        AttendanceStatus = 0,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedBy = nikPic,
                        IsDeleted = 0
                    };
                    dataEmailEksternal.Add(new FastBookEmployeeViewModel
                    {
                        BookingId = ibatch.BookingId,
                        Email = ibatch.Email,
                        Company = ibatch.Company,
                        Name = ibatch.Name,
                        IsPic = ibatch.IsPic,
                        Pin = ibatch.PinRoom
                    });
                    eksternalBatch.Add(ibatch);
                }
            }

            return (eksternalBatch, dataEmailEksternal);
        }


        public static string GenerateRandomPin()
        {
            var random = new Random();
            string pin = "";
            for (int i = 0; i < 6; i++)
            {
                pin += random.Next(0, 10).ToString();  // Generate a random numeric string of length 6
            }
            return pin;
        }


        public static FastBookBookingViewModel MobileFormatBooking(FastBookBookingViewModel databook)
        {
            return new FastBookBookingViewModel
            {
                BookingId = "",
                NoOrder = "",
                Title = databook.Title,
                RoomId = "",
                Date = databook.Date,
                Start = ParseDateTimeConvert(databook.Date.ToString("yyyy-MM-dd"), databook.Start.ToString("HH:mm:ss")),
                End = ParseDateTimeConvert(databook.Date.ToString("yyyy-MM-dd"), databook.End.ToString("HH:mm:ss")),
                TotalDuration = 0,
                DurationPerMeeting = 0,
                CostTotalBooking = 0,
                AlocationId = "",
                AlocationName = "",
                Pic = "",
                IsAlive = 1,
                IsMeal = 0,
                IsDeleted = 0,
                IsRescheduled = 0,
                IsCanceled = 0,
                IsExpired = 0,
                IsDevice = 1, // Mobile created
                CreatedAt = DateTime.Now,
                CreatedBy = "", // Mobile created
                ExternalLink = databook.ExternalLink ?? "",
                Note = databook.Note ?? "",
                RoomName = "",
                IsMerge = 0,
                MergeRoomName = "",
                MergeRoomId = "",
                MergeRoom = "",
                IsVip = databook.IsVip,
                VipUser = databook.VipUser ?? "",
                IsApprove = 0,
                UserApproval = "",
                Category = databook.Category,
                Timezone = databook.Timezone ?? ""
            };
        }

        private static DateTime ParseDateTimeConvert(string date, string time)
        {
            if (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
            {
                throw new ArgumentException("Date or Time is empty");
            }

            string fixedTime = time.Replace('.', ':'); // "07.00.00" → "07:00:00"

            string dateTimeString = $"{date} {fixedTime}"; // Example: "2025-02-23 07:00:00"
            string format = "yyyy-MM-dd HH:mm:ss";

            if (DateTime.TryParseExact(dateTimeString, format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            throw new FormatException($"Invalid date format: {dateTimeString}");
        }

        public static string GetPathEmailTemplate(string typeEmail)
        {
            var rootPath = AppContext.BaseDirectory; // Ensures correct base path
            var templateFolderPath = Path.Combine(rootPath, "EmailTemplate");
            var templatePath = Path.Combine(templateFolderPath, GetTemplateFileName(typeEmail)); // Corrected path

            return templatePath;
        }

        private static string GetTemplateFileName(string typeEmail)
        {
            return typeEmail switch
            {
                "invitation" => "undangan-meeting.html",  // Removed "EmailTemplate/"
                "reschedule" => "reschedule-meeting.html",
                "cancel" => "pembatalan-meeting.html",
                _ => throw new ArgumentException("Invalid email type")
            };
        }


        public static string FalcoPulseData(string ip, int? doorId)
            {
                return $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <FalcoPulseDoorOpen xmlns=""WebAPI"">
                          <tokenLoginID>Falco</tokenLoginID>
                          <tokenLoginPass>12345</tokenLoginPass>
                          <IP>{ip}</IP>
                          <DoorID>{doorId}</DoorID>
                          <aError>string</aError>
                        </FalcoPulseDoorOpen>
                      </soap:Body>
                    </soap:Envelope>";
            }
	
    }
}