using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingEmailTemplateService : BaseService<SettingEmailTemplateViewModel, SettingEmailTemplate>, ISettingEmailTemplateService
    {
        private readonly SettingEmailTemplateRepository _repo;
        private readonly IMapper _mapper;

        public SettingEmailTemplateService(SettingEmailTemplateRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingEmailTemplateVMResponse> GetAllSettingEmailTemplatesAsync()
        {
            var (list, err) = await _repo.GetAllSettingEmailTemplatesAsync();
            return new SettingEmailTemplateVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingEmailTemplateVMProp>>(list)
            };
        }

        public async Task<SettingEmailTemplateVMResponse> GetSettingEmailTemplateByIdAsync(long id)
        {
            SettingEmailTemplate? template = null;

            //if (Int32.TryParse(request.Id, out Int32 result))
                template = await _repo.GetSettingEmailTemplateById(id);

            if (template == null)
            {
                return null;
            }

            return _mapper.Map<SettingEmailTemplateVMResponse>(template);
        }

        public async Task<SettingEmailTemplate?> CreateSettingEmailTemplateAsync(SettingEmailTemplateCreateViewModelFR request)
        {
            SettingEmailTemplate item = _mapper.Map<SettingEmailTemplate>(request);
            item.IsDeleted = 0;
            //item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingEmailTemplateAsync(item);

            return result;
        }

        public async Task<SettingEmailTemplate?> UpdateSettingEmailTemplateAsync(SettingEmailTemplateUpdateViewModelFR request)
        {
            SettingEmailTemplate? template = null;

            //if (long.TryParse(request.Id, out long result))
            if (request.Type != null)
                template = await _repo.GetOneByField("type", request.Type);

            if (template == null)
            {
                return null;
            }

            _mapper.Map(request, template);

            //template.UpdatedAt = DateTime.Now;
            //template.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingEmailTemplateAsync(template))
            {
                return null;
            }

            return template;
        }

        public async Task<SettingEmailTemplate?> DeleteSettingEmailTemplateAsync(SettingEmailTemplateDeleteViewModelFR request)
        {
            SettingEmailTemplate? template = null;

            if (long.TryParse(request.Id, out long result))
                template = await _repo.GetSettingEmailTemplateById(result);

            if (template == null)
            {
                return null;
            }

            _mapper.Map(request, template);

            template.IsDeleted = 1;
            //template.UpdatedAt = DateTime.Now;
            // template.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingEmailTemplateAsync(template))
            {
                return null;
            }

            return template;
        }
    }
}