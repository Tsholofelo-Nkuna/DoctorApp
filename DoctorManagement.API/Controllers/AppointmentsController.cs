using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = $"{RoleConstants.Patient}, {RoleConstants.Doctor}")]
    public class AppointmentsController : ApiBaseController<AppointmentDto, AppointmentEntity, AppointmentFilter>
    {
        private readonly IAppointmentService _appointmentsService;
        public AppointmentsController(IUserContextService userContextService, IHttpClientFactory httpClientFactory, IAppointmentService primaryService) : base(userContextService, httpClientFactory, primaryService)
        {
            _appointmentsService = primaryService;
        }

        [AllowAnonymous]
        public override PageResponse<AppointmentDto> Get([FromBody] PageRequestDto<AppointmentFilter> pageRequest)
        {
            if(this.userContextService.GetCurrentUserAsync().Result is IdentityUser currentUser)
            {
                pageRequest.Filter.CurrentUserId = currentUser.Id;
            }
            return base.Get(pageRequest);
        }

        [HttpPost("[action]/{appointmentId}"), AllowAnonymous]
        public async Task<ResponseDto<AppointmentDto?>> Accept(Guid appointmentId)
        {
            var acceptResult =  await this._appointmentsService.Accept(appointmentId);
            return new()
            {
                Data = acceptResult,
                Message = acceptResult is not null ? "Appointment accepted" : "Failed to accept appointment",
                StatusCode = acceptResult is not null ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest,
            };
        }

        [HttpPost("[action]/{appointmentId}"), AllowAnonymous]
        public async Task<ResponseDto<AppointmentDto?>> Reject(Guid appointmentId)
        {
            var acceptResult = await this._appointmentsService.Reject(appointmentId);
            return new()
            {
                Data = acceptResult,
                Message = acceptResult is not null ? "Appointment rejected" : "Failed to accept appointment",
                StatusCode = acceptResult is not null ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest,
            };
        }
    }
}
