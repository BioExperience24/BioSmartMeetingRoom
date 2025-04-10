using System.Reflection;
using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.EmailService;
using _3.BusinessLogic.Services.Extension;
using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using _4.Helpers.Consumer.Report;
using _5.Helpers.Consumer;
using _5.Helpers.Consumer._Encryption._Secure;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.Custom;
using _6.Repositories.Repository;
using _7.Entities.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Global.Extensions;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Business Logic
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomService(this IServiceCollection services)
    {
        /** == Master Populate == **/
        services.AddScoped<IBeaconFloorService, BeaconFloorService>();
        services.AddScoped<IAccessControlService, AccessControlService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDepartmentService, DepartementService>();
        services.AddScoped<IDivisiService, DivisiService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IFacilityService, FacilityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAlocationService, AlocationService>();
        services.AddScoped<IAlocationTypeService, AlocationTypeService>();
        services.AddScoped<IUserConfigService, UserConfigService>();
        services.AddScoped<IPantryService, PantryService>();
        services.AddScoped<ILevelService, LevelService>();
        services.AddScoped<ILevelDescriptiionService, LevelDescriptiionService>();
        services.AddScoped<IBuildingService, BuildingService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRoomAutomationService, RoomAutomationService>();
        services.AddScoped<MethodHelperService>();
        services.AddScoped<IPantryDetailService, PantryDetailService>();
        services.AddScoped<IPantryDetailMenuVariantService, PantryDetailMenuVariantService>();
        services.AddScoped<IPantryDetailMenuVariantDetailService, PantryDetailMenuVariantDetailService>();
        services.AddScoped<IPantryMenuPaketService, PantryMenuPaketService>();
        services.AddScoped<IPantryTransaksiService, PantryTransaksiService>();
        services.AddScoped<IPantrySatuanService, PantrySatuanService>();
        services.AddScoped<IVariantService, VariantService>();
        services.AddTransient<IAttachmentListService, AttachmentListService>();
        services.AddScoped<IBuildingFloorService, BuildingFloorService>();
        services.AddScoped<ISettingEmailTemplateService, SettingEmailTemplateService>();
        services.AddScoped<ISettingInvoiceConfigService, SettingInvoiceConfigService>();
        services.AddScoped<ISettingInvoiceTextService, SettingInvoiceTextService>();
        services.AddScoped<ISettingLogConfigService, SettingLogConfigService>();
        services.AddScoped<ISettingPantryConfigService, SettingPantryConfigService>();
        services.AddScoped<ISettingSmtpService, SettingSmtpService>();
        services.AddScoped<IVariableTimeDurationService, VariableTimeDurationService>();
        services.AddScoped<ISettingRuleBookingService, SettingRuleBookingService>();
        services.AddScoped<IModuleBackendService, ModuleBackendService>();
        services.AddScoped<IAccessControllerTypeService, AccessControllerTypeService>();
        services.AddScoped<IAccessChannelService, AccessChannelService>();
        services.AddScoped<IAccessIntegratedService, AccessIntegratedService>();
        services.AddScoped<ILicenseListService, LicenseListService>();
        services.AddScoped<IRoomDisplayService, RoomDisplayService>();
        services.AddScoped<IKioskDisplayService, KioskDisplayService>();
        services.AddScoped<ILicenseSettingService, LicenseSettingService>();
        services.AddScoped<IRoomForUsageService, RoomForUsageService>();
        services.AddScoped<IBookingProcessService, BookingProcessService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAPIMainPantryService, APIMainPantryService>();
        services.AddScoped<IAPIMainDisplayService, APIMainDisplayService>();
        services.AddScoped<IWorkerAPIService, WorkerAPIService>();
        services.AddScoped<IIntegrationService, IntegrationService>();
        services.AddScoped<IExportReport, ExportReport>();
        services.AddScoped<IS3Service, S3Service>();
        services.AddScoped<IHelpItGaService, HelpItGaService>();

        // Email service
        services.AddScoped<IEmailService, EmailService>();
    }
    public static void AddCustomRepository(this IServiceCollection services)
    {
        services.AddScoped<AccessControlRepository>();
        services.AddScoped<BookingRepository>();
        services.AddScoped<BeaconFloorRepository>();
        services.AddScoped<CompanyRepository>();
        services.AddScoped<DepartmentRepository>();
        services.AddScoped<DivisiRepository>();
        services.AddScoped<EmployeeRepository>();
        services.AddScoped<FacilityRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<AlocationRepository>();
        services.AddScoped<AlocationTypeRepository>();
        services.AddScoped<UserConfigRepository>();
        services.AddScoped<AlocationMatrixRepository>();
        services.AddScoped<PantryRepository>();
        services.AddScoped<LevelRepository>();
        services.AddScoped<LevelDescriptionRepository>();
        services.AddScoped<BuildingRepository>();
        services.AddScoped<PantryDetailRepository>();
        services.AddScoped<RoomAutomationRepository>();
        services.AddScoped<RoomRepository>();
        services.AddScoped<ModuleBackendRepository>();
        services.AddScoped<PantryDetailMenuVariantRepository>();
        services.AddScoped<PantryDetailMenuVariantDetailRepository>();
        services.AddScoped<PantryMenuPaketRepository>();
        services.AddScoped<PantryTransaksiRepository>();
        services.AddScoped<PantrySatuanRepository>();
        services.AddScoped<BuildingFloorRepository>();
        services.AddScoped<SettingInvoiceConfigRepository>();
        services.AddScoped<SettingInvoiceTextRepository>();
        services.AddScoped<SettingLogConfigRepository>();
        services.AddScoped<SettingEmailTemplateRepository>();
        services.AddScoped<SettingPantryConfigRepository>();
        services.AddScoped<VariableTimeDurationRepository>();
        services.AddScoped<SettingPantryConfigRepository>();
        services.AddScoped<SettingSmtpRepository>();
        services.AddScoped<SettingRuleBookingRepository>();
        services.AddScoped<LicenseListRepository>();
        services.AddScoped<ModuleBackendRepository>();
        services.AddScoped<AccessControllerTypeRepository>();
        services.AddScoped<AccessChannelRepository>();
        services.AddScoped<AccessControllerFalcoRepository>();
        services.AddScoped<AccessIntegratedRepository>();
        services.AddScoped<RoomDisplayRepository>();
        services.AddScoped<KioskDisplayRepository>();
        services.AddScoped<LicenseSettingRepository>();
        services.AddScoped<RoomForUsageRepository>();
        services.AddScoped<BookingInvitationRepository>();
        services.AddScoped<PantryTransaksiDRepository>();
        services.AddScoped<TimeScheduleRepository>();
        services.AddScoped<PantryTransaksiStatusRepository>();
        services.AddScoped<PantryTransaksiDetailRepository>();
        services.AddScoped<BookingInvoiceRepository>();
        services.AddScoped<SendingEmailRepository>();
        services.AddScoped<SendingNotifRepository>();
        services.AddScoped<PantryTransaskiDRepository>();
        services.AddScoped<RoomDisplayInformationRepository>();
        services.AddScoped<HelpItGaRepository>();
    }

    public static IServiceCollection AddHelperService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISecureService>(new SecureService(configuration));
        return services;
    }
    
    /// <summary>
    /// Auto Mapping Vice Versa and Map List
    /// </summary>
    public class AutoMapperProfilling : Profile
    {
        public AutoMapperProfilling()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<PantryViewModel, Pantry>().ReverseMap();

            CreateMap<AlocationTypeViewModel, AlocationType>().ReverseMap();
            CreateMap<AlocationTypeVMUpdateFR, AlocationType>().ReverseMap().IgnoreNullProperty();
            CreateMap<AlocationTypeVMDefaultFR, AlocationType>().ReverseMap().IgnoreNullProperty();

            CreateMap<AlocationViewModel, Alocation>().ReverseMap().IgnoreNullProperty();
            CreateMap<object, AlocationViewModel>().MapNestedProperties();
            CreateMap<AlocationVMDefaultFR, Alocation>().ReverseMap().IgnoreNullProperty();
            CreateMap<AlocationVMCreateFR, Alocation>().ReverseMap().IgnoreNullProperty();
            CreateMap<AlocationVMUpdateFR, Alocation>().ReverseMap().IgnoreNullProperty();

            CreateMap<object, EmployeeViewModel>().MapNestedProperties();
            CreateMap<object, EmployeeVMResp>().MapNestedProperties();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<EmployeeVMDefaultFR, Employee>().ReverseMap().IgnoreNullProperty();
            CreateMap<EmployeeVMCreateFR, Employee>().ReverseMap().IgnoreNullProperty();
            CreateMap<EmployeeVMUpdateVipFR, Employee>().ReverseMap().IgnoreNullProperty();
            CreateMap<EmployeeVMDeleteFR, Employee>().ReverseMap().IgnoreNullProperty();
            CreateMap<EmployeeReportOrganizerUsage, EmployeeVMOrganizerCollection>().MapNestedProperties();
            CreateMap<EmployeeReportAttendees, EmployeeVMAttendeesCollection>().MapNestedProperties();

            CreateMap<Level, LevelViewModel>().ReverseMap().IgnoreNullProperty();
            CreateMap<LevelVMUpdateFR, LevelViewModel>().ReverseMap().IgnoreNullProperty();

            CreateMap<LevelDescriptiion, LevelDescriptiionViewModel>()
                .ReverseMap().IgnoreNullProperty();
            CreateMap<object, LevelDescriptiionViewModel>().MapNestedProperties();

            CreateMap<User, UserViewModel>().ReverseMap().IgnoreNullProperty();
            CreateMap<object, UserViewModel>().MapNestedProperties();
            CreateMap<User, UserVMDefaultFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<User, UserVMCreateFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<User, UserVMUpdateFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<User, UserVMUpdateFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<User, UserVMDisableFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<User, UserVMDeleteFR>().ReverseMap().IgnoreNullProperty();

            CreateMap<UserConfigViewModel, UserConfig>().ReverseMap().IgnoreNullProperty();
            CreateMap<AlocationMatrixViewModel, AlocationMatrix>().ReverseMap().IgnoreNullProperty();

            CreateMap<DivisiRepository, Divisi>().ReverseMap();
            CreateMap<FacilityRepository, Facility>().ReverseMap();
            CreateMap<FacilityVMProp, Facility>().ReverseMap();
            CreateMap<Facility, FacilityCreateViewModelFR>().ReverseMap();
            CreateMap<FacilityDeleteViewModelFR, Facility>().ReverseMap().IgnoreNullProperty();
            CreateMap<FacilityUpdateViewModelFR, Facility>().ReverseMap().IgnoreNullProperty();
            CreateMap<FacilityViewModel, Facility>().ReverseMap();

            CreateMap<Building, BuildingViewModel>().ReverseMap().IgnoreNullProperty();
            CreateMap<object, BuildingViewModel>().MapNestedProperties();
            CreateMap<Building, BuildingVMDefaultFR>().ReverseMap().IgnoreNullProperty();
            CreateMap<Building, BuildingVMDeleteFR>().ReverseMap().IgnoreNullProperty();

            CreateMap<PantryViewModel, Pantry>().ReverseMap();
            CreateMap<PantryDetailViewModel, PantryDetail>().ReverseMap();
            CreateMap<PantryDetailSelect, PantryDetailVMMenus>().ReverseMap();
            CreateMap<PantryTransaksiViewModel, PantryTransaksi>().ReverseMap();
            CreateMap<PantryTransaksiVMApporval, PantryTransaksiSelect>().ReverseMap();
            CreateMap<PantryTransaksiVMOrderApproval, PantryTransaksiOrderApproval>().ReverseMap();
            CreateMap<PantrySatuanViewModel, PantrySatuan>().ReverseMap();
            CreateMap<PantryMenuPaketViewModel, PantryMenuPaket>()
                .ForMember(dest => dest.PantryId, opt => opt.MapFrom(src => src.pantry_id))
                .ReverseMap();

            CreateMap<PantryDetailMenuVariantViewModel, PantryDetailMenuVariant>().ReverseMap().IgnoreNullProperty();
            CreateMap<PantryDetailMenuVariantDetailViewModel, PantryDetailMenuVariantDetail>().ReverseMap().IgnoreNullProperty();

            CreateMap<CompanyViewModel, Company>().ReverseMap();
            CreateMap<RoomData, RoomDataViewModel>().ReverseMap();
            CreateMap<RoomDetail, RoomDetailViewModel>().ReverseMap();
            CreateMap<RoomAutomation, RoomAutomationViewModel>().ReverseMap();
            CreateMap<BuildingDataDto, RoomBuildingViewModel>();
            CreateMap<BuildingFloor, BuildingFloorViewModel>().ReverseMap();

            CreateMap<BeaconFloor, BeaconFloorViewModel>().ReverseMap();
            CreateMap<BeaconFloorVMDefaultFR, BeaconFloorViewModel>().ReverseMap();
            CreateMap<BeaconFloorVMDetailFR, BeaconFloorViewModel>().ReverseMap();
            CreateMap<BeaconFloorVMUpdateFR, BeaconFloorViewModel>().ReverseMap();
            CreateMap<object, BeaconFloorViewModel>().MapNestedProperties();

            CreateMap<PantryDetailMenuVariantDetailViewModel, PantryDetailMenuVariantDetail>().ReverseMap();
            CreateMap<PantryDetailMenuVariantDetailRepository, PantryDetailMenuVariantDetail>().ReverseMap();

            CreateMap<SettingInvoiceConfigViewModel, SettingInvoiceConfig>().ReverseMap();
            CreateMap<SettingInvoiceConfigRepository, SettingInvoiceConfig>().ReverseMap();
            CreateMap<SettingInvoiceConfigVMProp, SettingInvoiceConfig>().ReverseMap();
            CreateMap<SettingInvoiceConfig, SettingInvoiceConfigCreateViewModelFR>().ReverseMap();
            CreateMap<SettingInvoiceConfigUpdateViewModelFR, SettingInvoiceConfig>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingInvoiceConfigUpdateViewModelFR, SettingInvoiceConfig>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingInvoiceTextViewModel, SettingInvoiceText>().ReverseMap();
            CreateMap<SettingInvoiceTextRepository, SettingInvoiceText>().ReverseMap();
            CreateMap<SettingInvoiceTextVMProp, SettingInvoiceText>().ReverseMap();
            CreateMap<SettingInvoiceText, SettingInvoiceTextCreateViewModelFR>().ReverseMap();
            CreateMap<SettingInvoiceTextUpdateViewModelFR, SettingInvoiceText>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingInvoiceTextUpdateViewModelFR, SettingInvoiceText>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingLogConfigViewModel, SettingLogConfig>().ReverseMap();
            CreateMap<SettingLogConfigRepository, SettingLogConfig>().ReverseMap();
            CreateMap<SettingLogConfigVMProp, SettingLogConfig>().ReverseMap();
            CreateMap<SettingLogConfig, SettingLogConfigCreateViewModelFR>().ReverseMap();
            CreateMap<SettingLogConfigUpdateViewModelFR, SettingLogConfig>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingLogConfigUpdateViewModelFR, SettingLogConfig>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingEmailTemplateViewModel, SettingEmailTemplate>().ReverseMap();
            CreateMap<SettingEmailTemplateRepository, SettingEmailTemplate>().ReverseMap();
            CreateMap<SettingEmailTemplateVMProp, SettingEmailTemplate>().ReverseMap();
            CreateMap<SettingEmailTemplate, SettingEmailTemplateCreateViewModelFR>().ReverseMap();
            CreateMap<SettingEmailTemplateUpdateViewModelFR, SettingEmailTemplate>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingEmailTemplateUpdateViewModelFR, SettingEmailTemplate>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingPantryConfigViewModel, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfigRepository, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfigVMProp, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfig, SettingPantryConfigCreateViewModelFR>().ReverseMap();
            CreateMap<SettingPantryConfigUpdateViewModelFR, SettingPantryConfig>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingPantryConfigUpdateViewModelFR, SettingPantryConfig>().ReverseMap().IgnoreNullProperty();

            CreateMap<VariableTimeDurationViewModel, VariableTimeDuration>().ReverseMap();
            CreateMap<VariableTimeDurationRepository, VariableTimeDuration>().ReverseMap();
            CreateMap<VariableTimeDurationVMProp, VariableTimeDuration>().ReverseMap();
            CreateMap<VariableTimeDuration, VariableTimeDurationCreateViewModelFR>().ReverseMap();
            CreateMap<VariableTimeDurationUpdateViewModelFR, VariableTimeDuration>().ReverseMap().IgnoreNullProperty();
            CreateMap<VariableTimeDurationUpdateViewModelFR, VariableTimeDuration>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingPantryConfigViewModel, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfigRepository, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfigVMProp, SettingPantryConfig>().ReverseMap();
            CreateMap<SettingPantryConfig, SettingPantryConfigCreateViewModelFR>().ReverseMap();
            CreateMap<SettingPantryConfigUpdateViewModelFR, SettingPantryConfig>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingPantryConfigUpdateViewModelFR, SettingPantryConfig>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingSmtpViewModel, SettingSmtp>().ReverseMap();
            CreateMap<SettingSmtpRepository, SettingSmtp>().ReverseMap();
            CreateMap<SettingSmtpVMProp, SettingSmtp>().ReverseMap();
            CreateMap<SettingSmtp, SettingSmtpCreateViewModelFR>().ReverseMap();
            CreateMap<SettingSmtpUpdateViewModelFR, SettingSmtp>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingSmtpUpdateViewModelFR, SettingSmtp>().ReverseMap().IgnoreNullProperty();

            CreateMap<SettingRuleBookingViewModel, SettingRuleBooking>().ReverseMap();
            CreateMap<SettingRuleBookingRepository, SettingRuleBooking>().ReverseMap();
            CreateMap<SettingRuleBookingVMProp, SettingRuleBooking>().ReverseMap();
            CreateMap<SettingRuleBooking, SettingRuleBookingCreateViewModelFR>().ReverseMap();
            CreateMap<SettingRuleBookingUpdateViewModelFR, SettingRuleBooking>().ReverseMap().IgnoreNullProperty();
            CreateMap<SettingRuleBookingUpdateViewModelFR, SettingRuleBooking>().ReverseMap().IgnoreNullProperty();

            CreateMap<LicenseListViewModel, LicenseList>().ReverseMap();
            CreateMap<LicenseListRepository, LicenseList>().ReverseMap();
            CreateMap<LicenseListVMProp, LicenseList>().ReverseMap();
            CreateMap<LicenseList, LicenseListCreateViewModelFR>().ReverseMap();
            CreateMap<LicenseListUpdateViewModelFR, LicenseList>().ReverseMap().IgnoreNullProperty();
            CreateMap<LicenseListUpdateViewModelFR, LicenseList>().ReverseMap().IgnoreNullProperty();

            CreateMap<ModuleBackend, ModuleBackendViewModel>().ReverseMap();

            CreateMap<AccessControllerType, AccessControllerTypeViewModel>().ReverseMap();

            CreateMap<AccessControl, AccessControlViewModel>().ReverseMap();
            CreateMap<AccessControl, AccessControlVMCreateFR>().ReverseMap();
            CreateMap<object, AccessControlViewModel>().MapNestedProperties();
            CreateMap<object, AccessControlVMRoom>().MapNestedProperties();

            CreateMap<AccessChannel, AccessChannelViewModel>().ReverseMap();
            CreateMap<object, AccessChannelViewModel>().MapNestedProperties();

            CreateMap<AccessIntegrated, AccessIntegratedViewModel>().ReverseMap();
            CreateMap<object, AccessIntegratedViewModel>().MapNestedProperties();

            CreateMap<EmployeeWithAccessInfo, EmployeeWithAccessInfoViewModel>().ReverseMap();

            //CreateMap<Room, RoomVMUpdateFRViewModel>().ReverseMap();
            CreateMap<EmployeeWithDetails, EmployeeWithDetailsViewModel>().ReverseMap();
            CreateMap<RoomUserCheckin, RoomUserCheckinViewModel>().ReverseMap();
            CreateMap<RoomMergeDetail, RoomMergeDetailViewModel>().ReverseMap();
            CreateMap<RoomDetail, RoomDetailViewModel>().ReverseMap();
            CreateMap<RoomForUsageDetail, RoomForUsageDetailViewModel>().ReverseMap();

            CreateMap<Room, RoomVMUResponseFRViewModel>().ReverseMap();



            CreateMap<RoomDisplay, RoomDisplayViewModel>().ReverseMap();
            CreateMap<RoomDisplaySelect, RoomDisplayViewModel>().MapNestedProperties();
            CreateMap<object, RoomDisplayViewModel>().MapNestedProperties();
            CreateMap<RoomDisplayVMCreateFR, RoomDisplayViewModel>().ReverseMap();
            CreateMap<RoomDisplayVMUpdateFR, RoomDisplayViewModel>().ReverseMap();
            CreateMap<RoomDisplayVMChangeStatusFR, RoomDisplayViewModel>().ReverseMap();
            CreateMap<RoomDisplayVMDeleteFR, RoomDisplayViewModel>().ReverseMap();

            CreateMap<BuildingFloor, BuildingFloorViewModel>().ReverseMap();
            CreateMap<object, BuildingFloorViewModel>().MapNestedProperties();

            CreateMap<KioskDisplay, KioskDisplayViewModel>().ReverseMap();
            CreateMap<object, KioskDisplayViewModel>().MapNestedProperties();
            CreateMap<KioskDisplayVMCreateFR, KioskDisplayViewModel>().ReverseMap();
            CreateMap<KioskDisplayVMUpdateFR, KioskDisplayViewModel>().ReverseMap();
            CreateMap<TimeBookingDTOViewModel, TimeBookingDTO>().ReverseMap();



            CreateMap<LicenseSettingViewModel, LicenseSetting>().ReverseMap();
            CreateMap<LicenseSettingRepository, LicenseSetting>().ReverseMap();
            CreateMap<LicenseSettingVMProp, LicenseSetting>().ReverseMap();
            CreateMap<LicenseSetting, LicenseSettingCreateViewModelFR>().ReverseMap();
            CreateMap<LicenseSettingUpdateViewModelFR, LicenseSetting>().ReverseMap().IgnoreNullProperty();
            CreateMap<LicenseSettingUpdateViewModelFR, LicenseSetting>().ReverseMap().IgnoreNullProperty();
            CreateMap<TimeBookingDTO, BookedTimeViewModel>().ReverseMap();

            CreateMap<RoomForUsageDetailListViewModel, RoomForUsageDetail>().ReverseMap();

            CreateMap<Booking, BookingViewModel>().ReverseMap();
            CreateMap<object, BookingViewModel>().MapNestedProperties();
            CreateMap<BookingChart, BookingVMChart>().MapNestedProperties();
            CreateMap<BookingReportUsage, BookingVMRoomUsageCollection>().MapNestedProperties();

            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Room, RoomVMCheckReschedule>().ReverseMap();
            CreateMap<object, RoomViewModelAlt>().MapNestedProperties();
            CreateMap<RoomVMDefaultFR, RoomViewModel>().ReverseMap();
            CreateMap<RoomVMUpdateFRViewModel, RoomViewModel>().ReverseMap();

            CreateMap<object, RoomVMChartTopRoom>().MapNestedProperties();

            //CreateMap<RoomVMUpdateFRViewModel, RoomViewModel>()
            //    .ForMember(dest => dest.WorkDay,
            //        opt => opt.MapFrom(src => src.WorkDay != null
            //            ? string.Join(",", src.WorkDay)
            //            : string.Empty));

            CreateMap<RoomForUsage, RoomForUsageViewModel>().ReverseMap();
            CreateMap<MenuHeaderLevel, MenuHeaderLevelVM>().ReverseMap();
            CreateMap<LevelMenu, MenuVM>().ReverseMap();

            CreateMap<object, PantryTransactionDetail>().MapNestedProperties();
            CreateMap<PantryTransaksiStatus, PantryTransaksiStatusViewModel>().ReverseMap();

            CreateMap<BookingInvitation, BookingInvitationViewModel>().ReverseMap().IgnoreNullProperty();
            CreateMap<BookingInvitation, BookingInvitationVMCategory>().ReverseMap();
            CreateMap<BookingInvitationEmployee, BookingInvitationVMCategory>().ReverseMap();
            CreateMap<object, BookingInvitationViewModel>().MapNestedProperties();
            // CreateMap<UserLogin, UserLoginVM>().ReverseMap();
            // CreateMap<User, UserLoginVM>().ReverseMap();
            CreateMap<UserLogin, UserViewModel>().ReverseMap();
            CreateMap<ModuleBackend, ModuleDetailsViewModel>().ReverseMap();

            CreateMap<PantryTransaksiAndMenu, PantryTransaksiAndMenuViewModel>().ReverseMap();
            CreateMap<object, PantryTransactionMobileHistory>().MapNestedProperties();

            CreateMap<SendingEmail, SendingEmailViewModel>().ReverseMap();
            CreateMap<SendingNotif, SendingNotifViewModel>().ReverseMap();
            CreateMap<Room, FastBookRoomViewModel>().ReverseMap();
            CreateMap<EmployeeViewModel, FastBookEmployeeViewModel>().ReverseMap();
            CreateMap<BookingInvitation, FastBookBookingInvitationViewModel>().ReverseMap();
            CreateMap<Booking, FastBookBookingViewModel>().ReverseMap();
            CreateMap<BookingInvitation, FastBookBookingInvitation>().ReverseMap();

            CreateMap<BookingInvoice, FastBookBookingInvoice>().ReverseMap();
            CreateMap<EmployeeViewModel, FastBookBookingInvitationViewModel>().ReverseMap();
            CreateMap<FastBookListlDataInternalFRViewModel, FastBookEmployeeViewModel>().ReverseMap();
            CreateMap<Employee, FastBookEmployeeViewModel>().ReverseMap();
            CreateMap<PantryTransaksiD, PantryTransaksiDViewModel>().ReverseMap();
            
            CreateMap<RoomDisplayInformation, RoomDisplayInformationViewModel>().ReverseMap();
            CreateMap<RoomDisplayInformationSelect, RoomDisplayInformationViewModel>().ReverseMap();
            
            CreateMap<HelpItGa, HelpItGaViewModel>().ReverseMap();
            CreateMap<object, HelpItGaViewModel>().MapNestedProperties();
            CreateMap<object, HelpItGaVMDataTableResponse>().MapNestedProperties();

            CreateMap<RoomDisplayInformationMeetingDTO, RoomDisplayInformationMeetingViewModel>().ReverseMap();
            
        }
    }

    public class AutoMapConfig
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfilling>();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            });

            return config;
        }
    }
}
