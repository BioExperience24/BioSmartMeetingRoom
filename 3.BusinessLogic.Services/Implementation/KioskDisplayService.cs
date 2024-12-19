using System.Transactions;
using _6.Repositories.Repository;

namespace _3.BusinessLogic.Services.Implementation
{
    public class KioskDisplayService
        : BaseLongService<KioskDisplayViewModel, KioskDisplay>, IKioskDisplayService
    {
        public KioskDisplayService(IMapper mapper, KioskDisplayRepository repo) 
            : base (repo, mapper)
        { }

        public override async Task<KioskDisplayViewModel?> Create(KioskDisplayViewModel viewModel)
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

                    var item = _mapper.Map<KioskDisplay>(viewModel);

                    item.DisplaySerial = _Random.Numeric(6, true).ToString();
                    item.UpdatedAt = now;
                    item.IsDeleted = 0;
                    item.IsLogged = 0;

                    var entity = await _repository.Create(item);

                    scope.Complete();

                    return entity == null ? null : _mapper.Map<KioskDisplayViewModel>(entity);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public override async Task<KioskDisplayViewModel?> Update(KioskDisplayViewModel viewModel)
        {
            var entity = await _repository.GetById(viewModel.Id.GetValueOrDefault());
            if (entity == null)
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

                    entity.DisplayName = viewModel.DisplayName;
                    entity.IsDeleted = 0;
                    entity.UpdatedAt = now;

                    await _repository.Update(entity);

                    scope.Complete();

                    return _mapper.Map<KioskDisplayViewModel>(entity);

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}