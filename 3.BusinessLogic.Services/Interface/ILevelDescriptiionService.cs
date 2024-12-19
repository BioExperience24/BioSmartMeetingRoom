using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ILevelDescriptiionService
    {
        Task<LevelDescriptiionViewModel> GetItemByLevelIdAsync(int levelId);
    }
}