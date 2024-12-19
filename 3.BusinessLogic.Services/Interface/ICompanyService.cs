using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface ICompanyService : IBaseService<CompanyViewModel>
{
    Task<CompanyViewModel?> GetOneItemAsync(bool withImageBase64 = true);
    Task<ReturnalModel> UploadMediaAsync(CompanyVMMediaFR request);
}
