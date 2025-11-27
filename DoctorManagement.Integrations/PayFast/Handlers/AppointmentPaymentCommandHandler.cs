using DoctorManagement.Integrations.PayFast.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Integrations.PayFast.Handlers
{
    public class AppointmentPaymentCommandHandler(IHttpClientFactory httpClientFactory, ILogger<AppointmentPaymentCommandHandler> logger) : IRequestHandler<AppointmentPaymentCommand, string>
    {
       
        
        public async Task<string> Handle(AppointmentPaymentCommand request, CancellationToken cancellationToken)
        {
            var  httpClient = httpClientFactory.CreateClient("PayFaseApi");
            var payload = new Dictionary<string, string> {
                    ["merchant_id"] = "15517635",
                    ["merchant_key"] = "4mhc1jef80pti",
                    ["amount"] = $"{request.Appointment.Doctor?.ConsultationFee:F2}",
                    ["item_name"] = request.Appointment.Id.ToString(),

            };
            var formData = new FormUrlEncodedContent(payload);
            var response = await httpClient.PostAsync("/", formData);
            logger.LogInformation($"{response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return  await response.Content.ReadAsStringAsync();
        }
    }
}
