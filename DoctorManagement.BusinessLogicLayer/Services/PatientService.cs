using AutoMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class PatientService : ServiceBase<PatientDto, PatientEntity>, IPatientService
    {
        public PatientService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
          
        }

        protected override IQueryable<PatientEntity> GetQueryable(PatientDto filters)
        {
            return base.GetQueryable(filters).Include(x => x.Contact).Include(x => x.Address);
        }
    }
}
