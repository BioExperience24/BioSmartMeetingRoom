using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Transactions;
using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;

namespace _6.Repositories.Repository;

public class ModuleBackendRepository
{

    private readonly MyDbContext _dbContext;
    private readonly DbSet<ModuleBackend> _dbSet;

    public ModuleBackendRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<ModuleBackend>();
    }


    public async Task<ModuleBackend?> GetItemByEntityAsync(ModuleBackend? entity = null)
    {
        var query = _dbContext.ModuleBackends.AsQueryable();

        if (entity != null)
        {
            if (entity.ModuleId > 0)
            {
                query = query.Where(q => q.ModuleId == entity.ModuleId);
            }

            if (entity.ModuleText != null)
            {
                query = query.Where(q => q.ModuleText == entity.ModuleText);
            }

            if (entity.Name != null)
            {
                query = query.Where(q => q.Name.Contains(entity.Name));
            }
        }

        query = query.OrderByColumn("ModuleId", "desc");

        var item = await query.FirstOrDefaultAsync();

        return item;
    }

    public async Task<IEnumerable<ModuleBackend>> GetItemsByModuleTextAsync(string[] moduleTexts)
    {
        /* var query = from moduleText in _dbContext.ModuleBackends
                    where moduleTexts.Contains(moduleText.ModuleText)
                    select moduleText; */
                    
        var query = from moduleText in _dbContext.ModuleBackends
                    select moduleText;

        if (moduleTexts.Any())
        {
            query = from moduleText in query
                    where moduleTexts.Contains(moduleText.ModuleText)
                    select moduleText;
        }

        return await query.ToListAsync();
    }


    /// <summary>
    /// Get records from ModuleBackend table based on a dynamic condition.
    /// </summary>
    /// <param name="condition">The condition to filter the records.</param>
    /// <returns>A list of ModuleBackend records matching the condition.</returns>
    public async Task<ModuleBackend?> GetModuleByTextAsync(string moduleText)
    {
        return await _dbContext.ModuleBackends
            .FirstOrDefaultAsync(m => m.ModuleText == moduleText);
    }
    public async Task<List<RoomMergeDetail?>> GetAllRoomMerge()
    {
        return await _dbContext.RoomMergeDetails
            .ToListAsync();
    }

    public async Task<List<RoomMergeDetail?>> GetRoomMerge(long id)
    {
        return await _dbContext.RoomMergeDetails
            .Where(m => m.RoomId == id)
            .ToListAsync();
    }
    public async Task<IEnumerable<Facility>> SelectAllFacilityAsync()
    {
        return await _dbContext.Facilities
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<RoomForUsageDetail?>> GetRoomForUsageDetail(long? id)
    {
        return await _dbContext.RoomForUsageDetails
            .Where(m => m.RoomId == id)
            .ToListAsync();
    }

    public async Task<List<SettingInvoiceText>> GetInvoiceStatus()
    {
        return await _dbContext.SettingInvoiceTexts
            .Where(m => m.IsDeleted == 0)
            .ToListAsync();
    }

    public async Task DeleteOldRoomForUsageDetailAsync(long id)
    {
        // Use ExecuteDelete for efficient deletion
        var rowsAffected = await _dbContext.RoomForUsageDetails
                                           .Where(x => x.RoomId == id)
                                           .ExecuteDeleteAsync();
    }



    //public async Task<List<BookingDto>> GetReportusage(string wreport, string wdate)
    ////{
      

    ////}


    public async Task RemoveRoomMergeDetail(long? roomId)
    {
        await _dbContext.RoomMergeDetails
            .Where(m => m.RoomId == roomId)
            .ExecuteDeleteAsync();
    }
    public async Task RemoveRoomDetail(long? roomId)
    {
        await _dbContext.RoomMergeDetails
            .Where(m => m.RoomId == roomId)
            .ExecuteDeleteAsync();
    }


    public async Task CreateBulkRoomForUsageDetail(List<RoomForUsageDetail> entities)
    {
        await _dbContext.BulkInsertAsync(entities.ToList());
    }


    public async Task<LicenseList?> GetLicenseByModule()
    {
        // Step 1: Fetch a single license information by module
        var licenseInfo = await _dbContext.LicenseLists
            .Where(l => l.Module == "module_room")
            .FirstOrDefaultAsync();

        return licenseInfo;
    }

    public virtual async Task CreateBulkRoomMergeDetail(IEnumerable<RoomMergeDetail> entities)
    {
        await _dbContext.BulkInsertAsync(entities.ToList());
    }

    public virtual async Task CreateBulkRoomForUsageDetail(IEnumerable<RoomForUsageDetail> entities)
    {
        await _dbContext.BulkInsertAsync(entities.ToList());
    }



    public virtual async Task CreateBulkRoomDetail(IEnumerable<RoomDetail> entities)
    {
        await _dbContext.BulkInsertAsync(entities.ToList());
    }


    /// <summary>
    /// Get a single ModuleBackend record by ModuleText.
    /// </summary>
    /// <param name="moduleText">The text of the module to search for.</param>
    /// <returns>The ModuleBackend record or null if not found.</returns>
    public async Task<ModuleBackend?> GetByModuleTextAsync(string moduleText)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.ModuleText == moduleText);
    }

    public async Task<List<object>> GetMenuAsync(string menuName = "", int menuId = 0, long levelId = 0)
    {
        var menuDb = await _dbContext.Levels
            .Where(l => l.Id == levelId && l.IsDeleted == 0)
            .Join(_dbContext.LevelDetails, l => l.Id, ld => ld.LevelId, (l, ld) => new { l, ld })
            .Join(_dbContext.Menus, join1 => join1.ld.MenuId, m => m.Id, (join1, m) => new { join1.l, join1.ld, m })
            .Join(_dbContext.MenuGroups, join2 => join2.m.MenuGroupId, mg => mg.Id, (join2, mg) => new MenuDto
            {
                IsChild = join2.m.IsChild,
                MgIcon = mg.Icon,
                MenuGroupId = mg.Id,
                GMenuName = mg.Name,
                LevelName = join2.l.Name,
                MenuName = join2.m.Name,
                Url = join2.m.Url,
                Icon = join2.m.Icon,
                Active = false // Default active state
            })
            .OrderBy(x => x.MenuName)
            .ToListAsync();


        menuDb.ForEach(menu => menu.Active = menu.MenuName.Equals(menuName, StringComparison.OrdinalIgnoreCase));

        return GetSendMenu(menuDb);
    }

    public List<object> GetSendMenu(List<MenuDto> menu)
    {
        var menuMaster = new Dictionary<string, object>();

        foreach (var value in menu)
        {
            if (value.IsChildBool)
            {
                var menuName = value.GMenuName.Trim();
                if (!menuMaster.ContainsKey(menuName))
                {
                    menuMaster[menuName] = new
                    {
                        Name = value.GMenuName,
                        Url = "",
                        Icon = value.MgIcon,
                        Active = value.Active,
                        Child = new List<object>()
                    };
                }

                var childMenu = new
                {
                    Name = value.MenuName,
                    Icon = value.Icon,
                    Url = value.Url,
                    Active = value.Active
                };

                ((List<object>)((dynamic)menuMaster[menuName]).Child).Add(childMenu);
            }
            else
            {
                var menuName = value.MenuName.Trim();
                if (!menuMaster.ContainsKey(menuName))
                {
                    menuMaster[menuName] = new
                    {
                        Name = value.MenuName,
                        Url = value.Url,
                        Icon = value.Icon,
                        Active = value.Active,
                        Child = new List<object>()
                    };
                }
            }
        }

        return menuMaster.Values.ToList();
    }

    public async Task<List<RoomDetailDto>> CheckRoomModuleLicense()
    {
        // Query to retrieve room details with associated automation and building data
        var query = from r in _dbContext.Rooms
                    join ra in _dbContext.RoomAutomations on r.AutomationId equals ra.Id into raJoin
                    from ra in raJoin.DefaultIfEmpty()
                    join b in _dbContext.Buildings on r.BuildingId equals b.Id into bJoin
                    from b in bJoin.DefaultIfEmpty()
                    where r.IsDeleted == 0 // Adjust if IsDeleted is boolean
                    orderby r.Name
                    select new RoomDetailDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Capacity = r.Capacity,
                        GoogleMap = r.GoogleMap,
                        RaName = ra != null ? ra.Name : null, // Null check for left join
                        RaId = ra != null ? ra.Id : (long?)null, // Null check for left join
                        BuildingName = b != null ? b.Name : null, // Null check for left join
                        BuildingDetail = b != null ? b.DetailAddress : null, // Null check for left join
                        BuildingGoogleMap = b != null ? b.GoogleMap : null // Null check for left join
                    };

        // Execute and return the query asynchronously
        return await query.ToListAsync();
    }

