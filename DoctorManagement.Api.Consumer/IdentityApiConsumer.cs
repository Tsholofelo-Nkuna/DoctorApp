using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public class IdentityApiConsumer : ApiConsumerBase<SignupDto>, IIdentityApiConsumer
    {
        public IdentityApiConsumer(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {

        }

        public async Task<ResponseDto<bool>?> DoctorSignup(SignupDoctorDto patientSignUp)
        {
            var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/Identity/Signup/Doctor", patientSignUp);
            if (apiResponse is { IsSuccessStatusCode: true } && await (apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> responseContent)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseDto<bool>?> PatientSignup(SignupDto patientSignUp)
        {
            patientSignUp.Contact.Email = patientSignUp.Credentials.Username;
            var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/Accounts/Signup/Patient", patientSignUp);
            if (apiResponse is { IsSuccessStatusCode: true } && await(apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> responseContent)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
