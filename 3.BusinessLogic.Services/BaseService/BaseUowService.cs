using Microsoft.Extensions.DependencyInjection;

namespace _3.BusinessLogic.Services.Implementation;

public abstract class BaseUowService<VM, E>(IServiceProvider sp, IMapper mapper)
    : IBaseService<VM>
    where VM : BaseViewModel, new()
    where E : BaseEntity, new()
{
    public readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get All - Mengembalikan semua data sebagai ViewModel
    /// </summary>
    /// <returns>IEnumerable ViewModel</returns>
    public virtual async Task<IEnumerable<VM>> GetAll()
    {
        using var dalSession = sp.GetRequiredService<DalSession>();//C# 8.0 udh otomatis dispose klo keluar scope
        UnitOfWork uow = dalSession.UnitOfWork;
        BaseUowRepository<E> repo = new(uow);
        var entities = await repo.GetAll();
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }

    /// <summary>
    /// Get By Id - Mengembalikan data ViewModel berdasarkan Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<VM?> GetById(string id)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        BaseUowRepository<E> repo = new(uow);
        var entity = await repo.GetById(id);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    /// <summary>
    /// Get List Object by field
    /// </summary>
    /// <param name="field">table Field, sesuaikan dengan property di DB</param>
    /// <param name="value">value field</param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<VM>> GetListByField(string field, string value)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        BaseUowRepository<E> repo = new(uow);
        var entities = await repo.GetListByField(field, value);
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }


    /// <summary>
    /// Get ONE object
    /// </summary>
    /// <param name="field">table Field, sesuaikan dengan property di DB</param>
    /// <param name="value">value field</param>
    /// <returns></returns>
    public virtual async Task<VM?> GetOneByField(string field, string value)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        BaseUowRepository<E> repo = new(uow);
        var entity = await repo.GetOneByField(field, value);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    /// <summary>
    /// Create baru
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="isAInc"></param>
    /// <returns></returns>
    public virtual async Task<VM?> Create(VM viewModel, bool isAInc = true)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);
        try
        {
            viewModel.IsDeleted = 0;
            if (string.IsNullOrEmpty(viewModel.Id) && isAInc)
            {
                viewModel.Id = (await repo.GetNextID()).ToString();
            }
            var entity = _mapper.Map<E>(viewModel);
            entity = await repo.Create(entity);

            uow.Commit();

            return entity == null ? null : _mapper.Map<VM>(entity);
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }

    /// <summary>
    /// create BULK object
    /// </summary>
    /// <param name="viewModels"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<VM>?> CreateBulk(IEnumerable<VM> viewModels)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);
        try
        {
            var entities = _mapper.Map<IEnumerable<E>>(viewModels);
            foreach (var entity in entities)
            {
                //entity.Id = null; // Pastikan ID sudah terisi sebelum ke sini
                entity.IsDeleted = 0;
            }

            await repo.CreateBulk(entities);

            uow.Commit();
            return _mapper.Map<IEnumerable<VM>>(entities);
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }

    /// <summary>
    /// update 1 object
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public virtual async Task<VM?> Update(VM viewModel)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);
        try
        {
            var entity = await repo.GetById(viewModel.Id!, true);
            if (entity == null)
            {
                return null;
            }
            _mapper.Map(viewModel, entity);

            entity.IsDeleted = 0;
            await repo.Update(entity);

            uow.Commit();

            return _mapper.Map<VM>(entity);
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }

    /// <summary>
    /// update BULK sekaligus
    /// </summary>
    /// <param name="viewModels"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<VM>?> UpdateBulk(IEnumerable<VM> viewModels)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);
        try
        {
            var entities = new List<E>();
            var allOldmodel = await repo.GetAll(true);
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

            if (entities.Count != 0)
            {
                await repo.UpdateBulk(entities);
            }

            uow.Commit();
            return _mapper.Map<IEnumerable<VM>>(entities);
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }

    /// <summary>
    /// is_deleted = 1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<VM?> SoftDelete(string id)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);

        try
        {
            var entity = await repo.GetById(id, true);

            if (entity == null)
            {
                return null;
            }
            entity.IsDeleted = 1;
            await repo.Update(entity);

            uow.Commit();

            return _mapper.Map<VM>(entity);
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }

    /// <summary>
    /// delete permanent from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<int?> PermanentDelete(string id)
    {
        using var dalSession = sp.GetRequiredService<DalSession>();
        UnitOfWork uow = dalSession.UnitOfWork;
        uow.BeginTransaction();
        BaseUowRepository<E> repo = new(uow);

        try
        {
            var entity = await repo.GetById(id, true);
            if (entity == null)
            {
                return null;
            }

            await repo.Delete(entity);
            uow.Commit();
            return 1;
        }
        catch (Exception)
        {
            uow.Rollback();
            throw;
        }
    }
}

