using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ApiBaseController
    {
        private readonly IPatientService _patientService;
      
        public PatientsController(IPatientService patientService, IUserContextService userContextService, IHttpClientFactory httpClientFactory) : base(userContextService, httpClientFactory) { 
            _patientService = patientService;
        }
        // POST api/<PatientsController>
        [HttpPost]
        public async Task<ResponseDto<bool>> Post([FromBody] PatientDto patient)
        {
          var added =  this._patientService.AddOrUpdate([patient], (await this.userContextService.GetCurrentUserAsync())?.Id);
            return new()
            {
                Data = added,
                Message = added ? "New patient created" : "Failed to create new patient"
            };
        }

        // PUT api/<PatientsController>/5
        [HttpPost("Get")]
        public PageResponse<PatientDto> GetPatients([FromBody] PageRequestDto<PatientDto> pageRequest)
        {
            return this._patientService.Get(pageRequest);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async void Delete(Guid patientId)
        {
           var currentUser = (await this.userContextService.GetCurrentUserAsync())?.Id;
            _patientService.Delete([patientId], currentUser);
        }
    }
}
