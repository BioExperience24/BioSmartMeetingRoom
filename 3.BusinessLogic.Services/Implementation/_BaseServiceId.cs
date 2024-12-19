using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation;

public abstract class BaseServiceId<VM, E> : IBaseServiceId<VM>
    where VM : BaseViewModelId, new()
    where E : BaseEntityId, new()
{
    protected readonly BaseRepositoryId<E> _repository;
    public readonly IMapper _mapper;

    public BaseServiceId(BaseRepositoryId<E> repository, IMapper mapper)
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
    public async Task<VM?> GetById(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<VM>(entity);
    }

    // Add - Menambahkan data baru dari ViewModel
    public async Task<int> Create(VM viewModel)
    {
        viewModel.IsDeleted = 0;
        viewModel.Id = (await _repository.GetNextID());
        var entity = _mapper.Map<E>(viewModel);
        await _repository.AddAsync(entity);
        return await _repository.CompleteAsync();
    }

    // Update - Mengupdate data dari ViewModel
    public async Task<bool> Update(VM viewModel)
    {
        var entity = _mapper.Map<E>(viewModel);
        if ( await _repository.UpdateAsync(entity)==1)
            return true;
        else 
            return false;
        // return await _repository.CompleteAsync();
    }

    public async Task<bool> SoftDelete(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity != null)
        {
            //entity.IsDeleted = 1;
            if ( await _repository.UpdateAsync(entity)==1)
                return true;
            else return false;
            // return await _repository.CompleteAsync();
        }
        else
        {
            return false;
        }

    }

    // Delete - Menghapus data berdasarkan Id
    public async Task<bool> PermanentDelete(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity != null)
        {
            return await _repository.Delete(entity);
            // return await _repository.CompleteAsync();
        }
        else
        {
            return false;
        }
    }
}

