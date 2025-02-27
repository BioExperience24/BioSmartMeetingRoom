
using System.Security.Claims;
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation;

public class EmployeeService : BaseService<EmployeeViewModel, Employee>, IEmployeeService
{
    private readonly IMapper __mapper;
    private readonly IHttpContextAccessor _httpCont;

    private readonly EmployeeRepository _repo;

    private readonly UserConfigRepository _userConfigRepo;

    private readonly AlocationMatrixRepository _alocationMatrixRepo;

    private readonly UserRepository _userRepo;

    private readonly IUserService _userService;

    private readonly IAttachmentListService _attachmentListService;

    public EmployeeService(
        IMapper mapper,
        EmployeeRepository repo,
        UserConfigRepository userConfigRepo,
        AlocationMatrixRepository alocationMatrixRepo,
        UserRepository userRepo,
        IUserService userService,
        IAttachmentListService attachmentListService,
        IHttpContextAccessor httpCont,
        IConfiguration config)
        : base(repo, mapper)
    {
        _repo = repo;
        __mapper = mapper;
        _userConfigRepo = userConfigRepo;
        _alocationMatrixRepo = alocationMatrixRepo;
        _userRepo = userRepo;
        _userService = userService;

        attachmentListService.SetTableFolder(
            config["UploadFileSetting:tableFolder:employee"] ?? "employee");
        attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
        attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
        attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
        _attachmentListService = attachmentListService;
        _httpCont = httpCont;
    }

