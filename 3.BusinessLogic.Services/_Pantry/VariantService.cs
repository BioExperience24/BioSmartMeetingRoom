using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class VariantService(IMapper mapper,
    IPantryDetailMenuVariantService pantryVariantService,
    IPantryDetailMenuVariantDetailService pantryVariantDetailService,
    PantryDetailMenuVariantRepository repoVariant,
    PantryDetailMenuVariantDetailRepository repoVariantDetail
    )
    : IVariantService
{
    public async Task<IEnumerable<PantryDetailMenuVariantViewModel>> GetVariantByMenuId(long PantryDetailId)
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
        var getVariant = await pantryVariantService.GetById(id);
        var getDetail = await pantryVariantDetailService.GetListByField("variant_id", id);

        List<PantryDetailMenuVariantDetailViewModel>? item_ = [];
        if (getVariant != null)
        {
            item_ = getDetail.ToList();
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
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            );
            try
            {
                var variantId = dReq.Id;
                var entity = await repoVariant.GetByIdAsync(variantId);
                var entityDetail = await repoVariantDetail.GetListByField("variant_id", variantId);
                if (entity == null)
                {
                    return null;
                }

                entity.IsDeleted = 1;
                await repoVariant.UpdateAsync(entity);

                if (entityDetail != null && entityDetail.Count > 0)
                {
                    entityDetail.ForEach(x => x.IsDeleted = 1);
                    await repoVariantDetail.UpdateBulk(entityDetail);
                }
                res = mapper.Map<PantryDetailMenuVariantViewModel>(entity);

                scope.Complete();
            }
            catch (Exception)
            {
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
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            );
            try
            {
                uReq.IsDeleted = 0;
                uReq.multiple = uReq.rule;
                var variantId = uReq.Id;
                var entity = await repoVariant.GetByIdAsync(variantId);
                var oldDetail = await repoVariantDetail.GetListByField("variant_id", variantId);
                if (entity == null)
                {
                    return null;
                }
                mapper.Map(uReq, entity);
                await repoVariant.UpdateAsync(entity);

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

                await repoVariantDetail.CreateBulk(addNewVariantDetail);
                await repoVariantDetail.UpdateBulk(updateOldVariantDetail);

                res = mapper.Map<PantryDetailMenuVariantViewModel>(entity);

                scope.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        return res;
    }

    public async Task<IEnumerable<PantryDetailMenuVariantDetailViewModel>> GetVariantDetailByMenuId(long PantryDetailId)
    {
        using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            );
        var data = await repoVariantDetail.GetByMenuId(PantryDetailId);

        return mapper.Map<IEnumerable<PantryDetailMenuVariantDetailViewModel>>(data);
    }
}