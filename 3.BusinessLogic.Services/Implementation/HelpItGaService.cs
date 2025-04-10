
using System.Security.Claims;
using System.Transactions;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation
{
    public class HelpItGaService : BaseService<HelpItGaViewModel, HelpItGa>, IHelpItGaService
    {
        private readonly IHttpContextAccessor _httpCtx;
        private readonly HelpItGaRepository _helpItGaRepo;
        private readonly EmployeeRepository _employeeRepo;
        private readonly BookingRepository _bookingRepo;

        public HelpItGaService(
            IHttpContextAccessor httpCtx,
            IMapper mapper,
            HelpItGaRepository repo,
            EmployeeRepository employeeRepo,
            BookingRepository bookingRepo
        ) : base(repo, mapper)
        {
            _httpCtx = httpCtx;
        
            _helpItGaRepo = repo;
            _employeeRepo = employeeRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<DataTableResponse> GetDataTablesAsync(HelpItGaVMDataTable request)
        {
            
            var filter = new HelpItGaFilter
            {
                Type = request.Type,
                Search = request.SearchValue,
            };

            var item = await _helpItGaRepo.GetAllItemFilterdByEntityAsync(filter, request.Length, request.Start);
            
            var collectionMap = _mapper.Map<List<HelpItGaVMDataTableResponse>>(item.Collections);
            int no = request.Start + 1;
            collectionMap.ForEach(x =>
            {
                x.No = no++;
                x.DatetimeFormatted = _DateTime.Format(x.Datetime);
                x.CreatedAtFormatted = _DateTime.Format(x.CreatedAt);
                x.UpdatedAtFormatted = _DateTime.Format(x.UpdatedAt);
                x.ProcessAtFormatted = _DateTime.Format(x.ProcessAt);
                x.DoneAtFormatted = _DateTime.Format(x.DoneAt);
                x.RejectAtFormatted = _DateTime.Format(x.RejectAt);

                switch (x.Status)
                {
                    case "process":
                        x.UserApproved = x.ProcessBy;
                        break;
                    case "done":
                        x.UserApproved = x.DoneBy;
                        break;
                    case "reject":
                        x.UserApproved = x.RejectBy;
                        break;
                    default:
                        x.UserApproved = "";
                        break;
                }
            });

            return new DataTableResponse {
                Draw = request.Draw,
                RecordsTotal = item.RecordsTotal,
                RecordsFiltered = item.RecordsFiltered,
                Data = collectionMap,
            };
        }

        public async Task<ReturnalModel> ListRequestAsync(HelpItGaVMFilterList request)
        {
            ReturnalModel ret = new();

            // from token
            var authUserRole = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            // .from token

            if (authUserRole != "1" && authUserRole != "6")
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "You are not allowed to access this feature";
                return ret;
            }

            var filter = new HelpItGaFilter{};

            if (!string.IsNullOrEmpty(request.Type))
            {
                string[] type = { HelpItGaType.IT, HelpItGaType.GA };

                if (!type.Contains(request.Type))
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Type not valid";
                    return ret;
                }

                filter.Type = request.Type;
            }

            if (!string.IsNullOrEmpty(request.RoomId))
            {
                filter.RoomId = request.RoomId;
            }

            if (request.Start.HasValue)
            {
                filter.Start = request.Start.Value.ToDateTime(TimeOnly.MinValue);
            }

            if (request.End.HasValue)
            {
                filter.End = request.End.Value.ToDateTime(TimeOnly.MaxValue);
            }

            var item = await _helpItGaRepo.GetAllItemFilterdByEntityAsync(filter);
            
            var collectionMap = _mapper.Map<List<HelpItGaVMDataTableResponse>>(item.Collections);
            int no = 1;
            collectionMap.ForEach(x =>
            {
                x.No = no++;
                x.DatetimeFormatted = _DateTime.Format(x.Datetime);
                x.CreatedAtFormatted = _DateTime.Format(x.CreatedAt);
                x.UpdatedAtFormatted = _DateTime.Format(x.UpdatedAt);
                x.ProcessAtFormatted = _DateTime.Format(x.ProcessAt);
                x.DoneAtFormatted = _DateTime.Format(x.DoneAt);
                x.RejectAtFormatted = _DateTime.Format(x.RejectAt);

                switch (x.Status)
                {
                    case "process":
                        x.UserApproved = x.ProcessBy;
                        break;
                    case "done":
                        x.UserApproved = x.DoneBy;
                        break;
                    case "reject":
                        x.UserApproved = x.RejectBy;
                        break;
                    default:
                        x.UserApproved = "";
                        break;
                }
            });

            ret.Collection = collectionMap;

            return ret;
        }

        public async Task<ReturnalModel> ChangeStatusAsync(HelpItGaVMChangeStatus request)
        {
            ReturnalModel ret = new();

            // from token
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            // .from token

            DateTime now = DateTime.Now;

            var item = await _helpItGaRepo.GetByIdAsync(request.Id);

            if (item == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data not found";
                return ret;
            }

            var user = await _employeeRepo.GetItemByNikAsync(authUserNIK!);
            var authUserName = user?.Name ?? "";

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    switch (request.Status)
                    {
                        case "process":
                            item.Status = "process";
                            item.ProcessAt = now;
                            item.ProcessBy = authUserName;
                            break;
                        case "reject":
                            item.Status = "reject";
                            item.RejectAt = now;
                            item.RejectBy = authUserName;
                            item.ResponseReject = request.Note ?? "";
                            break;
                        case "done":
                            item.Status = "done";
                            item.DoneAt = now;
                            item.DoneBy = authUserName;
                            item.ResponseDone = request.Note ?? "";
                            item.TimeUntilDoneAt = item.ProcessAt.HasValue ? (int)(now - item.ProcessAt.Value).TotalMinutes : 0;
                            break;
                    }

                    await _helpItGaRepo.UpdateAsync(item);

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    
        public async Task<ReturnalModel> SubmitRequestAsync(HelpItGaVMRequest request)
        {
            ReturnalModel ret = new();

            string[] type = { HelpItGaType.IT, HelpItGaType.GA };

            string[] facilityReason = { HelpItGaProblemReason.Comfort, HelpItGaProblemReason.Connection, HelpItGaProblemReason.Facility };

            if (!type.Contains(request.Type) || !facilityReason.Contains(request.ProblemReason))
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = !type.Contains(request.Type) ? 
                    "Type not valid" : "Problem reason not valid";
                return ret;
            }

            // from token
            // var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            var authUserRole = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            // .from token

            // var user = await _employeeRepo.GetItemByNikAsync(authUserNIK!);
            // var authUserName = user?.Name ?? "";

            if (authUserRole != "1" && authUserRole != "6")
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "You are not allowed to access this feature";
                return ret;
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var id = Guid.NewGuid().ToString();

                    TimeSpan time = TimeSpan.Parse(request.Time);
                    
                    DateTime dateTime = request.Date.ToDateTime(TimeOnly.FromTimeSpan(time));

                    var booking = await _bookingRepo.GetBookingOnGoingFilteredByRoomIdAsync(request.RoomId, request.Date, time);

                    if (booking == null)
                    {
                        ret.Status = ReturnalType.BadRequest;
                        ret.Message = "No ongoing meeting in this room";
                        return ret;
                    }

                    var item = new HelpItGa
                    {
                        Id = id,
                        Type = request.Type,
                        Subject = request.Subject,
                        Description = request.Description,
                        BookingId = booking.BookingId,
                        Datetime = dateTime,
                        RoomId = request.RoomId,
                        ProblemFacility = request.ProblemFacility,
                        ProblemReason = request.ProblemReason,
                        CreatedAt = dateTime,
                        CreatedBy = booking.Pic,
                        Status = "pending",
                        IsDeleted = 0
                    };

                    var result = await _helpItGaRepo.AddAsync(item);

                    var collection = _mapper.Map<HelpItGaViewModel>(result);
                    
                    collection.DatetimeFormatted = _DateTime.Format(collection.Datetime);
                    collection.CreatedAtFormatted = _DateTime.Format(collection.CreatedAt);
                    collection.UpdatedAtFormatted = _DateTime.Format(collection.UpdatedAt);
                    collection.ProcessAtFormatted = _DateTime.Format(collection.ProcessAt);
                    collection.DoneAtFormatted = _DateTime.Format(collection.DoneAt);
                    collection.RejectAtFormatted = _DateTime.Format(collection.RejectAt);

                    ret.Collection = collection;

                    scope.Complete();
                    
                    return ret;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

        }
    }
}