using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingRuleBookingRepository : BaseRepositoryId<SettingRuleBooking>
    {
        private readonly MyDbContext _dbContext;

        public SettingRuleBookingRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingRuleBooking>?, string? err)> GetAllSettingRuleBookingsAsync()
        {
            try
            {
                var query = _dbContext.SettingRuleBookings.AsQueryable();

                //query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("Id", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingRuleBooking?> GetSettingRuleBookingTopOne()
        {
            return await _dbContext.SettingRuleBookings.FirstOrDefaultAsync();
        }
        public async Task<SettingRuleBooking?> GetSettingRuleBookingById(long id)
        {
            return await _dbContext.SettingRuleBookings.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<SettingRuleBooking?> GetSettingRuleBookingByPinDefault(string pin)
        {
            return await _dbContext.SettingRuleBookings.FirstOrDefaultAsync(c => c.RoomPinNumber == pin);
        }

        public async Task<SettingRuleBooking?> AddSettingRuleBookingAsync(SettingRuleBooking item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingRuleBookingAsync");

                _dbContext.SettingRuleBookings.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingRuleBookingAsync");

                return null;
            }

            return item;
        }
        public override async Task<int> UpdateAsync(SettingRuleBooking item)
        {
            //var dependentEntity = _context.SettingRuleBookings
            //                    .FirstOrDefault(e => e.Id == item.Id);
            //if (dependentEntity != null)
            //{
            //    _context.SettingRuleBookings.Remove(dependentEntity);
            //    _context.SaveChanges();
            //}

            _context.Entry(item).State = EntityState.Modified;
            //_context.Entry(item).Property(e => e.Id).IsModified = false;

            return await _context.SaveChangesAsync();
            //var newPrincipalEntity = new SettingRuleBooking
            //{
            //    // Set properties for the new principal entity
            //};
            //_context.SettingRuleBookings.Add(newPrincipalEntity);
            //return _context.SaveChanges();

        }

        //public async Task<bool> UpdateSettingRuleBookingAsync(SettingRuleBooking item)
        //{
        //    //using var transaction = _dbContext.Database.BeginTransaction();

        //    try
        //    {
        //        //await transaction.CreateSavepointAsync("UpdateSettingRuleBookingAsync");

        //        //_dbContext.SettingRuleBookings.Attach(item);

        //        //_dbContext.Entry(item).Property(e => e.Id).IsModified = false;
        //        //_dbContext.Entry(item).Property(e => e.Duration).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IfUnusedRoom).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.MaxEndMeeting).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.NotifUnusedMeeting).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.NotifUnuseBeforeMeeting).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.UnuseCancelFee).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.MaxDisplayDuration).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.RoomPin).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.RoomPinNumber).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.RoomPinRefresh).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ExtendMeeting).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ExtendMeetingMax).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ExtendCountTime).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ExtendMeetingNotification).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.EndEarlyMeeting).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.LimitTimeBooking).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.UpdatedBy).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.UpdatedAt).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsConfigSettingEnable).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigRoomForUsage).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsEnableApproval).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigApprovalUser).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsEnablePermission).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigPermissionUser).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigPermissionCheckin).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigPermissionEnd).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigMinDuration).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigMaxDuration).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigAdvanceBooking).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsEnableRecurring).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsEnableCheckin).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigAdvanceCheckin).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsRealeaseCheckinTimeout).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigReleaseRoomCheckinTimeout).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.ConfigParticipantCheckinCount).IsModified = true;
        //        //_dbContext.Entry(item).Property(e => e.IsEnableCheckinCount).IsModified = true;

        //        _context.Entry(item).Property(e => e.Id).IsModified = false;

        //        return await _context.SaveChangesAsync();
        //        //await _dbContext.SaveChangesAsync();

        //        //await transaction.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //        //await transaction.RollbackToSavepointAsync("UpdateSettingRuleBookingAsync");

        //        return false;
        //    }

        //    //return true;
        //}
    }
}