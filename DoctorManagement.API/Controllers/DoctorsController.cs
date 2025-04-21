using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;

using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ApiBaseController
    {
        private readonly IDocterService _docterService;
        public DoctorsController(IUserContextService userContextService,IHttpClientFactory httpClient, IDocterService docterService) : base(userContextService, httpClient)
        {
            this._docterService = docterService;
        }

        [HttpPost("Get")]
        public PageResponse<DoctorDto> GetDoctors(PageRequestDto<DoctorDto> pageRequest)
        {
            return this._docterService.Get(pageRequest);
        }

        [HttpPost]
        public async Task<ResponseDto<bool>> Post(DoctorDto doctor)
        {
            var result = this._docterService.AddOrUpdate([doctor], (await this.userContextService.GetCurrentUserAsync()).Id);
            return new()
            {
                Data = result,
            };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            var result =  this._docterService.Delete([id], (await this.userContextService.GetCurrentUserAsync()).Id);
            return new()
            {
                Data = result,
            };
        }
    }
}
