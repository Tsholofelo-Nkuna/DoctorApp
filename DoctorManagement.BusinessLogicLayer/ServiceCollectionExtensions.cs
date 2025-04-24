using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorManagement.BusinessLogicLayer
{
    public static class ServiceCollectionExtensions
    {
       public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IDocterService, DoctorService>();
            services.AddScoped<IDataSourceService, DataSourceService>();
            return services;
        }
    }
}
