using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourceController : ApiBaseController<DataSourceDto, DataSourceEntity, DataSourceFilter>
    {
        private readonly IDataSourceService _dataSourceService;
        public DataSourceController(IUserContextService userContextService, IHttpClientFactory httpClientFactory, IDataSourceService dataSourceService) : base(userContextService, httpClientFactory, dataSourceService)
        {
            _dataSourceService = dataSourceService;
            
        }

    }
}
