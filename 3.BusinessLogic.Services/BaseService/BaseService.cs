using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public abstract class BaseService<VM, E>(BaseRepository<E> repository, IMapper mapper)
    : IBaseService<VM>
    where VM : BaseViewModel, new()
    where E : BaseEntity, new()
{
    protected readonly BaseRepository<E> _repository = repository;
    public readonly IMapper _mapper = mapper;

    // Get All - Mengembalikan semua data sebagai ViewModel
    public virtual async Task<IEnumerable<VM>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }


    // Get By Id - Mengembalikan data ViewModel berdasarkan Id
    public virtual async Task<VM?> GetById(string id)
    {
        var entity = await _repository.GetByIdAsync(id);
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
    public virtual async Task<VM?> Create(VM viewModel, bool isAInc = true)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                viewModel.IsDeleted = 0;
                if (string.IsNullOrEmpty(viewModel.Id) && isAInc)
                {
                    viewModel.Id = (await _repository.GetNextID()).ToString();
                }

                var entity = _mapper.Map<E>(viewModel);
                entity = await _repository.AddAsync(entity);

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
                    //entity.Id = null; // Pastikan ID sudah terisi sebelum ke sini
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
        var entity = await _repository.GetByIdAsync(viewModel.Id!);

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
                await _repository.UpdateAsync(entity);

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


    public virtual async Task<VM?> SoftDelete(string id)
    {
        var entity = await _repository.GetByIdAsync(id);

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
                await _repository.UpdateAsync(entity);

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
    public virtual async Task<int?> PermanentDelete(string id)
    {
        var entity = await _repository.GetByIdAsync(id);
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

