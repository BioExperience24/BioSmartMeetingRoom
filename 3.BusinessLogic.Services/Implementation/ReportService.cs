

using Microsoft.Extensions.DependencyInjection;

namespace _3.BusinessLogic.Services.Implementation
{
    public class ReportService(
        IMapper mapper,
        IServiceProvider serviceProvider,
        BookingRepository bookingRepo,
        EmployeeRepository employeeRepo,
        BookingInvitationRepository bookingInvitationRepo
    )
        : IReportService
    {
        private readonly IMapper _mapper = mapper;

        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private readonly BookingRepository _bookingRepo = bookingRepo;

        private readonly EmployeeRepository _employeeRepo = employeeRepo;

        private readonly BookingInvitationRepository _bookingInvitationRepo = bookingInvitationRepo;

        public async Task<BookingVMRoomUsageData> GetAllRoomUsageReportItemAsync(BookingVMRoomUsageFR request)
        {
            var entity = new Booking{};

            if (request.Date != null)
            {
                string[] dates = request.Date.Split(" - ");
                entity.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                entity.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
            }

            if (request.BuildingId > 0)
            {
                entity.BuildingId = request.BuildingId;
            }

            if (request.RoomId != null)
            {
                entity.RoomId = request.RoomId;
            }

            if (request.AlocationId != null)
            {
                entity.AlocationId = request.AlocationId;
            }

            var data = await _bookingRepo.GetAllItemReportUsageAsync(entity, request.Length, request.Start);

            var result = _mapper.Map<List<BookingVMRoomUsageCollection>>(data.Collection);

            var no = request.Start + 1;

            foreach (var item in result)
            {
                item.No = no;
                var timeStart = item.Start.ToString("HH:mm");
                var timeEnd = item.End.ToString("HH:mm");
                item.Time = $"{timeStart} - {timeEnd}";

                item.BookingDate = $"{item.Date.ToString("dd MMM yyyy")} {item.Time}";

                
                no++;
            }

            return new BookingVMRoomUsageData {
                Collection = result,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered
            };
        }
    
        public async Task<EmployeeVMOrganizerData> GetAllOrganizerUsageReportItemAsync(EmployeeVMOrganizerFR request)
        {
            var filter = new EmployeeFilter{};

            var filterBookingInvitation = new BookingInvitationFilter{};

            var filterBooking = new BookingFilter{};

            if (request.Date != null)
            {
                string[] dates = request.Date.Split(" - ");
                
                filter.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filter.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");

                filterBookingInvitation.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filterBookingInvitation.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");

                filterBooking.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filterBooking.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
            }

            if (request.BuildingId > 0)
            {
                filter.BuildingId = request.BuildingId;
                filterBookingInvitation.BuildingId = request.BuildingId;
                filterBooking.BuildingId = request.BuildingId;
            }

            if (request.RoomId != null)
            {
                filter.RoomId = request.RoomId;
                filterBookingInvitation.RoomId = request.RoomId;
                filterBooking.RoomId = request.RoomId;
            }

            if (request.Nik != null)
            {
                filter.Nik = request.Nik;
            }

            var data = await _employeeRepo.GetAllOrganizerUsageReportItemAsync(filter, request.Length, request.Start);

            var tasks1 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var biRepo = scope.ServiceProvider.GetRequiredService<BookingInvitationRepository>();

                        filterBookingInvitation.Nik = item.Employee.Nik;
                        filterBookingInvitation.Checkin = 0;
                        item.TotalAttendees = await biRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);
                    }
                }
            }).ToList();

            var tasks2 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var biRepo = scope.ServiceProvider.GetRequiredService<BookingInvitationRepository>();

                        filterBookingInvitation.Nik = item.Employee.Nik;
                        filterBookingInvitation.Checkin = 1;
                        item.TotalAttendeesCheckin = await biRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);
                    }
                }
            }).ToList();

            var tasks3 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var bRepo = scope.ServiceProvider.GetRequiredService<BookingRepository>();

                        filterBooking.Nik = item.Employee.Nik;
                        var totalDuration = await bRepo.GetCountTotalDurationByPicAsync(filterBooking);
                        item.TotalDuration = totalDuration ?? 0;
                    }
                }
            }).ToList();

            // Tunggu semua task selesai
            var allTasks = tasks1.Concat(tasks2).Concat(tasks3);  
            await Task.WhenAll(allTasks);
            
            // INFO: alternatif jika metode task terdapat issue (valuenya berubah ubah)
            /* foreach (var item in items)
            {
                if (item.Employee != null) {
                    
                    filterBookingInvitation.Nik = item.Employee.Nik;
                    filterBookingInvitation.Checkin = 0;

                    item.TotalAttendees = await _bookingInvitationRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);

                    filterBookingInvitation.Checkin = 1;
                    item.TotalAttendeesCheckin = await _bookingInvitationRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);

                    filterBooking.Nik = item.Employee.Nik;
                    var totalDuration = await _bookingRepo.GetCountTotalDurationByPicAsync(filterBooking);
                    item.TotalDuration = totalDuration ?? 0;
                }
            } */

            var result = _mapper.Map<List<EmployeeVMOrganizerCollection>>(data.Collections);

            var no = request.Start + 1;

            foreach (var item in result)
            {
                item.No = no;
                no++;
            }

            return new EmployeeVMOrganizerData {
                Collections = result,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered
            };
        }

        public async Task<EmployeeVMAttendeesData> GetAllAttendeesReportItemAsync(EmployeeVMOrganizerFR request)
        {
            var filter = new EmployeeFilter{};

            var filterBookingInvitation = new BookingInvitationFilter{};

            var filterBooking = new BookingFilter{};

            if (request.Date != null)
            {
                string[] dates = request.Date.Split(" - ");
                
                filter.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filter.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");

                filterBookingInvitation.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filterBookingInvitation.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");

                filterBooking.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                filterBooking.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
            }

            if (request.BuildingId > 0)
            {
                filter.BuildingId = request.BuildingId;
                filterBookingInvitation.BuildingId = request.BuildingId;
                filterBooking.BuildingId = request.BuildingId;
            }

            if (request.RoomId != null)
            {
                filter.RoomId = request.RoomId;
                filterBookingInvitation.RoomId = request.RoomId;
                filterBooking.RoomId = request.RoomId;
            }

            if (request.Nik != null)
            {
                filter.Nik = request.Nik;
            }

            var data = await _employeeRepo.GetAllAttendeesReportItemAsync(filter, request.Length, request.Start);

            var tasks1 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var biRepo = scope.ServiceProvider.GetRequiredService<BookingInvitationRepository>();

                        filterBookingInvitation.Nik = item.Employee.Nik;
                        filterBookingInvitation.Checkin = 1;
                        filterBookingInvitation.AttendanceStatus = 0;
                        item.TotalAttendeesCheckin = await biRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);
                    }
                }
            }).ToList();

            var tasks2 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var bRepo = scope.ServiceProvider.GetRequiredService<BookingRepository>();

                        filterBooking.Nik = item.Employee.Nik;
                        var totalDuration = await bRepo.GetCountTotalDurationByPicAsync(filterBooking);
                        item.TotalDuration = totalDuration ?? 0;
                    }
                }
            }).ToList();

            var tasks3 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var biRepo = scope.ServiceProvider.GetRequiredService<BookingInvitationRepository>();

                        filterBookingInvitation.Nik = item.Employee.Nik;
                        filterBookingInvitation.Checkin = 0;
                        filterBookingInvitation.AttendanceStatus = 1;
                        item.TotalPresent = await biRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);
                    }
                }
            }).ToList();

            var tasks4 = data.Collections!.Select(async item => {
                if (item.Employee != null) {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var biRepo = scope.ServiceProvider.GetRequiredService<BookingInvitationRepository>();

                        filterBookingInvitation.Nik = item.Employee.Nik;
                        filterBookingInvitation.Checkin = 0;
                        filterBookingInvitation.AttendanceStatus = 2;
                        item.TotalAbsent = await biRepo.GetCountAttendeesByPicAsync(filterBookingInvitation);
                    }
                }
            }).ToList();

            // Tunggu semua task selesai
            var allTasks = tasks1.Concat(tasks2).Concat(tasks3).Concat(tasks4);
            await Task.WhenAll(allTasks);  

            var result = _mapper.Map<List<EmployeeVMAttendeesCollection>>(data.Collections);

            var no = request.Start + 1;

            foreach (var item in result)
            {
                item.No = no;
                no++;
            }

            return new EmployeeVMAttendeesData {
                Collections = result,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered
            };
        }
    }
}