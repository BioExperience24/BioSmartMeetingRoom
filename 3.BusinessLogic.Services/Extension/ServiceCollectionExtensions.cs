using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;
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
        services.AddScoped<IAccessControlService, AccessControlService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDepartmentService, DepartementService>();
        services.AddScoped<IDivisiService, DivisiService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IFacilityService, FacilityService>();
    }
    public static void AddCustomRepository(this IServiceCollection services)
    {
        services.AddScoped<AccessControlRepository>();
        services.AddScoped<CompanyRepository>();
        services.AddScoped<DepartmentRepository>();
        services.AddScoped<DivisiRepository>();
        services.AddScoped<EmployeeRepository>();
        services.AddScoped<FacilityRepository>();
    }

    /// <summary>
    /// Auto Mapping Vice Versa and Map List
    /// </summary>
    public class AutoMapperProfilling : Profile
    {
        public AutoMapperProfilling()
        {
            CreateMap<AccessControlViewModel, AccessControl>().ReverseMap();
            CreateMap<CompanyViewModel, Company>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<DivisiRepository, Divisi>().ReverseMap();
            CreateMap<EmployeeRepository, Employee>().ReverseMap();
            CreateMap<FacilityRepository, Facility>().ReverseMap();
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
