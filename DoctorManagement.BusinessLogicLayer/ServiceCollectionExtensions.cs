using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services;
using DoctorManagement.Integrations;
using DoctorManagement.Integrations.PayFast.Handlers;
using DoctorManagement.Shared.Services;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace DoctorManagement.BusinessLogicLayer
{
    public static class ServiceCollectionExtensions
    {
       public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {

            var apiBaseAddress = "https://www.payfast.co.za​/eng/process";
        #if DEBUG
                    apiBaseAddress = "https://sandbox.payfast.co.za​/eng/process";
        #endif
            services
               .AddHttpClient("PayFaseApi", c =>
               {
                   c.BaseAddress = new Uri(apiBaseAddress);
               });
            services.AddMapster();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IDocterService, DoctorService>();
            services.AddScoped<IDataSourceService, DataSourceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<MailSenderService>();
            services.AddIntegrationServices();
            return services;
        }
    }
}
