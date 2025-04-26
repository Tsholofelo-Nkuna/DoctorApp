using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ApiBaseController<DoctorDto, DoctorEntity, DoctorFilter>
    {
      
        public DoctorsController(IUserContextService userContextService,IHttpClientFactory httpClient, IDocterService docterService) : base(userContextService, httpClient, docterService)
        {
          
        }


    }
}
