using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DoctorsController : ApiBaseController<DoctorDto, DoctorEntity, DoctorFilter>
    {
      
        public DoctorsController(IUserContextService userContextService,IHttpClientFactory httpClient, IDocterService docterService) : base(userContextService, httpClient, docterService)
        {
        }

        [AllowAnonymous]
        public override Task<ResponseDto<IEnumerable<Guid>>> Post([FromBody] DoctorDto rec)
        {
            return base.Post(rec);
        }

        [HttpPost("[action]")]
        public async Task<ResponseDto<DoctorSettingsDto>> UpdateDoctorSettingsForCurrentUser(DoctorSettingsDto settings)
        {
            var doctorService = _principalService as IDocterService;
            var serviceReponse = await doctorService!.UpdateDoctorSettingsForCurrentUser(settings);
            return serviceReponse ?? new();
        }
    }
}
