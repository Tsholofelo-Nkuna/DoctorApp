using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Presentation.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IUserContextService _userContextService;
        public PatientsController(IPatientService patientService, IUserContextService userContextService) { 
            _patientService = patientService;
            _userContextService = userContextService;
        }
        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] PatientDto patient)
        {
        }

        // PUT api/<PatientsController>/5
        [HttpPost("Get")]
        public void Put([FromBody] PageRequestDto<PatientDto> pageRequest)
        {
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async void Delete(Guid patientId)
        {
           var currentUser =  await _userContextService.GetCurrentUserAsync();
            _patientService.Delete([patientId], currentUser);
        }
    }
}