    public async Task<IEnumerable<EmployeeVMResp>> GetItemsAsync()
    {
        var employees = await _repo.GetItemsAsync();

        return __mapper.Map<List<EmployeeVMResp>>(employees);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repo.GetCountAsync();
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetItemsWithoutUserAsync()
    {
        var employees = await _repo.GetItemsWithoutUserAsync();

        return __mapper.Map<List<EmployeeViewModel>>(employees);
    }

    public async Task<EmployeeViewModel> GetProfileAsync()
    {
        var nik = _httpCont?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        
        var employee = await _repo.GetItemByNikAsync(nik!);

        return __mapper.Map<EmployeeViewModel>(employee);
    }

    public async Task<EmployeeViewModel?> CreateAsync(EmployeeVMCreateFR request)
    {

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var userConfig = await _userConfigRepo.GetOneItem();

                if (userConfig == null)
                {
                    return null;
                }

                var now = DateTime.Now;

                var gid = now.ToString("yyyyMMddHHmmss"); // generate ID

                AlocationMatrix aMatrix = new AlocationMatrix
                {
                    AlocationId = request.DepartmentId,
                    Nik = gid
                };

                var alocationMatrixs = await _alocationMatrixRepo.GetItemsWithEntityAsync(aMatrix);

                // Delete data alocation_matrix
                if (
                    alocationMatrixs.Any()
                    && await _alocationMatrixRepo.DeleteRangeAsync(alocationMatrixs) > 0)
                {
                    // TODO: Create activity log delete alocation_matrix
                    // On progress
                }

                // Insert data alocation_matrix
                if (await _alocationMatrixRepo.AddAsync(aMatrix) != null)
                {
                    // TODO: Create activity log create alocation_matrix
                    // On progress
                }

                var employeeItem = new Employee
                {
                    CompanyId = request.CompanyId,
                    DepartmentId = request.DepartmentId,
                    Name = request.Name,
                    NikDisplay = request.NikDisplay,
                    Email = request.Email,
                    NoPhone = request.NoPhone,
                    NoExt = request.NoExt,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    Address = request.Address ?? "",
                    CardNumber = request.CardNumber ?? "",
                    CardNumberReal = request.CardNumber ?? "",
                    IsVip = request.IsVip == "on" ? 1 : 0,
                    VipApproveBypass = request.VipApproveBypass == "on" ? 1 : 0,
                    VipLimitCapBypass = request.VipLimitCapBypass == "on" ? 1 : 0,
                    VipLockRoom = request.VipLockRoom == "on" ? 1 : 0,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDeleted = 0,
                    Id = gid,
                    Nik = gid,
                };

                if (request.Photo != null)
                {
                    employeeItem.Photo = request.Photo;
                }

                var employee = await _repo.AddAsync(employeeItem);

                if (employee == null)
                {
                    return null;
                }

                // TODO: Create activity log create employee
                // On progress

                // Generate username
                var username = request.NikDisplay!.Trim();

                // Generate password
                var password = _Base64.Encrypt(userConfig.DefaultPassword);

                var userItem = new User
                {
                    Name = request.Name,
                    Username = username,
                    Password = password,
                    RealPassword = userConfig.DefaultPassword,
                    EmployeeId = employee.Id!,
                    IsDisactived = 0,
                    // CreatedBy = // uncomment jika auth sudah ada
                    CreatedAt = now,
                    AccessId = "1",
                    IsDeleted = 0,
                    LevelId = 2
                };

                var user = await _userRepo.Create(userItem);

                if (user == null)
                {
                    return null;
                }

                // TODO: Create activity log create user
                // On progress

                scope.Complete();

                return __mapper.Map<EmployeeViewModel>(employee);
            }
            catch (Exception)
            {
                if (request.Photo != null)
                {
                    await _attachmentListService.DeleteFile(request.Photo);
                }
                throw;
            }
        }

    }

    public async Task<EmployeeViewModel?> UpdateAsync(string id, EmployeeVMDefaultFR request)
    {
        var employee = await _repo.GetByIdAsync(id);

        if (employee == null)
        {
            return null;
        }

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                if (employee.DepartmentId != request.DepartmentId)
                {
                    AlocationMatrix wmatrix = new AlocationMatrix
                    {
                        AlocationId = employee.DepartmentId,
                        Nik = id
                    };

                    var alocationMatrixs = await _alocationMatrixRepo.GetItemsWithEntityAsync(wmatrix);

                    // Delete data alocation_matrix
                    if (
                        alocationMatrixs.Any()
                        && await _alocationMatrixRepo.DeleteRangeAsync(alocationMatrixs) > 0)
                    {
                        // TODO: Create activity log delete alocation_matrix
                        // On progress
                    }

                    AlocationMatrix insertMatrix = new AlocationMatrix
                    {
                        AlocationId = request.DepartmentId,
                        Nik = id
                    };

                    // Insert data alocation_matrix
                    if (await _alocationMatrixRepo.AddAsync(insertMatrix) != null)
                    {
                        // TODO: Create activity log create alocation_matrix
                        // On progress
                    }
                }

                __mapper.Map(request, employee);

                DateTime now = DateTime.Now;

                employee.UpdatedAt = now;
                employee.Address = request.Address ?? "";
                employee.CardNumber = request.CardNumber ?? "";
                employee.CardNumberReal = request.CardNumber ?? "";

                await _repo.UpdateAsync(employee);

                scope.Complete();

                var result = __mapper.Map<EmployeeViewModel>(employee);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<EmployeeViewModel?> UpdateVipAsync(string id, EmployeeVMUpdateVipFR request)
    {
        var employee = await _repo.GetByIdAsync(id);

        if (employee == null)
        {
            return null;
        }

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                DateTime now = DateTime.Now;

                employee.IsVip = request.IsVip == "on" ? 1 : 0;
                employee.VipApproveBypass = request.VipApproveBypass == "on" && request.IsVip == "on" ? 1 : 0;
                employee.VipLimitCapBypass = request.VipLimitCapBypass == "on" && request.IsVip == "on" ? 1 : 0;
                employee.VipLockRoom = request.VipLockRoom == "on" && request.IsVip == "on" ? 1 : 0;
                employee.UpdatedAt = now;

                await _repo.UpdateAsync(employee);

                scope.Complete();

                var result = __mapper.Map<EmployeeViewModel>(employee);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<EmployeeViewModel?> DeleteAsync(string id)
    {
        var employee = await _repo.GetByIdAsync(id);

        if (employee == null)
        {
            return null;
        }

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                // DateTime now = DateTime.Now;
                // employee.UpdatedAt = now;
                employee.IsDeleted = 1;

                await _repo.UpdateAsync(employee);

                scope.Complete();

                var result = __mapper.Map<EmployeeViewModel>(employee);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
