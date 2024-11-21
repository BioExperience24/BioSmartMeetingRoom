using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation;

public abstract class BaseService<VM, E> : IBaseService<VM>
    where VM : BaseViewModel, new()
    where E : BaseEntity, new()
{
    protected readonly BaseRepository<E> _repository;
    public readonly IMapper _mapper;

    public BaseService(BaseRepository<E> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Get All - Mengembalikan semua data sebagai ViewModel
    public async Task<IEnumerable<VM>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        var result = _mapper.Map<List<VM>>(entities); // Menggunakan List<VM> sebagai target
        return result;
    }


    // Get By Id - Mengembalikan data ViewModel berdasarkan Id
    public async Task<VM?> GetById(string id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    // Add - Menambahkan data baru dari ViewModel
    public async Task<int> Create(VM viewModel)
    {
        viewModel.IsDeleted = 0;
        var entity = _mapper.Map<E>(viewModel);
        await _repository.AddAsync(entity);
        return await _repository.CompleteAsync();
    }

    // Update - Mengupdate data dari ViewModel
    public async Task<int> Update(VM viewModel)
    {
        var entity = _mapper.Map<E>(viewModel);
        _repository.UpdateAsync(entity);
        return await _repository.CompleteAsync();
    }

    public async Task<int?> SoftDelete(string id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity != null)
        {
            entity.IsDeleted = 1;
            _repository.UpdateAsync(entity);
            return await _repository.CompleteAsync();
        }
        else
        {
            return null;
        }

    }

    // Delete - Menghapus data berdasarkan Id
    public async Task<int?> PermanentDelete(string id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity != null)
        {
            _repository.Delete(entity);
            return await _repository.CompleteAsync();
        }
        else
        {
            return null;
        }
    }
}

