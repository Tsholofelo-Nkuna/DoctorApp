using AutoMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class AppointmentService : ServiceBase<AppointmentDto, AppointmentEntity, AppointmentFilter>, IAppointmentService
    {
        public AppointmentService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override bool Add(List<AppointmentDto> inserted, string? currentUserId)
        {
            var targetDoctorEntities = from insertedRec in inserted.Where(doc => doc.DoctorId != Guid.Empty)
                                join doctor in this.dbContext.Doctors.AsNoTracking()
                                on insertedRec.DoctorId equals doctor.Id
                                select doctor;
            var targetPatientEntitues = from insertedRec in inserted.Where(patient => patient.PatientId != Guid.Empty)
                                 join dbPatient in dbContext.Patients.AsNoTracking()
                                 on insertedRec.PatientId equals dbPatient.Id
                                 select dbPatient;
            var targetDoctors = targetDoctorEntities.Select(doc => mapper.Map<DoctorDto>(doc));
            var targetPatients = targetPatientEntitues.Select(patient => mapper.Map<PatientDto>(patient));
            inserted.ForEach(insert =>
            {
                if(targetDoctors.Any(target => target.Id == insert.DoctorId))
                {
                    insert.Doctor = targetDoctors.FirstOrDefault(tDoc => tDoc.Id == insert.DoctorId)!;
                   
                }
                if(targetPatients.Any(tPatient => tPatient.Id == insert.PatientId))
                {
                    insert.Patient = targetPatients.FirstOrDefault(p => p.Id == insert.PatientId)!;
                }
            });
            return base.Add(inserted, currentUserId);
        }
    }
}
