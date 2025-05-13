using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoctorManagement.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUtilServices(this IServiceCollection services, ServiceScope scope)
        {
            services.Configure<EmailSettings>(config => {
                config.Smtp = "smtp.gmail.com";
                config.SendFrom = "izybill85@gmail.com";
                config.Password = "owuj gxtw ycfz xxwj";
                config.Port = 587;
            });
            if (scope == ServiceScope.Scoped)
            {
                services.AddScoped<MailSenderService>();
            }
            else if(scope == ServiceScope.Singleton)
            {
                services.AddSingleton<MailSenderService>();
            }
            else
            {
                services.AddTransient<MailSenderService>();
            }
          
            return services;
        }
    }
}
