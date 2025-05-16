using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models.Base;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public class IdentityApiConsumer : ApiConsumerBase<SignupDto, BaseFilter>, IIdentityApiConsumer
    {

        
        public IdentityApiConsumer(
            IOptions<ApiOptions> options, 
            IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
            this.ControllerName = "Accounts";
        }

        public async Task<ResponseDto<IEnumerable<Guid>>?> DoctorSignup(SignupDoctorDto doctorSignUp)
        {
            var apiResponse = await this.HttpClient.PostAsJsonAsync<SignupDoctorDto>($"api/Accounts/Signup/Doctor", doctorSignUp);
            if (apiResponse is { IsSuccessStatusCode: true } && await (apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<Guid>>>()) is ResponseDto<IEnumerable<Guid>> responseContent)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseDto<TokenResponseDto>?> Login(string userName, string password)
        {
            try
            {
                var response = await this.HttpClient.PostAsJsonAsync<LoginCredentialsDto>(
               "api/accounts/signin" , new LoginCredentialsDto {
                   Username = userName,
                   Password = password
               });
             
                if (response is { IsSuccessStatusCode: true } && ( await response.Content.ReadFromJsonAsync<ResponseDto<TokenResponseDto>>()) is ResponseDto<TokenResponseDto> token)
                {
                    return new()
                    {
                        Data = token.Data,
                        Message = "Login successful"
                    };
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

        public async Task<ResponseDto<IEnumerable<Guid>>?> PatientSignup(SignupDto patientSignUp)
        {
            try
            {
                patientSignUp.Contact.Email = patientSignUp.Credentials.Username;
                var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/Accounts/Signup/Patient", patientSignUp);
                if (apiResponse is { IsSuccessStatusCode: true } && await (apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<Guid>>>()) is ResponseDto<IEnumerable<Guid>> responseContent)
                {
                    return responseContent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { 
               return null;
            }
        }
    }
}
