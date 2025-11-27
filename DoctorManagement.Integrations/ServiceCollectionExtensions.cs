using DoctorManagement.Integrations.PayFast.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Integrations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AppointmentPaymentCommand).Assembly);
            return services;
        }
    }
}
