using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models.Base;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Base
{
    public class ApiConsumerBase<TDto, TFilter> : IApiConsumerBase<TDto, TFilter> where TDto : new() where TFilter: BaseFilter, new()
    {
        public string ControllerName { get; set; }
       
        public string AccessToken
        {
            get => _token;
            set
            {
                _token = value;
                HttpClient.DefaultRequestHeaders.Authorization = new("Bearer", _token);
            }
        }
        protected HttpClient HttpClient { get; set; }
        private string _token;
       
        public event EventHandler<ResponseDto<bool>> Unauthorized;
      

        public ApiConsumerBase(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory)
        {
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && (cert.Issuer.Equals("CN=localhost") || cert.Issuer.Contains("let's encrypt", StringComparison.OrdinalIgnoreCase)))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            var client = new HttpClient(handler);
            HttpClient = httpClientFactory.CreateClient(options.Value.HttpClientName);
#if DEBUG
            HttpClient = client;
#endif
            this.HttpClient.BaseAddress = new Uri(options.Value.BaseAddress);
            
        }

        public virtual async Task<ResponseDto<IEnumerable<string>>?> GetUserRoles()
        {
            try
            {
                var apiResponse = await this.HttpClient.GetAsync($"api/{this.ControllerName}/GetUserRoles");
                if (apiResponse is { IsSuccessStatusCode: true } && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<string>>>()) is ResponseDto<IEnumerable<string>> responseContent)
                {
                    responseContent.StatusCode = HttpStatusCode.OK;
                    return responseContent;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    ResponseDto<IEnumerable<string>> results = new()
                    {
                        Data = [],
                        Message = "Not authorized",
                        StatusCode = HttpStatusCode.Unauthorized,
                    };
                    Unauthorized?.Invoke(this, new() { Data = false, Message = results.Message, StatusCode = results.StatusCode });
                    return results;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public virtual async Task<ResponseDto<string>> GetUserId()
        {

            try
            {
                var apiResponse = await this.HttpClient.GetAsync($"api/{this.ControllerName}/GetUserId");
                if (apiResponse is { IsSuccessStatusCode: true } && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<string>>()) is ResponseDto<string> responseContent)
                {
                    responseContent.StatusCode = HttpStatusCode.OK;
                    return responseContent;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    ResponseDto<string> results = new()
                    {
                        Data = string.Empty,
                        Message = "Not authorized",
                        StatusCode = HttpStatusCode.Unauthorized,
                    };
                    Unauthorized?.Invoke(this, new() { Data = false, Message = results.Message, StatusCode = results.StatusCode });
                    return results;
                }
                else
                {
                    return new() { Data = string.Empty};
                }
            }
            catch (Exception ex)
            {

                return new();
            }
        }

        public virtual async Task<ResponseDto<IEnumerable<Guid>>?> Add(TDto dto)
        {
            try
            {
                var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/{this.ControllerName}", dto);
                if (apiResponse is { IsSuccessStatusCode: true } && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<Guid>>>()) is ResponseDto<IEnumerable<Guid>> responseContent)
                {
                    responseContent.StatusCode = HttpStatusCode.OK;
                    return responseContent;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    ResponseDto<IEnumerable<Guid>> results = new()
                    {
                        Data = [],
                        Message = "Not authorized",
                        StatusCode = HttpStatusCode.Unauthorized,
                    };
                    Unauthorized?.Invoke(this, new() { Data = false, Message = results.Message, StatusCode = results.StatusCode});
                    return results;
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

        public virtual async Task<ResponseDto<bool>?> Delete(Guid guid)
        {
            try
            {
                var apiResponse = await this.HttpClient.DeleteAsync($"api/{ControllerName}/{guid}");
                if (apiResponse is { IsSuccessStatusCode: true } && await (apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> responseContent)
                {
                    responseContent.StatusCode = HttpStatusCode.OK;
                    return responseContent;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    string message = "Not authorized";
                    var results = new ResponseDto<bool>()
                    {
                        Data = false,
                        Message = message,
                        StatusCode = HttpStatusCode.Unauthorized,
                    };
                    Unauthorized?.Invoke(this, results);
                    return results;
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

        public virtual async Task<PageResponse<TDto>?> Get(PageRequestDto<TFilter> pageRequest)
        {
            try
            {
                var apiResponse = await this.HttpClient.PostAsJsonAsync($"api/{ControllerName}/Get", pageRequest);
                if (apiResponse is { IsSuccessStatusCode: true } && (await apiResponse.Content.ReadFromJsonAsync<PageResponse<TDto>>()) is PageResponse<TDto> response)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                else if (apiResponse is { StatusCode: HttpStatusCode.Unauthorized })
                {
                    string message = "Not authorized";
                    Unauthorized?.Invoke(this, new()
                    {
                        Data = false,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = message

                    });
                    return new()
                    {
                        Data = [],
                        Message = message,
                        StatusCode = HttpStatusCode.Unauthorized,
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
    }
}