public async Task<(string Error, List<RoomData> Data)> GetRoomDataAsync()
    {
        try
        {
            var data = await (from r in _dbContext.Rooms
                              join ra in _dbContext.RoomAutomations on r.AutomationId equals ra.Id into raJoin
                              from ra in raJoin.DefaultIfEmpty()
                              join b in _dbContext.Buildings on r.BuildingId equals b.Id into bJoin
                              from b in bJoin.DefaultIfEmpty()
                              where r.IsDeleted == 0
                              orderby r.Name
                              select new RoomData
                              {
                                  Id = r.Id,                  // Inherited from Room
                                  Name = r.Name,              // Inherited from Room
                                  RadId = r.Radid,                // Inherited from Room
                                  RaName = ra.Name,               // From RoomAutomations
                                  RaId = ra.Id,                   // From RoomAutomations
                                  AutomationName = ra.Name,       // From RoomAutomations
                                  BuildingName = b.Name,          // From Buildings
                                  BuildingDetail = b.DetailAddress, // From Buildings
                                  BuildingGoogleMap = b.GoogleMap,  // From Buildings

                                  // Inherited Room properties:
                                  Capacity = r.Capacity,
                                  Description = r.Description,
                                  GoogleMap = r.GoogleMap,
                                  IsAutomation = r.IsAutomation,
                                  AutomationId = r.AutomationId,
                                  FacilityRoom = r.FacilityRoom,
                                  WorkDay = r.WorkDay,
                                  WorkTime = r.WorkTime,
                                  WorkStart = r.WorkStart,
                                  WorkEnd = r.WorkEnd,
                                  Image = r.Image,
                                  Image2 = r.Image2,
                                  MultipleImage = r.MultipleImage,
                                  Price = r.Price,
                                  Location = r.Location,
                                  IsDisabled = r.IsDisabled,
                                  IsBeacon = r.IsBeacon,
                                  CreatedBy = r.CreatedBy,
                                  CreatedAt = r.CreatedAt,
                                  UpdatedAt = r.UpdatedAt,
                                  IsConfigSettingEnable = r.IsConfigSettingEnable,
                                  ConfigRoomForUsage = r.ConfigRoomForUsage,
                                  IsEnableApproval = r.IsEnableApproval,
                                  ConfigApprovalUser = r.ConfigApprovalUser,
                                  IsEnablePermission = r.IsEnablePermission,
                                  ConfigPermissionUser = r.ConfigPermissionUser,
                                  ConfigPermissionCheckin = r.ConfigPermissionCheckin,
                                  ConfigPermissionEnd = r.ConfigPermissionEnd,
                                  ConfigMinDuration = r.ConfigMinDuration,
                                  ConfigMaxDuration = r.ConfigMaxDuration,
                                  ConfigAdvanceBooking = r.ConfigAdvanceBooking,
                                  IsEnableRecurring = r.IsEnableRecurring,
                                  IsEnableCheckin = r.IsEnableCheckin,
                                  ConfigAdvanceCheckin = r.ConfigAdvanceCheckin,
                                  IsRealeaseCheckinTimeout = r.IsRealeaseCheckinTimeout,
                                  ConfigReleaseRoomCheckinTimeout = r.ConfigReleaseRoomCheckinTimeout,
                                  ConfigParticipantCheckinCount = r.ConfigParticipantCheckinCount,
                                  IsEnableCheckinCount = r.IsEnableCheckinCount,
                                  ConfigGoogle = r.ConfigGoogle,
                                  ConfigMicrosoft = r.ConfigMicrosoft,

                                  // Additional fields:
                                  RoomDetail = new List<RoomDetail>()
                              }).ToListAsync();

            return (null, data);
        }
        catch (Exception ex)
        {
            return (ex.Message, new List<RoomData>());
        }
    }



    public async Task<List<RoomDetail>> QuerySql(string query)
    {
        return await _dbContext.RoomDetails.FromSqlRaw(query).ToListAsync();
    }

    // Get All
    public async Task<IEnumerable<RoomForUsage>> SelectAllRoomForUsageAsync()
    {
        return await _dbContext.RoomForUsages
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .ToListAsync();
    }

    // Get All
    public async Task<IEnumerable<RoomUserCheckin>> SelectAllRoomForUserCheckinAsync()
    {
        return await _dbContext.RoomUserCheckins
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .ToListAsync();
    }


    public async Task<IEnumerable<RoomUserCheckin>> SelectAllRoomUserCheckinAsync(
        Dictionary<string, object> condition,
        string[] fields = null,
        string result = "result")
    {
        var moduleBackendRepository = new _BaseModuleRepository<RoomUserCheckin>(_dbContext);
        var data = await moduleBackendRepository.SelectAllDataAsync(condition, fields, result);
        return data;

    }

    public async Task<List<EmployeeWithAccessInfo>> GetEmployeesWithAccessAsync()
    {
        // Assuming "filterCondition" represents the equivalent of "$w" in PHP.
        var query = _dbContext.Employees
            .Join(
                _dbContext.Users, // Join table
                employee => employee.Id, // Foreign key in Employees
                user => user.EmployeeId, // Key in Users
                (employee, user) => new EmployeeWithAccessInfo
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    EmployeeId = employee.Id,
                    Nik = employee.Nik,
                    CardNumber = employee.CardNumber,
                    PhoneNumber = employee.NoPhone,
                    ExtensionNumber = employee.NoExt,
                    AccessId = user.AccessId
                }
            )
            .Where(joined => !string.IsNullOrEmpty(joined.AccessId) && joined.AccessId.Contains("4")); // LIKE '%4%'

       var dataEmployee =  await query.Select(result => new EmployeeWithAccessInfo
        {
            Name = result.Name,
            Email = result.Email,
            EmployeeId = result.EmployeeId,
            Nik = result.Nik,
            CardNumber = result.CardNumber,
            PhoneNumber = result.PhoneNumber,
            ExtensionNumber = result.ExtensionNumber,
            AccessId = result.AccessId
        }).ToListAsync();

        return dataEmployee;
    }

