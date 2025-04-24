using DoctorManagement.Api.Consumer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConsumers(this IServiceCollection services, string apiBaseAddress)
        {

            services
                .AddHttpClient()
                 .Configure<ApiOptions>(c =>
                 {
                     c.HttpClientName = "DefaultApi";
                     c.BaseAddress = apiBaseAddress;
                 })
                .AddScoped<IPatientApiConsumer, PatientApiConsumer>()
                .AddScoped<IIdentityApiConsumer, IdentityApiConsumer>()
                .AddScoped<IDoctorApiConsumer, DoctorApiConsumer>();
               

            return services;
        }
    }
}
