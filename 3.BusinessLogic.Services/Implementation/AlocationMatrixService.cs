using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace _3.BusinessLogic.Services.Implementation
{
    public class AlocationMatrixService : IAlocationMatrixService
    {
        private readonly AlocationMatrixRepository _repo;
    
        private readonly IMapper _mapper;

        public AlocationMatrixService(AlocationMatrixRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AlocationMatrixViewModel?> GetItemByIdAsync(AlocationMatrixViewModel condition)
        {
            var cond = _mapper.Map<AlocationMatrix>(condition);

            var alocationMatrix = await _repo.GetItemWithEntityAsync(cond);

            if (alocationMatrix == null)
            {
                return null;
            }

            var item = _mapper.Map<AlocationMatrixViewModel>(alocationMatrix);

            return item;
        }

        /* public async Task<AlocationMatrixViewModel?> CreateItemAsync(AlocationMatrixViewModel request)
        {
            return null;
        } */

        /* public async Task<AlocationMatrixViewModel> UpdateItemAsync(AlocationMatrixViewModel request)
        {
            return null;
        } */

    }
}