using System.Drawing;
using System.Text.Json;
using System.Transactions;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomAutomationService : BaseLongService<RoomAutomationViewModel, RoomAutomation>, IRoomAutomationService
    {
        private readonly RoomAutomationRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper __mapper;

        public RoomAutomationService(RoomAutomationRepository repo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repo, mapper)
        {
            _repo = repo;
            __mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


    }

}