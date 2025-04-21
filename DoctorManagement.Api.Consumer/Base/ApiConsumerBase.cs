using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Base
{
    public class ApiConsumerBase<TDto> : IApiConsumerBase<TDto> where TDto : new()
    {
       
        public string AccessToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected HttpClient HttpClient { get; set; }
       

        public ApiConsumerBase(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory)
        {
            var handler = new HttpClientHandler();

#if DEBUG
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
#endif

            var client = new HttpClient(handler);
            HttpClient = httpClientFactory.CreateClient(options.Value.HttpClientName);
#if DEBUG
            HttpClient = client;
#endif
            this.HttpClient.BaseAddress = new Uri(options.Value.BaseAddress);
            
        }
        public virtual async Task<ResponseDto<bool>?> Add(TDto dto, string controller)
        {
            var apiResponse = await this.HttpClient.PostAsJsonAsync(controller, dto);
            if(apiResponse is { IsSuccessStatusCode : true } && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> responseContent)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<ResponseDto<bool>?> Delete(Guid guid, string controller)
        {
            var apiResponse = await this.HttpClient.DeleteAsync($"{controller}/{guid}");
            if(apiResponse is { IsSuccessStatusCode :true } && await (apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> responseContent)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<PageResponse<TDto>?> Get(PageRequestDto<TDto> pageRequest, string controller)
        {
            var apiResponse = await this.HttpClient.PostAsJsonAsync($"{controller}/Get", pageRequest);
            if(apiResponse is { IsSuccessStatusCode :true } && (await apiResponse.Content.ReadFromJsonAsync<PageResponse<TDto>>()) is PageResponse<TDto> response)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public virtual (string accessToken, string refreshToken) GetAccessToken(string username, string password)
        {
            return ("","");
        }
    }
}
