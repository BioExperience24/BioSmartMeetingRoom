using _5.Helpers.Consumer.EnumType;
using Newtonsoft.Json;

namespace _3.BusinessLogic.Services.Implementation;

public class IntegrationService : IIntegrationService
{
    private readonly ModuleBackendRepository _repoModuleBackend;
    public string AppUrl { get; set; }

    private readonly IConfiguration config;
    private readonly IMapper _mapper;

    public IntegrationService(IMapper mapper, ModuleBackendRepository repoModuleBackend, IConfiguration config)
    {
        _mapper = mapper;
        _repoModuleBackend = repoModuleBackend;
        this.config = config;
        AppUrl = config["App:BaseUrl"] ?? string.Empty;
    }

    public async Task<ReturnalModel> GetIntegrationData()
    {
        var ret = new ReturnalModel();
        var alarmIntegration = await _repoModuleBackend.GetAlarmIntegration();
        var integration365 = await _repoModuleBackend.GetIntegration365();

        var moduleIntAlarm = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.IntAlarm);
        var moduleIntGoogle = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.IntGoogle);
        var moduleInt365 = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Int365);

        var mapModuleIntAlarm = _mapper.Map<ModuleDetailsViewModel>(moduleIntAlarm);
        var mapModuleIntGoogle = _mapper.Map<ModuleDetailsViewModel>(moduleIntGoogle);
        var mapModuleInt365 = _mapper.Map<ModuleDetailsViewModel>(moduleInt365);

        var modules = new Dictionary<string, object>
    {
        { "alarm", mapModuleIntAlarm },
        { "google", mapModuleIntGoogle },
        { "m365", mapModuleInt365 }
    };

        var m365Devices = new M365DevicesViewModel
        {
            UrlCallback = AppUrl + config["AppExternal:UrlCallbackIntegration"],
            UrlDisM365 = config["AppExternal:UrlDisM365"],
            UrlOpenM365 = AppUrl + config["AppExternal:UrlOpenM365"],
        };

        ret.Collection = new IntegrationViewModel
        {
            AlarmIntegration = JsonConvert.SerializeObject(alarmIntegration),
            M365Integration = JsonConvert.SerializeObject(integration365),
            M365Devices = m365Devices,
            Modules = modules
        };

        return ret;
    }


}

