using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public abstract class BaseLongService<VM, E>(BaseLongRepository<E> repository, IMapper mapper)
    : IBaseLongService<VM>
    where VM : BaseLongViewModel, new()
    where E : BaseLongEntity, new()
{
    protected readonly BaseLongRepository<E> _repository = repository;
    public readonly IMapper _mapper = mapper;

    // Get All - Mengembalikan semua data sebagai ViewModel
    public virtual async Task<IEnumerable<VM>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }


    // Get By Id - Mengembalikan data ViewModel berdasarkan Id
    public virtual async Task<VM?> GetById(long id)
    {
        var entity = await _repository.GetById(id);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    // Get All - Mengembalikan data sebagai ViewModel
    public virtual async Task<IEnumerable<VM>> GetListByField(string field, string value)
    {
        var entities = await _repository.GetListByField(field, value);
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }


    // Get By Id - Mengembalikan data ViewModel berdasarkan Id
    public virtual async Task<VM?> GetOneByField(string field, string value)
    {
        var entity = await _repository.GetOneByField(field, value);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    // Add - Menambahkan data baru dari ViewModel
    public virtual async Task<VM?> Create(VM viewModel)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                viewModel.Id = null; //id harus null untuk autogengerate
                viewModel.IsDeleted = 0;
                var item = _mapper.Map<E>(viewModel);

                var entity = await _repository.Create(item);

                scope.Complete();

                return entity == null ? null : _mapper.Map<VM>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    //insert BULK
    public virtual async Task<IEnumerable<VM>?> CreateBulk(IEnumerable<VM> viewModels)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var entities = _mapper.Map<IEnumerable<E>>(viewModels);

                foreach (var entity in entities)
                {
                    entity.Id = null; // Pastikan ID null untuk auto-generate
                    entity.IsDeleted = 0;
                }

                await _repository.CreateBulk(entities);

                scope.Complete();

                return _mapper.Map<IEnumerable<VM>>(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    // Update - Mengupdate data dari ViewModel
    public virtual async Task<VM?> Update(VM viewModel)
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
                _mapper.Map(viewModel, entity);

                entity.IsDeleted = 0;
                await _repository.Update(entity);

                scope.Complete();

                return _mapper.Map<VM>(entity);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public virtual async Task<IEnumerable<VM>?> UpdateBulk(IEnumerable<VM> viewModels)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var entities = new List<E>();
                var allOldmodel = await _repository.GetAllAsync();
                foreach (var viewModel in viewModels)
                {
                    var entity = allOldmodel.FirstOrDefault(x => x.Id == viewModel.Id);
                    if (entity != null)
                    {
                        _mapper.Map(viewModel, entity);
                        entity.IsDeleted = 0;
                        entities.Add(entity);
                    }//tidak update yg idnya tidak ada, skip ae
                }

                if (entities.Any())
                {
                    await _repository.UpdateBulk(entities);
                }

                scope.Complete();

                return _mapper.Map<IEnumerable<VM>>(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    public virtual async Task<VM?> SoftDelete(long id)
    {
        var entity = await _repository.GetById(id);
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
                entity.IsDeleted = 1;
                await _repository.Update(entity);

                scope.Complete();

                return _mapper.Map<VM>(entity);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    // Delete - Menghapus data berdasarkan Id
    public virtual async Task<int?> PermanentDelete(long id)
    {
        var entity = await _repository.GetById(id);
        if (entity != null)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    await _repository.Delete(entity);

                    scope.Complete();

                    return 1;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        else
        {
            return 0;
        }
    }
}

