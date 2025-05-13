using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public class AppointmentApiConsumer : ApiConsumerBase<AppointmentDto, AppointmentFilter>, IAppointmentApiConsumer
    {
        public AppointmentApiConsumer(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
            this.ControllerName = "Appointments";
        }

        public async Task<ResponseDto<AppointmentDto?>?> Accept(Guid appointmentId)
        {
            try
            {
                var result = await this.HttpClient.PostAsync($"api/{this.ControllerName}/Accept", null);
                if(result is { IsSuccessStatusCode: true} && await( result.Content.ReadFromJsonAsync<ResponseDto<AppointmentDto?>?>() ) is ResponseDto<AppointmentDto?> validReasponseContent)
                {
                    return validReasponseContent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ResponseDto<AppointmentDto?>?> Reject(Guid appointmentId)
        {
            try
            {
                var result = await this.HttpClient.PostAsync($"api/{this.ControllerName}/Reject", null);
                if (result is { IsSuccessStatusCode: true } && await(result.Content.ReadFromJsonAsync<ResponseDto<AppointmentDto?>?>()) is ResponseDto<AppointmentDto?> validReasponseContent)
                {
                    return validReasponseContent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
