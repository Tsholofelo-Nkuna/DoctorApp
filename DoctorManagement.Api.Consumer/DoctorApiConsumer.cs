using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public class DoctorApiConsumer : ApiConsumerBase<DoctorDto, DoctorFilter>, IDoctorApiConsumer
    {

       // public override event EventHandler<ResponseDto<bool>> Unauthorized;
        public DoctorApiConsumer(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
            this.ControllerName = "Doctors";
        }

        public async Task<ResponseDto<DoctorSettingsDto>> UpdateDoctorSettingsForCurrentUser(DoctorSettingsDto settings)
        {

            try
            {
                var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/{this.ControllerName}/UpdateDoctorSettingsForCurrentUser", settings);
                if (apiResponse is { IsSuccessStatusCode: true } && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<DoctorSettingsDto>>()) is ResponseDto<DoctorSettingsDto> responseContent)
                {
                    responseContent.StatusCode = HttpStatusCode.OK;
                    return responseContent;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    ResponseDto<DoctorSettingsDto> results = new()
                    {
                        Message = "Not authorized",
                        StatusCode = HttpStatusCode.Unauthorized,
                    };
                    Unauthorized?.Invoke(this, new() { Message = results.Message, StatusCode = results.StatusCode });
                    return results;
                }
                else
                {
                    return new();
                }
            }
            catch (Exception ex)
            {

                return new();
            }
        }
    }
}
