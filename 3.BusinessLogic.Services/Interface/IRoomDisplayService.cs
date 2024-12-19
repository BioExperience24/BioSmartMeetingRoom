using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IRoomDisplayService : IBaseLongService<RoomDisplayViewModel>
    {
        Task<IEnumerable<RoomDisplayViewModel>> GetAllItemAsync();
        Task<RoomDisplayViewModel?> ChangeStatusEnabledAsync(RoomDisplayViewModel viewModel);
        Task<(string?, string?)> DoUploadAsync(IFormFile? file);
    }
}