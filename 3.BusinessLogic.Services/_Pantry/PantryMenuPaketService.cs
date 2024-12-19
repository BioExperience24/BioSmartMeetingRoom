using System.Net.Mail;
using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class PantryMenuPaketService(PantryMenuPaketRepository repo, IMapper mapper)
    : BaseService<PantryMenuPaketViewModel, PantryMenuPaket>(repo, mapper), IPantryMenuPaketService
{
    public override async Task<IEnumerable<PantryMenuPaketViewModel>> GetAll()
    {
        var entity = await repo.GetPackageWithPantry();

        var vm = _mapper.Map<IEnumerable<PantryMenuPaketViewModel>>(entity);
        foreach (var item in vm)
        {
            item.pantry_name = item.pantry?.name;
        }
        return vm.OrderBy(x => x.name);
    }

    public async Task<PantryPackageDataAndDetail> GetPackageAndDetail(string id)
    {
        var result = new PantryPackageDataAndDetail();
        var entity = await repo.GetPackageWithPantry(id);
        result.data = mapper.Map<PantryMenuPaketViewModel>(entity);
        var pantryDetail = await repo.GetPantryDetailByPackageId(id);
        result.detail = mapper.Map<List<PantryDetailViewModel>>(pantryDetail);
        return result;
    }

    public override async Task<PantryMenuPaketViewModel?> Create(PantryMenuPaketViewModel viewModel, bool isAInc = true)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
        try
        {
            viewModel.IsDeleted = 0;
            viewModel.Id = Guid.NewGuid().ToString();
            viewModel.created_at = DateTime.Now;

            var entity = _mapper.Map<PantryMenuPaket>(viewModel);
            entity = await repo.AddAsync(entity);
            List<PantryMenuPaketD> listPackageD = [];
            if (viewModel.menu != null)
            {
                foreach (var item in viewModel.menu)
                {
                    listPackageD.Add(new()
                    {
                        MenuId = item.Id ?? 0,
                        PackageId = viewModel.Id,
                        IsDeleted = 0
                    });
                }
            }
            await repo.CreateBulkPackageD(listPackageD);

            scope.Complete();

            return entity == null ? null : _mapper.Map<PantryMenuPaketViewModel>(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PantryMenuPaketViewModel?> UpdatePackage(PantryMenuPaketViewModel viewModel)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
        try
        {
            var entity = await repo.GetByIdAsync(viewModel.Id!);
            if (entity == null)
            {
                return null;
            }
            _mapper.Map(viewModel, entity);
            entity.IsDeleted = 0;
            await _repository.UpdateAsync(entity);

            await repo.SoftDeleteBulkByPackageId(viewModel.Id!);

            //delete and re ADD all
            List<PantryMenuPaketD> addNewDetail = [];
            var newDetail = viewModel.menu ?? [];
            foreach (var item in newDetail)
            {
                addNewDetail.Add(new()
                {
                    MenuId = item.Id ?? 0,
                    PackageId = viewModel.Id!,
                    IsDeleted = 0
                });
            }
            await repo.CreateBulkPackageD(addNewDetail);
            scope.Complete();

            return _mapper.Map<PantryMenuPaketViewModel>(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PantryMenuPaketViewModel?> DeletePackage(string? id)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        );
        try
        {
            var entity = await repo.GetByIdAsync(id!);
            if (entity == null)
            {
                return null;
            }
            entity.IsDeleted = 1;
            await _repository.UpdateAsync(entity);

            await repo.SoftDeleteBulkByPackageId(id!);
            scope.Complete();

            return _mapper.Map<PantryMenuPaketViewModel>(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }
}