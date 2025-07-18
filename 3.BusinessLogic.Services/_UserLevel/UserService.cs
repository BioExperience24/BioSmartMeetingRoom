using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;
using Azure.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class UserService : BaseLongService<UserViewModel, User>, IUserService
{
    private readonly UserRepository _repo;
    private readonly EmployeeRepository _employeeRepo;
    private readonly TokenManagement _tokenManagement;
    private readonly IHttpContextAccessor _httpCont;

    private readonly IMapper __mapper;
    private readonly ILevelService _levelService;

    public UserService(UserRepository repo, IMapper mapper, EmployeeRepository employeeRepo,
        ILevelService levelService, IOptions<TokenManagement> tokenManagement, IHttpContextAccessor httpCont)
        : base(repo, mapper)
    {
        _repo = repo;
        __mapper = mapper;
        _employeeRepo = employeeRepo;
        _levelService = levelService;
        _tokenManagement = tokenManagement.Value;
        _httpCont = httpCont;
    }

    public override async Task<UserViewModel?> GetById(long id)
    {
        var user = await _repo.GetById(id);

        if (user == null)
        {
            return null;
        }

        var result = _mapper.Map<UserViewModel>(user);

        result.Password = _Base64.Decrypt(result.Password);

        return result;
    }

    public async Task<IEnumerable<UserViewModel>> GetItemsAsync()
    {
        var authUserNIK = _httpCont?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var filter = new UserFilter { };
        if (!string.IsNullOrEmpty(authUserNIK))
        {
            filter.ExceptEmployeeId = authUserNIK;
        }

        var items = await _repo.GetItemsFilteredByEntityAsync(filter);

        return __mapper.Map<List<UserViewModel>>(items);
    }

    public async Task<UserViewModel?> CreateAsync(UserVMCreateFR request)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                DateTime now = DateTime.Now;

                var employee = await _employeeRepo.GetByIdAsync(request.EmployeeId.Trim());

                if (employee == null)
                {
                    return null;
                }

                var item = __mapper.Map<User>(request);
                item.Name = employee.Name;
                item.RealPassword = request.Password.Trim();//harusnnya gk disimpen di db ini, percuma amat diencrypt
                item.Password = _Base64.Encrypt(request.Password.Trim());
                item.CreatedAt = now;
                item.UpdatedAt = now;
                // item.CreatedBy = ""; // uncomment jika sudah ada auth user
                item.IsDeleted = 0;

                if (request.AccessId.Any())
                {
                    item.AccessId = string.Join(
                        "#",
                        request.AccessId
                            .Where(s => !string.IsNullOrEmpty(s)) // Mengabaikan Elemen Kosong atau Null
                            .Select(s => s.Trim()) // Menghapus spasi tambahan
                    );
                }

                var user = await _repo.Create(item);

                scope.Complete();

                return __mapper.Map<UserViewModel>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<UserViewModel?> UpdateAsync(UserVMUpdateFR request)
    {
        DateTime now = DateTime.Now;

        var user = await _repo.GetById(request.Id);

        if (user == null)
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
                _mapper.Map(request, user);

                user.RealPassword = request.Password.Trim();
                user.Password = _Base64.Encrypt(request.Password.Trim());
                user.UpdatedAt = now;
                // user.UpdatedBy = ""; // uncomment jika sudah ada auth user
                user.IsDeleted = 0;

                if (request.AccessId.Any())
                {
                    user.AccessId = string.Join(
                        "#",
                        request.AccessId
                            .Where(s => !string.IsNullOrEmpty(s)) // Mengabaikan Elemen Kosong atau Null
                            .Select(s => s.Trim()) // Menghapus spasi tambahan
                    );
                }

                await _repo.Update(user);

                scope.Complete();

                var result = _mapper.Map<UserViewModel>(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<UserViewModel?> DisableAsync(UserVMDisableFR request)
    {
        DateTime now = DateTime.Now;
        var user = await _repo.GetById(request.Id);

        if (user == null)
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
                _mapper.Map(request, user);

                user.UpdatedAt = now;
                // user.UpdatedBy = ""; // uncomment jika sudah ada auth user

                await _repo.Update(user);

                scope.Complete();

                var result = _mapper.Map<UserViewModel>(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<UserViewModel?> DeleteAsync(UserVMDeleteFR request)
    {
        DateTime now = DateTime.Now;
        var user = await _repo.GetById(request.Id);

        if (user == null)
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
                _mapper.Map(request, user);

                user.UpdatedAt = now;
                // user.UpdatedBy = ""; // uncomment jika sudah ada auth user
                user.IsDeleted = 1;

                await _repo.Update(user);

                scope.Complete();

                var result = _mapper.Map<UserViewModel>(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    //make sure, before this method called with isWebiew is true, the function already check the request is validated
    public async Task<ReturnalModel> CheckLogin(LoginModel request, bool isWebview = false)
    {
        ReturnalModel ret = new();
        User? user = null; // Ensure `user` is accessible outside blocks

        if (!isWebview)
        {
            var encryptPass = _Base64.Encrypt(request.Password.Trim());
            user = await _repo.GetUserByUsernamePassword(request.Username, encryptPass);

            if (user == null)
            {
                return new ReturnalModel
                {
                    Title = ReturnalType.Failed,
                    Status = ReturnalType.Failed,
                    Message = "Invalid username or password.",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
        else
        {
            user = await _repo.GetUserByUsername(request.Username);
        }

        if (user == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "User not found.",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var getLevel = await _levelService.GetLevel(user.LevelId);

        // get side menu by level (role)
        List<LevelMenu> getMenu = await _levelService.GetMenu(user.LevelId);
        List<LevelMenu> getMenuParent = getMenu.Where(x => x.IsChild == 0).ToList();
        List<LevelMenu> getMenuChild = getMenu.Where(x => x.IsChild != 0).ToList();
        List<LevelMenu> getGroupMenu = getMenuChild.GroupBy(x => x.MenuGroupId).Select(x => x.First()).ToList();
        var menuMap = _mapper.Map<List<MenuVM>>(getMenuParent);
        if (user.LevelId == 2)
        {
             List<MenuVM> listAproval = [
                
             ];

            List<MenuVM> listMeetingRoom = [
                
             ];

            List<MenuVM> listPantry = [
                
             ];

            if (user.AccessId!.Contains('1'))
            {

                listMeetingRoom.Add(new MenuVM
                {
                    MenuName = "Room Schedule",
                    MenuIcon = "schedule",
                    MenuUrl = "/booking",
                    MenuSort = 0,
                    ModuleText = "",  
                });

                listMeetingRoom.Add(new MenuVM
                {
                    MenuName = "Room Usage",
                    MenuIcon = "description",
                    MenuUrl = "/report-usage",
                    MenuSort = 0,
                    ModuleText = "",  
                });

                menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "MeetingRoom",
                        MenuIcon = "input",
                        MenuUrl = "#",
                        MenuSort = 0,
                        ModuleText = "",
                        Child = listMeetingRoom
                    }
                );
                
            }
            var employee = await _employeeRepo.GetByIdAsync(user.EmployeeId);
            var checkAsHeadEmployee = await _employeeRepo.GetByHeadEmployeeId(employee.Id);
            if(user.AccessId!.Contains('2'))
            {
                    listPantry.Add(new MenuVM
                {
                    MenuName = "Order Management",
                    MenuIcon = "schedule",
                    MenuUrl = "/pantry-transaction",
                    MenuSort = 4,
                    ModuleText = "",  
                });

                menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "Snack & Pantry",
                        MenuIcon = "local_cafe",
                        MenuUrl = "#",
                        MenuSort = 4,
                        ModuleText = "",
                        Child = listPantry
                    }
                );

     
            }
            if(user.AccessId!.Contains('3') && checkAsHeadEmployee == null)
            {
                listAproval.Add(new MenuVM
                {
                    MenuName = "Approval  Order",
                    MenuIcon = "open_with",
                    MenuUrl = "/approval-order",
                    MenuSort = 2,
                    ModuleText = "",
                });

            }
            if(user.AccessId!.Contains('4'))
            {
                listAproval.Add(new MenuVM
                {
                    MenuName = "Approval Meeting",
                    MenuIcon = "done_all",
                    MenuUrl = "/approval-meeting",
                    MenuSort = 1,
                    ModuleText = "",
                });
            }

             if(user.AccessId!.Contains('3') || user.AccessId!.Contains('4'))
             {
                menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "Approval",
                        MenuIcon = "done_all",
                        MenuUrl = "#",
                        MenuSort = 1,
                        ModuleText = "",
                        Child = listAproval
                    }
                );
             }
            if (checkAsHeadEmployee != null)
           {     
                listAproval.Add(new MenuVM
                {
                    MenuName = "Approval  Order",
                    MenuIcon = "open_with",
                    MenuUrl = "/approval-order",
                    MenuSort = 2,
                    ModuleText = "",
                });
            }
          
             
        }
        else if(user.LevelId == 6)
        {
            List<MenuVM> listHelp = [
                
             ];
               List<MenuVM> listAproval = [
                
             ];

            List<MenuVM> listMeetingRoom = [
                
             ];

            List<MenuVM> listPantry = [
                
             ];

            if (user.AccessId!.Contains('1'))
            {
                listMeetingRoom.Add(new MenuVM
                {
                    MenuName = "Room Schedule",
                    MenuIcon = "schedule",
                    MenuUrl = "/booking",
                    MenuSort = 0,
                    ModuleText = "",  
                });
                    listMeetingRoom.Add(new MenuVM
                {
                    MenuName = "Room Management",
                    MenuIcon = "management",
                    MenuUrl = "/room",
                    MenuSort = 0,
                    ModuleText = "",  
                });
                listMeetingRoom.Add(new MenuVM
                {
                    MenuName = "Room Usage",
                    MenuIcon = "description",
                    MenuUrl = "/report-usage",
                    MenuSort = 0,
                    ModuleText = "",  
                });

                    menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "MeetingRoom",
                        MenuIcon = "input",
                        MenuUrl = "#",
                        MenuSort = 0,
                        ModuleText = "",
                        Child = listMeetingRoom
                    }
                );
  
                
            }
            var employee = await _employeeRepo.GetByIdAsync(user.EmployeeId);
            var checkAsHeadEmployee = await _employeeRepo.GetByHeadEmployeeId(employee.Id);
            if(user.AccessId!.Contains('2'))
            {
                    listPantry.Add(new MenuVM
                {
                    MenuName = "Order Management",
                    MenuIcon = "schedule",
                    MenuUrl = "/pantry-transaction",
                    MenuSort = 4,
                    ModuleText = "",  
                });

                menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "Snack & Pantry",
                        MenuIcon = "local_cafe",
                        MenuUrl = "#",
                        MenuSort = 4,
                        ModuleText = "",
                        Child = listPantry
                    }
                );
            }
            if(user.AccessId!.Contains('3'))
            {
                listAproval.Add(new MenuVM
                {
                    MenuName = "Approval  Order",
                    MenuIcon = "open_with",
                    MenuUrl = "/approval-order",
                    MenuSort = 2,
                    ModuleText = "",
                });

            }
            if(user.AccessId!.Contains('4'))
            {
                listAproval.Add(new MenuVM
                {
                    MenuName = "Approval Meeting",
                    MenuIcon = "done_all",
                    MenuUrl = "/approval-meeting",
                    MenuSort = 1,
                    ModuleText = "",
                });
            }
        //     if (checkAsHeadEmployee != null)
        //    {
                
        //         listAproval.Add(new MenuVM
        //         {
        //             MenuName = "Approval  Order",
        //             MenuIcon = "open_with",
        //             MenuUrl = "/approval-order",
        //             MenuSort = 2,
        //             ModuleText = "",
        //         });
            
        //     }
            menuMap.Add(
                    new MenuVM
                    {
                        MenuName = "Approval",
                        MenuIcon = "done_all",
                        MenuUrl = "#",
                        MenuSort = 1,
                        ModuleText = "",
                        Child = listAproval
                    }
                );


            menuMap.Add(
                    new MenuVM
                {
                    MenuName = "Help",
                    MenuIcon = "help_outline",
                    MenuUrl = "#",
                    MenuSort = 1,
                    ModuleText = "",  
                    Child = listHelp
                });

            listHelp.Add(new MenuVM
                {
                    MenuName = "Help IT",
                    MenuIcon = "apps",
                    MenuUrl = "/help-it",
                    MenuSort = 2,
                    ModuleText = "",  
                });
            listHelp.Add(new MenuVM
                {
                    MenuName = "Help GS",
                    MenuIcon = "apps",
                    MenuUrl = "/help-ga",
                    MenuSort = 2,
                    ModuleText = "",  
                });
        }
        else
        {
            foreach (var item in getGroupMenu)
            {
                menuMap.Add(
                    new MenuVM
                    {

                        MenuName = item.GroupName!,
                        MenuIcon = item.GroupIcon!,
                        MenuUrl = "#",
                        MenuSort = item.MenuSort,
                        ModuleText = item.ModuleText,
                        Child = getMenuChild.Where(x => x.MenuGroupId == item.MenuGroupId).Select(x => new MenuVM
                        {
                            MenuName = x.MenuName,
                            MenuIcon = x.MenuIcon,
                            MenuUrl = x.MenuUrl,
                            MenuSort = x.MenuSort,
                            ModuleText = x.ModuleText
                        }).ToList()
                    }
                );
            }
        }

        var menuOrdered = menuMap.OrderBy(x => x.MenuSort).ToList();
        // .get side menu by level (role)

        var userVM = _mapper.Map<UserViewModel>(user);
        userVM.Level = _mapper.Map<LevelViewModel>(getLevel);
        // userVM.MenuHeaders = _mapper.Map<List<MenuHeaderLevelVM>>(getMenuHeaader);
        userVM.SideMenu = menuOrdered;

        ret.Message = "Login successful!";
        ret.Collection = userVM;
        return ret;
    }

    public async Task<ReturnalModel> RequestToken(LoginModel request, bool isWebview = false)
    {
        var cek = await CheckLogin(request);
        if (cek.Collection == null || cek.Status != ReturnalType.Success)
        {
            return cek;
        }

        var userVM = (UserViewModel)cek.Collection;
        userVM.Token = CreateToken(userVM, isWebview);
        var ret = new ReturnalModel
        {
            Message = "Login successful!",
            Collection = userVM
        };
        return ret;
    }


    public async Task<ReturnalModel> WebviewLogin(LoginWebviewModel request)
    {
        var checkUsername = await _repo.GetUserByUsernameWithFilter(request.Username);

        if (checkUsername == null || checkUsername.EmployeeId != request.Nik)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Login failed, user not found",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var mapToLogin = new LoginModel
        {
            Username = checkUsername.Username,
            Password = ""
        };

        var cek = await CheckLogin(mapToLogin, true);
        if (cek.Collection == null || cek.Status != ReturnalType.Success)
        {
            return cek;
        }

        var userVM = (UserViewModel)cek.Collection;
        userVM.Token = CreateToken(userVM, true);
        var ret = new ReturnalModel
        {
            Message = "Login successful!",
            Collection = userVM
        };
        return ret;
    }

    public string CreateToken(UserViewModel getValidUser, bool isWebview = false)
    {
        string token;
        var claim = new List<Claim> {
            new (ClaimTypes.NameIdentifier, getValidUser.Id?.ToString() ?? "InvalidUser"),
            new (ClaimTypes.Name, getValidUser.Username),
            new (ClaimTypes.Role, getValidUser.LevelId.ToString() ?? "0"),
            new (ClaimTypes.Actor, getValidUser.AccessId ?? "#"),
            new (ClaimTypes.UserData, getValidUser.Nik ?? "Nik"),
            new ("IsWebview", isWebview ? "true" : "false")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            _tokenManagement.Issuer,
            _tokenManagement.Audience,
            claim,
            expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var encodedToken = tokenHandler.WriteToken(jwtToken);

        return encodedToken;
    }

    public async Task<ReturnalModel> GetAuthUser()
    {
        var username = _httpCont?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid User",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        var getUser = await base.GetOneByField("username", username);

        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = getUser
        };
    }

    public Dictionary<string, string>? GetAllClaims()
    {
        // Ambil semua klaim dari user yang sedang login
        var claims = _httpCont?.HttpContext?.User.Claims;
        if (claims == null || !claims.Any())
        {
            return null;
        }

        // Atau simpan dalam dictionary jika ingin akses lebih mudah
        Dictionary<string, string> claimsDictionary = claims.ToDictionary(c => c.Type, c => c.Value);
        return claimsDictionary;
    }
    public async Task<UserViewModel?> GetUserJoin(LoginModel request)
    {
        var encryptPass = _Base64.Encrypt(request.Password.Trim());
        var items = await _repo.GetUserJoin(request.Username, encryptPass);

        return __mapper.Map<UserViewModel>(items);
    }

    public async Task<ReturnalModel> PantryLogin(LoginModel request)
    {
        var getUser = await GetUserJoin(request);

        if (getUser == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid User",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        getUser.SecureQr = null;
        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = getUser
        };
    }

    public async Task<ReturnalModel> DisplayLogin(LoginModel request)
    {
        var getUser = await GetUserJoin(request);

        if (getUser == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid User",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        if (getUser.LevelId != 1)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Forbidden,
                Message = "Failed login, your access is restricted",
                StatusCode = (int)HttpStatusCode.Forbidden
            };
        }

        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = getUser
        };
    }

    public async Task<ReturnalModel> UpdateUsernameAsync(UserVMUpdateUsernameFR request, long id)
    {
        var ret = new ReturnalModel();
        DateTime now = DateTime.Now;

        // Get the actual user by ID
        var existingUser = await _repo.GetById(id);

        if (existingUser == null)
        {
            ret.Message = "User not found";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        if (existingUser.Username == request.Username)
        {
            ret.Message = "Username is same with the old one";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        var existingUsername = await _repo.GetUserByUsernameWithFilter(request.Username);
        if (existingUsername != null)
        {
            ret.Message = "Username already exists";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        existingUser.Username = request.Username; // Update username
        existingUser.UpdatedAt = now;

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                await _repo.Update(existingUser);
                scope.Complete();

                _mapper.Map<UserViewModel>(existingUser);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<ReturnalModel> UpdatePassword(UserVMUpdatePasswordFR request, long id)
    {
        var ret = new ReturnalModel();
        DateTime now = DateTime.Now;
        // Get the actual user by ID
        var existingUser = await _repo.GetById(id);

        if (existingUser == null)
        {
            ret.Message = "User not found";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        if (existingUser.Password != _Base64.Encrypt(request.Password.Trim()))
        {
            ret.Message = "Old password is incorrect";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        if (request.NewPassword.Trim() != request.ConfirmationPassword.Trim())
        {
            ret.Message = "Confirmation password is not same with new password";
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        existingUser.RealPassword = request.NewPassword.Trim();//harusnnya gk disimpen di db ini, percuma amat diencrypt
        existingUser.Password = _Base64.Encrypt(request.NewPassword.Trim());
        existingUser.UpdatedAt = now;

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                await _repo.Update(existingUser);
                scope.Complete();

                _mapper.Map<UserViewModel>(existingUser);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}