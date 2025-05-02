
using Blazored.LocalStorage;
using DoctorManagement.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, ServiceScope scope)
        {
           
            if(scope == ServiceScope.Singleton)
            {
                services.AddBlazoredLocalStorageAsSingleton();
            }
            else if(scope == ServiceScope.Scoped)
            {
               services.AddBlazoredLocalStorage();
            }
            else
            {
                
            }
            return services;
        }
    }
}
