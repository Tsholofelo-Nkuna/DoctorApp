using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourceController : ApiBaseController<DataSourceDto, DataSourceEntity, DataSourceFilter>
    {
        private readonly IDataSourceService _dataSourceService;
        public DataSourceController(IUserContextService userContextService,
            IHttpClientFactory httpClientFactory, IDataSourceService dataSourceService) : base(userContextService, httpClientFactory, dataSourceService)
        {
            _dataSourceService = dataSourceService;
            
        }

        [AllowAnonymous]
        public override Task<ResponseDto<IEnumerable<Guid>>> Post([FromBody] DataSourceDto rec)
        {
            return base.Post(rec);
        }

    }
}
