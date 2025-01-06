using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class UserService : BaseLongService<UserViewModel, User>, IUserService
{
    private readonly UserRepository _repo;

    private readonly IMapper __mapper;

    private readonly EmployeeRepository _employeeRepo;

    public UserService(UserRepository repo, IMapper mapper, EmployeeRepository employeeRepo) : base(repo, mapper)
    {
        _repo = repo;
        __mapper = mapper;
        _employeeRepo = employeeRepo;
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
        var items = await _repo.GetItemsAsync();

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

    public async Task<ReturnalModel> CheckLogin(LoginModel request)
    {
        ReturnalModel ret = new();
        var encryptPass = _Base64.Encrypt(request.Password.Trim());
        var user = await _repo.GetUserByUsernamePassword(request.Username, encryptPass);

        if (user == null)
        {
            ret.Title = ReturnalType.Failed;
            ret.Status = ReturnalType.Failed;
            ret.Message = "Invalid username or password.";
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        ret.Message = "Login successful!";
        ret.Collection = _mapper.Map<UserViewModel>(user);
        return ret;
    }
}