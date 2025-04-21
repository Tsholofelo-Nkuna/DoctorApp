using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IUserContextService userContextService;
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly HttpClient AppApi;

        public ApiBaseController(IUserContextService userContextService, IHttpClientFactory httpClientFactory)
        {
            this.userContextService = userContextService;
            _httpClientFactory = httpClientFactory;
            AppApi = _httpClientFactory.CreateClient(WebApiNameConstants.AppApi);
        }
    }
}
