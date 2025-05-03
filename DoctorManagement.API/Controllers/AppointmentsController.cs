using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = $"{RoleConstants.Patient}")]
    public class AppointmentsController : ApiBaseController<AppointmentDto, AppointmentEntity, AppointmentFilter>
    {
        public AppointmentsController(IUserContextService userContextService, IHttpClientFactory httpClientFactory, IAppointmentService primaryService) : base(userContextService, httpClientFactory, primaryService)
        {
        }
    }
}