public async Task<List<EmployeeWithDetails>> GetEmployeesWithDetailsAsync()
{
    var query = from employee in _dbContext.Employees
                join company in _dbContext.AlocationTypes
                    on employee.CompanyId equals company.Id into companyJoin
                from company in companyJoin.DefaultIfEmpty() // Left join
                join department in _dbContext.Alocations
                    on employee.DepartmentId equals department.Id into departmentJoin
                from department in departmentJoin.DefaultIfEmpty() // Left join
                select new EmployeeWithDetails
                {
                    Id = employee.Id,
                    DivisionId = employee.DivisionId,
                    CompanyId = employee.CompanyId,
                    DepartmentId = employee.DepartmentId,
                    Name = employee.Name,
                    Nik = employee.Nik,
                    NikDisplay = employee.NikDisplay,
                    Photo = employee.Photo,
                    Email = employee.Email,
                    NoPhone = employee.NoPhone,
                    NoExt = employee.NoExt,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender,
                    Address = employee.Address,
                    CardNumber = employee.CardNumber,
                    Priority = employee.Priority,
                    IsVip = employee.IsVip,
                    VipApproveBypass = employee.VipApproveBypass,
                    VipLimitCapBypass = employee.VipLimitCapBypass,
                    VipLockRoom = employee.VipLockRoom,
                    CompanyName = company.Name,
                    DepartmentName = department.Name
                };

    // Apply ordering
    query = query.OrderBy(e => e.Priority);

    return await query.ToListAsync();
}




    public virtual async Task<IEnumerable<object>> GetSingleRoomsAsync()
    {
        //var query = "SELECT * FROM smart_meeting_room.smart_meeting_room.room WHERE is_deleted = 0 AND type_room = 'single'";
        //var singleRooms = await _dbContext.Rooms.FromSqlRaw(query).ToListAsync();

        //return singleRooms;
        return await _dbContext.Rooms
            .Where(e => e.IsDeleted == 0 && e.TypeRoom.ToLower() == "single") // Filter is_deleted
            .ToListAsync();
    }
    public async Task<List<RoomDetail?>> GetRoomDetailById(long? id)
    {
        return await _dbContext.RoomDetails
            .Where(m => m.RoomId == id)
            .ToListAsync();
    }


    public async Task<IEnumerable<object>> GetSettingRuleBookingAsync()
    {
        //return singleRooms;
        return await _dbContext.SettingRuleBookings// Filter is_deleted
            .ToListAsync();
    }


    public async Task<IEnumerable<object>> GetInvoiceStatusName()
    {
        //return singleRooms;
        return await _dbContext.SettingInvoiceTexts// Filter is_deleted
            .Where(m => m.IsDeleted == 0)
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> GetDataBuildingAsync(Dictionary<string, object> conditions)
    {
        //try
        //{
        var query = _dbContext.Buildings
            .Where(b => b.IsDeleted == 0)
            .OrderBy(b => b.Id)
            .Select(b => new BuildingDataDto
            {
                Id = b.Id,
                Name = b.Name,
                Image = b.Image,
                Description = b.Description,
                Timezone = b.Timezone,
                DetailAddress = b.DetailAddress,
                GoogleMap = b.GoogleMap,
                Koordinate = b.Koordinate,
                IsDeleted = b.IsDeleted,
                CreatedBy = b.CreatedBy,
                CreatedAt = b.CreatedAt,
                UpdatedBy = b.UpdatedBy,
                UpdatedAt = b.UpdatedAt,
                CountRoom = _dbContext.Rooms.Count(rr => rr.IsDeleted == 0 && rr.BuildingId == b.Id),
                CountFloor = _dbContext.BeaconFloors.Count(ff => ff.IsDeleted == 0 && ff.BuildingId == b.Id),
                CountDesk = _dbContext.DeskRooms
                    .Join(_dbContext.DeskRoomTables, dr => dr.Id, ddd => ddd.DeskRoomId, (dr, ddd) => new { dr, ddd })
                    .Count(dr_ddd => dr_ddd.ddd.IsDeleted == 0 && dr_ddd.dr.BuildingId == b.Id)
            });

        // Apply conditions in-memory
        var data = await query.ToListAsync();
        data = data.Where(b => conditions.All(c => c.Value.Equals(b.GetType().GetProperty(c.Key)?.GetValue(b)))).ToList();

        return data;
    }

    public async Task<IEnumerable<object>> GetDataFloorAsync(Dictionary<string, object> conditions)
    {
            var query = _dbContext.BuildingFloors
                .Join(_dbContext.Buildings,
                    floor => floor.BuildingId,
                    building => building.Id,
                    (floor, building) => new { floor, building })
                .Where(fb => fb.floor.IsDeleted == 0 && fb.building.IsDeleted == 0)
                .OrderBy(fb => fb.floor.Position)
                .Select(fb => new
                {
                    fb.floor,
                    fb.building
                });

            var data = await query.ToListAsync(); // Fetch data first

            // Apply conditions in-memory (client-side)
            var filteredData = data.Where(fb =>
                conditions.All(c =>
                {
                    var floorPropertyValue = fb.floor.GetType().GetProperty(c.Key)?.GetValue(fb.floor);
                    var buildingPropertyValue = fb.building.GetType().GetProperty(c.Key)?.GetValue(fb.building);
                    return (floorPropertyValue != null && floorPropertyValue.Equals(c.Value)) ||
                           (buildingPropertyValue != null && buildingPropertyValue.Equals(c.Value));
                })
            ).Select(fb => fb.floor).ToList();

            return filteredData;

        }

    public async Task<List<BuildingDataDto>> GetBuildingData()
    {
            var result = _dbContext.Buildings
                .Where(b => b.IsDeleted == 0) // Only include non-deleted buildings
                .Select(b => new BuildingDataDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Generate = b.Generate,
                    Image = b.Image,
                    Description = b.Description,
                    Timezone = b.Timezone,
                    DetailAddress = b.DetailAddress,
                    GoogleMap = b.GoogleMap,
                    Koordinate = b.Koordinate,
                    IsDeleted = b.IsDeleted,
                    CreatedBy = b.CreatedBy,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    UpdatedBy = b.UpdatedBy,

                    // Aggregate counts
                    CountRoom = _dbContext.Rooms
                        .Count(r => r.IsDeleted == 0 && r.BuildingId == b.Id),

                    CountFloor = _dbContext.BeaconFloors
                        .Count(f => f.IsDeleted == 0 && f.BuildingId == b.Id),

                    CountDesk = _dbContext.DeskRoomTables
                        .Join(
                            _dbContext.DeskRooms,
                            ddd => ddd.DeskRoomId,
                            dr => dr.Id,
                            (ddd, dr) => new { ddd, dr }
                        )
                        .Count(joined => joined.ddd.IsDeleted == 0 && joined.dr.BuildingId == b.Id)
                })
                .OrderBy(b => b.Id)
                .ToList();

        return result;
    }

    public async Task<object> GetDataRoom()
    {


        var query = from room in _dbContext.Rooms
                    join RoomAutomation in _dbContext.RoomAutomations
                        on room.AutomationId equals RoomAutomation.Id into raJoin
                    from RoomAutomation in raJoin.DefaultIfEmpty() // Left join
                    join Building in _dbContext.Buildings
                        on room.BuildingId equals Building.Id into buildingJoin
                    from Building in buildingJoin.DefaultIfEmpty() // Left join
                    select new RoomDetailDto
                    {

                        Id = room.Id,
                        Radid = room.Radid,
                        BuildingId = room.BuildingId,
                        FloorId = room.FloorId,
                        TypeRoom = room.TypeRoom,
                        Name = room.Name,
                        Capacity = room.Capacity,
                        Description = room.Description,
                        GoogleMap = room.GoogleMap,
                        IsAutomation = room.IsAutomation,
                        AutomationId = room.AutomationId,
                        FacilityRoom = room.FacilityRoom,
                        WorkDay = room.WorkDay,
                        WorkTime = room.WorkTime,
                        WorkStart = room.WorkStart,
                        WorkEnd = room.WorkEnd,
                        Image = room.Image,
                        Image2 = room.Image2,
                        MultipleImage = room.MultipleImage,
                        Price = room.Price,
                        Location = room.Location,
                        IsDisabled = room.IsDisabled,
                        IsBeacon = room.IsBeacon,
                        CreatedBy = room.CreatedBy,
                        CreatedAt = room.CreatedAt,
                        UpdatedAt = room.UpdatedAt,
                        IsDeleted = room.IsDeleted,
                        IsConfigSettingEnable = room.IsConfigSettingEnable,
                        ConfigRoomForUsage = room.ConfigRoomForUsage,
                        IsEnableApproval = room.IsEnableApproval,
                        ConfigApprovalUser = room.ConfigApprovalUser,
                        IsEnablePermission = room.IsEnablePermission,
                        ConfigPermissionUser = room.ConfigPermissionUser,
                        ConfigPermissionCheckin = room.ConfigPermissionCheckin,
                        ConfigPermissionEnd = room.ConfigPermissionEnd,
                        ConfigMinDuration = room.ConfigMinDuration,
                        ConfigMaxDuration = room.ConfigMaxDuration,
                        ConfigAdvanceBooking = room.ConfigAdvanceBooking,
                        IsEnableRecurring = room.IsEnableRecurring,
                        IsEnableCheckin = room.IsEnableCheckin,
                        ConfigAdvanceCheckin = room.ConfigAdvanceCheckin,
                        ConfigReleaseRoomCheckinTimeout = room.ConfigReleaseRoomCheckinTimeout,
                        ConfigParticipantCheckinCount = room.ConfigParticipantCheckinCount,
                        IsEnableCheckinCount = room.IsEnableCheckinCount,
                        ConfigGoogle = room.ConfigGoogle,
                        ConfigMicrosoft = room.ConfigMicrosoft,
                        RaId = RoomAutomation.Id,
                        RaName = RoomAutomation.Name,
                        BuildingName = Building.Name,
                        BuildingDetail = Building.DetailAddress,
                        BuildingGoogleMap = Building.GoogleMap

                    };


        var data = await query.ToListAsync(); 

        return data;

    }
    //public async Task<object> getDataEmployeeAsync()
    //{


    //    var query = from Employee in _dbContext.Employees
    //                join AlocationType in _dbContext.AlocationTypes
    //                    on Employee.CompanyId equals AlocationType.Id into eatJoin
    //                from AlocationType in eatJoin.DefaultIfEmpty() // Left join
    //                join Alocation in _dbContext.Alocations
    //                    on Employee.DepartmentId equals Alocation.Id into aeJoin
    //                from Alocation in aeJoin.DefaultIfEmpty() // Left join
    //                select new
    //                {
    //                };
    //    var data = await query.OrderBy(Employee => Employee.Id).ToListAsync();

    //    return data;
    //}


    public async Task<object> GetBookingMenuByConditions(string menu_name = "Booking", long? LevelId = 1)
    {
        var query = from Level in _dbContext.Levels
                    join LevelDetail in _dbContext.LevelDetails
                        on Level.Id equals LevelDetail.LevelId into lldJoin
                    from LevelDetail in lldJoin.DefaultIfEmpty() // Left join
                    join Menu in _dbContext.Menus
                        on LevelDetail.MenuId equals Menu.Id into mldJoin
                    from Menu in mldJoin.DefaultIfEmpty() // Left join
                    join MenuGroup in _dbContext.MenuGroups
                        on Menu.MenuGroupId equals MenuGroup.Id into lmgJoin
                    from MenuGroup in lmgJoin.DefaultIfEmpty() // Left join
                    where Level.Id == LevelId
                          && Level.IsDeleted == 0
                          && (Menu.IsDeleted == 0 || Menu == null) // Handle left join null case
                    orderby Menu.Sort
                    select new BookingMenuDto
                    {
                        LevelId = Level.Id,
                        LevelName = Level.Name,
                        MenuGroupId = MenuGroup != null ? MenuGroup.Id : (long?)null,
                        GMenuName = MenuGroup != null ? MenuGroup.Name : null,
                        GMenuId = MenuGroup != null ? MenuGroup.Id : (long?)null,
                        MgIcon = MenuGroup != null ? MenuGroup.Icon : null,
                        MenuName = Menu != null ? Menu.Name : null,
                        Url = Menu != null ? Menu.Url : null,
                        Icon = Menu != null ? Menu.Icon : null,
                        IsChild = Menu != null ? Menu.IsChild : null,
                    };

        var data = await query.ToListAsync();

        return data;
    }

}
