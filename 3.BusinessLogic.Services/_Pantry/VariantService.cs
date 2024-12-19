using Microsoft.Extensions.DependencyInjection;

namespace _3.BusinessLogic.Services.Implementation;

public class VariantService(IMapper mapper, IPantryDetailMenuVariantService pantryVariantService,
    IPantryDetailMenuVariantDetailService pantryVariantDetailService, IServiceProvider sp)
    : IVariantService
{
    public async Task<IEnumerable<PantryDetailMenuVariantViewModel>> GetVariantByPatryDetailId(long PantryDetailId)
    {
        var data = await pantryVariantService.GetListByField("menu_id", PantryDetailId.ToString());
        return data;
    }


    public async Task<PantryDetailMenuVariantViewModel?> CreateMenuAndVariant(PantryDetailMenuVariantViewModel request)
    {
        request.Id = Guid.NewGuid().ToString();
        request.multiple = request.rule;
        var result = await pantryVariantService.Create(request);

        if (request.variant != null && result != null)
        {
            List<PantryDetailMenuVariantDetailViewModel> item_detail = [];
            foreach (var item in request.variant)
            {
                item.variant_id = result.Id;
                item_detail.Add(item);
            }
            await pantryVariantDetailService.CreateBulk(item_detail);
        }

        return result;
    }

    public async Task<PantryVariantDataAndDetail?> GetVariantId(string id)
    {
        var getVariantT = pantryVariantService.GetById(id);
        var getDetailT = pantryVariantDetailService.GetListByField("variant_id", id);

        var getVariant = await getVariantT;
        List<PantryDetailMenuVariantDetailViewModel>? item_ = [];
        if (getVariant != null)
        {
            item_ = (await getDetailT).ToList();
        }
        PantryVariantDataAndDetail result = new()
        {
            data = getVariant,
            detail = item_
        };

        return result;
    }

    public async Task<PantryDetailMenuVariantViewModel?> DeleteVariantAndDetails(PantryDetailMenuVariantViewModel dReq)
    {
        PantryDetailMenuVariantViewModel? res = null;
        if (dReq?.Id != null)
        {
            using var dalSession = sp.GetRequiredService<DalSession>();
            UnitOfWork uow = dalSession.UnitOfWork;
            uow.BeginTransaction();
            PantryDetailMenuVariantRepository repo = new(uow);
            PantryDetailMenuVariantDetailRepository repo2 = new(uow);
            try
            {
                var variantId = dReq.Id;
                var entity = await repo.GetById(variantId, true);
                var entityDetail = await repo2.GetListByField("variant_id", variantId, true);
                if (entity == null)
                {
                    return null;
                }

                entity.IsDeleted = 1;
                await repo.Update(entity);

                if (entityDetail != null && entityDetail.Count > 0)
                {
                    entityDetail.ForEach(x => x.IsDeleted = 1);
                    await repo2.UpdateBulk(entityDetail);
                }
                res = mapper.Map<PantryDetailMenuVariantViewModel>(entity);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        return res;
    }

    public async Task<PantryDetailMenuVariantViewModel?> UpdateVariantAndDetail(PantryDetailMenuVariantViewModel uReq)
    {
        PantryDetailMenuVariantViewModel? res = null;
        if (uReq?.Id != null)
        {
            using var dalSession = sp.GetRequiredService<DalSession>();
            UnitOfWork uow = dalSession.UnitOfWork;
            uow.BeginTransaction();
            PantryDetailMenuVariantRepository repo = new(uow);
            PantryDetailMenuVariantDetailRepository repo2 = new(uow);
            try
            {
                uReq.IsDeleted = 0;
                uReq.multiple = uReq.rule;
                var variantId = uReq.Id;
                var entity = await repo.GetById(variantId, true);
                var oldDetail = await repo2.GetListByField("variant_id", variantId, true);
                if (entity == null)
                {
                    return null;
                }
                mapper.Map(uReq, entity);
                await repo.Update(entity);

                List<PantryDetailMenuVariantDetail> addNewVariantDetail = [];
                List<PantryDetailMenuVariantDetail> updateOldVariantDetail = [];
                List<PantryDetailMenuVariantDetail> newDetail = [];
                mapper.Map(uReq.variant, newDetail);
                foreach (var item in newDetail)
                {
                    var checkOld = oldDetail.FirstOrDefault(x => x.Id == item.Id);
                    if (checkOld == null)
                    {
                        item.IsDeleted = 0;
                        item.VariantId = variantId;
                        addNewVariantDetail.Add(item);
                    }
                    else
                    {
                        checkOld = mapper.Map<PantryDetailMenuVariantDetail>(item);
                        updateOldVariantDetail.Add(checkOld);
                    }
                }

                await repo2.CreateBulk(addNewVariantDetail);
                await repo2.UpdateBulk(updateOldVariantDetail);

                res = mapper.Map<PantryDetailMenuVariantViewModel>(entity);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        return res;
    }
}