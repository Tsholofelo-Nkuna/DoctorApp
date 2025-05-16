using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PatientsController : ApiBaseController<PatientDto, PatientEntity, BaseFilter>
    {
       
      
        public PatientsController(
            IPatientService patientService, 
            IUserContextService userContextService, 
            IHttpClientFactory httpClientFactory) : 
            base(userContextService, httpClientFactory, patientService)
        { 
         
        }

        [AllowAnonymous]
        public override Task<ResponseDto<IEnumerable<Guid>>> Post([FromBody] PatientDto rec)
        {
            return base.Post(rec);
        }

    }
}
