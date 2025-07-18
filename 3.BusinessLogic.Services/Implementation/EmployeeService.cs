
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

    private readonly AlocationTypeRepository _alocationTypesRepo;
    private readonly AlocationRepository _alocationRepo;

    private readonly IUserService _userService;

    private readonly IAttachmentListService _attachmentListService;

    private readonly IConfiguration _config;

    public EmployeeService(
        IMapper mapper,
        EmployeeRepository repo,
        UserConfigRepository userConfigRepo,
        AlocationMatrixRepository alocationMatrixRepo,
        UserRepository userRepo,
        AlocationTypeRepository alocationTypesRepo,
        AlocationRepository alocationRepo,
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
        _alocationTypesRepo = alocationTypesRepo;
        _alocationRepo = alocationRepo;
        _userService = userService;

        attachmentListService.SetTableFolder(
            config["UploadFileSetting:tableFolder:employee"] ?? "employee");
        attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
        attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
        attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
        _attachmentListService = attachmentListService;
        _httpCont = httpCont;
        _config = config;
    }

    public async Task<IEnumerable<EmployeeVMResp>> GetItemsAsync()
    {
        var employees = await _repo.GetItemsAsync();

        var collections = __mapper.Map<List<EmployeeVMResp>>(employees);

        collections.Select(x =>
        {
            x.HeadEmployeeId = x.HeadEmployeeId ?? string.Empty;
            return x;
        }).ToList();

        return collections;
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
        // check if the employee nrp already exists
        var existEmployee = await _repo.GetItemByNikDisplayAsync(request.NikDisplay!);
        if (existEmployee != null)
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
                var authUserNIK = _httpCont?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

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
                { }

                // Insert data alocation_matrix
                if (await _alocationMatrixRepo.AddAsync(aMatrix) != null)
                { }

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
                    HeadEmployeeId = request.HeadEmployeeId ?? string.Empty,
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
                    CreatedBy = authUserNIK,
                    CreatedAt = now,
                    AccessId = "1#2#3",
                    IsDeleted = 0,
                    LevelId = 2
                };

                var user = await _userRepo.Create(userItem);

                if (user == null)
                {
                    return null;
                }

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

        if (employee.NikDisplay != request.NikDisplay)
        {
            // check if the employee nrp already exists
            var existEmployee = await _repo.GetItemByNikDisplayAsync(request.NikDisplay!);
            if (existEmployee != null)
            {
                return null;
            }
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
                employee.HeadEmployeeId = request.HeadEmployeeId ?? "";

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

    public async Task<EmployeeViewModel?> UpdateProfile(string id, EmployeeVMDefaultFR request)
    {
        var employee = await _repo.GetItemByIdAsync(id);

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
                __mapper.Map(request, employee);

                DateTime now = DateTime.Now;

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

        if (employee == null || employee.IsProtected == 1)
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
                var user = await _userRepo.GetUserByEmployeeIdAsync(employee.Id!);

                // DateTime now = DateTime.Now;
                // employee.UpdatedAt = now;
                employee.IsDeleted = 1;

                await _repo.UpdateAsync(employee);

                if (user != null)
                {
                    user.IsDeleted = 1;
                    await _userRepo.Update(user);
                }

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

    public async Task<ReturnalModel> ImportAsync(EmployeeVMImportFR request)
    {
        ReturnalModel ret = new ReturnalModel();

        if (request.FileImport == null || request.FileImport.Length == 0)
        {
            // ret.StatusCode = StatusCodes.Status400BadRequest;
            ret.Status = ReturnalType.Failed;
            ret.Message = "File not found";
            return ret;
        }

        var errValidationMessage = validationFileImport(request.FileImport);
        if (!string.IsNullOrEmpty(errValidationMessage))
        {
            // ret.StatusCode = StatusCodes.Status400BadRequest;
            ret.Status = ReturnalType.Failed;
            ret.Message = errValidationMessage;
            return ret;
        }

        var data = await generateDataFromCsv(request.FileImport!);

        if (data == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "File not valid";
            return ret; 
        }

        // check if the employee nrp already exists
        string[] EmployeeNikDisplays = data.Select(x => x.Nrp).Distinct().ToArray();

        var existNrp = await _repo.GetItemsByNikDisplayAsync(EmployeeNikDisplays);
        
        if (existNrp != null && existNrp.Any())
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Employee NRP already exists";
            return ret;
        }

        // check if the NRP (Nik Display) of the head employee exists or not
        string[] headEmployeeNikDisplays = data.Select(x => x.HeadEmployeeId).Distinct().ToArray();

        var headEmployees = await _repo.GetItemsByNikDisplayAsync(headEmployeeNikDisplays);
        if (!headEmployees.Any() || headEmployees.Count() != headEmployeeNikDisplays.Length)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Head Employee not found";
            return ret;
        }
        
        // update headEmployeeId data with the id from headEmployees
        data = data.Select(item => {
            var headEmployeeId = headEmployees.FirstOrDefault(x => x.NikDisplay == item.HeadEmployeeId)?.Id;

            item.HeadEmployeeId = headEmployeeId ?? string.Empty;
            return item;
        }).ToList();

        // check if the departmentId exists or not
        string[] departmentCodes = data.Select(x => x.DepartmentId).Distinct().ToArray();

        var departments = await _alocationRepo.GetItemsByDepartmentCode(departmentCodes);
        if (!departments.Any() || departments.Count() != departmentCodes.Length)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Department not found";
            return ret;
        }

        // update DepartmentId data with the id from departments
        data = data.Select(item => {
            var departmentId = departments.FirstOrDefault(x => x.DepartmentCode == item.DepartmentId)?.Id;

            item.DepartmentId = departmentId ?? string.Empty;
            return item;
        }).ToList();

        string[] gids = new string[data.Count()];
        List<Employee> employees = new List<Employee>();
        List<User> users = new List<User>();
        List<AlocationMatrix> aMatrixs = new List<AlocationMatrix>();

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var authUserNIK = _httpCont?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
                DateTime now = DateTime.Now;

                var genders = data.Select(x => x.Gender).Distinct().ToList();
                var birthDates = data.Select(x => x.BirthDate).Distinct().ToList();
                var alocationTypeIds = data.Select(x => x.CompanyId).Distinct().ToArray();
                var alocationIds = data.Select(x => x.DepartmentId).Distinct().ToArray();

                if (!validationGender(genders))
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Gender not valid. only male, female, or other";
                    return ret;
                }

                if (birthDates != null & !validationBirthDate(birthDates!, new[] { "yyyy-MM-dd", "dd/MM/yyyy" }))
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Birthdate not valid. only yyyy-MM-dd or dd/MM/yyyy format";
                    return ret;
                }

                var countCompany = await _alocationTypesRepo.CountByIds(alocationTypeIds);
                var countDepartment = await _alocationRepo.CountByIds(alocationIds);

                Console.WriteLine($"countCompany: {countCompany}");
                Console.WriteLine($"countDepartment: {countDepartment}");
                Console.WriteLine($"alocationTypeIds.Length: {alocationTypeIds.Length}");
                Console.WriteLine($"alocationIds.Length: {alocationIds.Length}");

                if (alocationTypeIds.Length != countCompany || alocationIds.Length != countDepartment)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Company or Department not found";
                    return ret;
                }

                var userConfig = await _userConfigRepo.GetOneItem();

                if (userConfig == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "User config not found";
                    return ret;
                }

                gids = generateGIds(data.Count());

                foreach (var item in data.Select((val, key) => new { val, key }))
                {
                    var gid = gids[item.key];

                    // Collect AlocationMatrix
                    AlocationMatrix aMatrix = new AlocationMatrix
                    {
                        AlocationId = item.val.DepartmentId,
                        Nik = gid
                    };
                    
                    aMatrixs.Add(aMatrix);

                    // Collect Employee
                    var birthDate = item.val.BirthDate != null ? _String.ToDateOnlyMultiFormat(item.val.BirthDate, new[] { "yyyy-MM-dd", "dd/MM/yyyy" }) : (DateOnly?)null;
                    var employee = new Employee
                    {
                        CompanyId = item.val.CompanyId,
                        DepartmentId = item.val.DepartmentId,
                        Name = item.val.Name,
                        NikDisplay = item.val.Nrp,
                        Email = item.val.Email,
                        NoPhone = item.val.NoPhone,
                        NoExt = item.val.NoExt,
                        BirthDate = birthDate,
                        Gender = item.val.Gender?.ToLowerInvariant() ?? "other",
                        Address = item.val.Address ?? string.Empty,
                        CardNumber = item.val.CardNumber ?? string.Empty,
                        CardNumberReal = item.val.CardNumber ?? string.Empty,
                        IsVip = item.val.IsVip,
                        VipApproveBypass = item.val.VipApproveBypass,
                        VipLimitCapBypass = 0,
                        VipLockRoom = 0,
                        CreatedAt = now,
                        UpdatedAt = now,
                        IsDeleted = 0,
                        Id = gid,
                        Nik = gid,
                        HeadEmployeeId = item.val.HeadEmployeeId ?? string.Empty,
                    };
                    employees.Add(employee);
                    
                    // Collect User
                    // Generate username
                    var username = item.val.Nrp!.Trim();

                    // Generate password
                    var password = _Base64.Encrypt(userConfig.DefaultPassword);

                    var user = new User
                    {
                        Name = item.val.Name,
                        Username = username,
                        Password = password,
                        RealPassword = userConfig.DefaultPassword,
                        EmployeeId = employee.Id!,
                        IsDisactived = 0,
                        CreatedBy = authUserNIK ?? string.Empty,
                        CreatedAt = now,
                        AccessId = "1#2#3",
                        IsDeleted = 0,
                        LevelId = 2
                    };
                    users.Add(user);
                }

                // bulk insert AlocationMatrix
                if (aMatrixs.Any())
                {
                    await _alocationMatrixRepo.AddRangeAsync(aMatrixs);
                }

                // bulk insert Employee
                if (employees.Any())
                {
                    await _repo.CreateBulk(employees);
                }

                // bulk insert User
                if (users.Any())
                {
                    await _userRepo.CreateBulk(users);
                }

                scope.Complete();

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public async Task<ReturnalModel> GetHeadEmployeesAsync()
    {
        var ret = new ReturnalModel();

        var headEmployees = await _repo.GetHeadEmployeesAsync();

        ret.Collection = __mapper.Map<List<EmployeeViewModel>>(headEmployees);

        return ret;
    }
    
    private string validationFileImport(IFormFile file)
    {
        var allowedExtensions = _config["UploadFileSetting:importExtensionAllowed"]?.Split('#') ?? new[] { ".csv" };
        var allowedMimeTypes  = _config["UploadFileSetting:importContentTypeAllowed"]?.Split('#') ?? new[] { "text/csv", "application/vnd.ms-excel" };
        var sizeLimit = Convert.ToInt32(_config["UploadFileSetting:importSizeLimit"] ?? "8");
        long maxFileSize = sizeLimit * 1024 * 1024; // 8 MB in bytes

        // Validasi ukuran file max 8 MB
        if (file.Length > maxFileSize)
        {
            return $"Ukuran file maksimal adalah {sizeLimit} MB.";
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant().TrimStart('.');
        var contentType = file.ContentType.ToLowerInvariant();

        if (!allowedExtensions.Contains(fileExtension) || !allowedMimeTypes.Contains(contentType))
        {
            return $"Only {string.Join(", ", allowedExtensions)} files are allowed.";
        }

        return string.Empty;
    }

    private async Task<IEnumerable<EmployeeVMImportData>?> generateDataFromCsv(IFormFile file)
    {
        var result = new List<EmployeeVMImportData>();

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);
        
        // Baca header (baris pertama)
        var headerLine = await reader.ReadLineAsync();
        if (string.IsNullOrWhiteSpace(headerLine))
        {
            return null;
        }

        // Deteksi separator berdasarkan jumlah kolom
        char separator = headerLine.Contains(';') && headerLine.Split(';').Length > 1 ? ';' : ',';

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Split menggunakan separator yang terdeteksi
            var parts = line.Split(separator);

            if (parts.Length < 16) continue; // Validasi jumlah kolom

            var data = new EmployeeVMImportData
            {
                Company = parts[0],
                CompanyId = parts[1],
                DepartmentName = parts[2],
                DepartmentId = parts[3],
                HeadEmployeeId = parts[4],
                Name = parts[5],
                Email = parts[6],
                Nrp = parts[7],
                BirthDate = parts[8],
                Gender = parts[9],
                Address = parts[10],
                CardNumber = parts[11],
                NoPhone = parts[12],
                NoExt = parts[13],
                IsVip = int.TryParse(parts[14], out var isVip) ? isVip : 0,
                VipApproveBypass = int.TryParse(parts[15], out var vipApproveBypass) ? vipApproveBypass : 0
            };

            result.Add(data);
        }

        return result;
    }

    private string[] generateGIds(int count)
    {
        var gids = new string[count];

        var format = "yyyyMMddHHmmss";
        DateTime baseTime = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            DateTime adjustedTime = baseTime.AddSeconds(i);
            string timestamp = adjustedTime.ToString(format);
            gids[i] = timestamp;
        }

        return gids;
    }

    private bool validationBirthDate(IEnumerable<string> input, string[] formats)
    {
        foreach (var date in input)
        {
            if (!DateOnly.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return false; // Invalid date format
            }
        }

        return true; // All dates are valid
    }

    private static readonly HashSet<string> ValidGenders = new(StringComparer.OrdinalIgnoreCase)
    {
        "male", "female", "other"
    };

    private bool validationGender(IEnumerable<string> input)
    {
        // var validOptions = new HashSet<string> { "male", "female", "other" };
        return input.All(x => ValidGenders.Contains(x));
    }
}
