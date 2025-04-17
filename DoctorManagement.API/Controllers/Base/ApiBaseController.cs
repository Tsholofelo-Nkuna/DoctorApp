using DoctorManagement.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IUserContextService userContextService;

        public ApiBaseController(IUserContextService userContextService) {
          this.userContextService = userContextService;
        }
    }
}
