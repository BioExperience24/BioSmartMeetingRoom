using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingInvoiceTextService : BaseService<SettingInvoiceTextViewModel, SettingInvoiceText>, ISettingInvoiceTextService
    {
        private readonly SettingInvoiceTextRepository _repo;
        private readonly IMapper _mapper;

        public SettingInvoiceTextService(SettingInvoiceTextRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingInvoiceTextVMResponse> GetAllSettingInvoiceTextsAsync()
        {
            var (list, err) = await _repo.GetAllSettingInvoiceTextsAsync();
            return new SettingInvoiceTextVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingInvoiceTextVMProp>>(list)
            };
        }

        public async Task<SettingInvoiceTextVMResponse> GetSettingInvoiceTextByIdAsync(SettingInvoiceTextUpdateViewModelFR request)
        {
            SettingInvoiceText? text = await _repo.GetSettingInvoiceTextById(request.Id);

            if (text == null)
            {
                return null;
            }

            return _mapper.Map<SettingInvoiceTextVMResponse>(text);
        }

        public async Task<SettingInvoiceText?> CreateSettingInvoiceTextAsync(SettingInvoiceTextCreateViewModelFR request)
        {
            SettingInvoiceText item = _mapper.Map<SettingInvoiceText>(request);
            item.IsDeleted = 0;
            item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingInvoiceTextAsync(item);

            return result;
        }

        public async Task<SettingInvoiceText?> UpdateSettingInvoiceTextAsync(SettingInvoiceTextUpdateViewModelFR request)
        {
            SettingInvoiceText? text = await _repo.GetSettingInvoiceTextById(request.Id);

            if (text == null)
            {
                return null;
            }

            _mapper.Map(request, text);

            text.UpdatedAt = DateTime.Now;
            // text.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingInvoiceTextAsync(text))
            {
                return null;
            }

            return text;
        }

        public async Task<SettingInvoiceText?> DeleteSettingInvoiceTextAsync(SettingInvoiceTextDeleteViewModelFR request)
        {
            SettingInvoiceText? text = await _repo.GetSettingInvoiceTextById(request.Id);

            if (text == null)
            {
                return null;
            }

            _mapper.Map(request, text);

            text.IsDeleted = 1;
            text.UpdatedAt = DateTime.Now;
            // text.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingInvoiceTextAsync(text))
            {
                return null;
            }

            return text;
        }
    }
}