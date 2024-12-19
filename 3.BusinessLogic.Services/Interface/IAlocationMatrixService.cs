

namespace _3.BusinessLogic.Services.Interface
{
    public interface IAlocationMatrixService
    {
        Task<AlocationMatrixViewModel?> GetItemByIdAsync(AlocationMatrixViewModel condition);
        // Task<AlocationMatrixViewModel?> CreateItemAsync(AlocationMatrixViewModel request);
        // Task<AlocationMatrixViewModel> UpdateItemAsync(AlocationMatrixViewModel request);
    }
}