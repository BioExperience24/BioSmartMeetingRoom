
namespace _3.BusinessLogic.Services.Interface
{
    public interface IHelpItGaService : IBaseService<HelpItGaViewModel>
    {
        Task<DataTableResponse> GetDataTablesAsync(HelpItGaVMDataTable request);
        Task<ReturnalModel> ChangeStatusAsync(HelpItGaVMChangeStatus request);
        Task<ReturnalModel> SubmitRequestAsync(HelpItGaVMRequest request);
        Task<ReturnalModel> ListRequestAsync(HelpItGaVMFilterList request);
    }
}