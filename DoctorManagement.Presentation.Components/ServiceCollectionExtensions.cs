
using Blazored.LocalStorage;
using DoctorManagement.Presentation.Services;
using DoctorManagement.Presentation.Services.Interfaces;
using DoctorManagement.Shared;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DoctorManagement.Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, ServiceScope scope, IConfiguration configs)
        {

            if (scope == ServiceScope.Singleton)
            {
                services.AddBlazoredLocalStorageAsSingleton();
                services.AddUtilServices(ServiceScope.Singleton);
                services.AddSingleton<IMailTemplateMessageHandler, MailTemplateMessageHandler>();
            }
            else if(scope == ServiceScope.Scoped)
            {
               services.AddBlazoredLocalStorage();
               services.AddUtilServices(ServiceScope.Scoped);
               services.AddScoped<IMailTemplateMessageHandler, MailTemplateMessageHandler>();
            }
            else
            {
                services.AddUtilServices(ServiceScope.Transient);
                services.AddTransient<IMailTemplateMessageHandler, MailTemplateMessageHandler>();
            }
            return services;
        }
    }
}
