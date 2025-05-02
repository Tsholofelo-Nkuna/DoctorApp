using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;


namespace DoctorManagement.Api.Consumer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConsumers(this IServiceCollection services, string apiBaseAddress, ServiceScope scope = ServiceScope.Scoped)
        {

            services
                .AddHttpClient()
                 .Configure<ApiOptions>(c =>
                 {
                     c.HttpClientName = "DefaultApi";
                     c.BaseAddress = apiBaseAddress;
                 });
            
            if(scope == ServiceScope.Scoped)
            {
                services
                   
                    .AddScoped<IPatientApiConsumer, PatientApiConsumer>()
                    .AddScoped<IIdentityApiConsumer, IdentityApiConsumer>()
                    .AddScoped<IDoctorApiConsumer, DoctorApiConsumer>()
                    .AddScoped<IDataSourceApiConsumer, DataSourceApiConsumer>();
            }
            else if(scope == ServiceScope.Singleton)
            {
                services
                   
                   .AddSingleton<IPatientApiConsumer, PatientApiConsumer>()
                   .AddSingleton<IIdentityApiConsumer, IdentityApiConsumer>()
                   .AddSingleton<IDoctorApiConsumer, DoctorApiConsumer>()
                   .AddSingleton<IDataSourceApiConsumer, DataSourceApiConsumer>();
            }
            else
            {
                services
                .AddTransient<IPatientApiConsumer, PatientApiConsumer>()
                .AddTransient<IIdentityApiConsumer, IdentityApiConsumer>()
                .AddTransient<IDoctorApiConsumer, DoctorApiConsumer>()
                .AddTransient<IDataSourceApiConsumer, DataSourceApiConsumer>();
            }
                
                

            return services;
        }
    }
}
