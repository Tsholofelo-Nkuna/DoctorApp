using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.BusinessLogicLayer.Services;
using DoctorManagement.DataAccessLayer.Entities.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController<TDto, TEntity, TFilter> : ControllerBase where TDto: DtoBase, new() where TFilter : BaseFilter, new() where TEntity : EntityBase
    {
        protected readonly IUserContextService userContextService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceBase<TDto, TEntity, TFilter> _principalService;
        protected readonly HttpClient AppApi;

        public ApiBaseController(
            IUserContextService userContextService, 
            IHttpClientFactory httpClientFactory, 
            IServiceBase<TDto, TEntity, TFilter> primaryService)
        {
            this.userContextService = userContextService;
            _httpClientFactory = httpClientFactory;
            AppApi = _httpClientFactory.CreateClient(WebApiNameConstants.AppApi);
            this._principalService = primaryService;
        }

        [HttpPost]
         public virtual async Task<ResponseDto<bool>> Post([FromBody] TDto rec)
        {
            var added = this._principalService.AddOrUpdate([rec], (await this.userContextService.GetCurrentUserAsync())?.Id);
            return new()
            {
                Data = added,
                Message = added ? "New record created" : "Failed to create new record"
            };
        }

        // PUT api/<PatientsController>/5
        [HttpPost("Get")]
        public virtual PageResponse<TDto> Get([FromBody] PageRequestDto<TFilter> pageRequest)
        {
            return this._principalService.Get(pageRequest);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public virtual async Task<ResponseDto<bool>> Delete(Guid patientId)
        {
            var currentUser = (await this.userContextService.GetCurrentUserAsync())?.Id;
            var result =  _principalService.Delete([patientId], currentUser);
            return new()
            {
                Data = result,
                Message = result ? "Record deleted successully": "Delete failed"
            };
        }
    }
}
