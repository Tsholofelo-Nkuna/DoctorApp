using AutoMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;


namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class AppointmentService : ServiceBase<AppointmentDto, AppointmentEntity, AppointmentFilter>, IAppointmentService
    {
        public AppointmentService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public override bool AddOrUpdate(List<AppointmentDto> inserted, string? currentUserId)
        {
            var targetDoctorEntities = from insertedRec in inserted.Where(doc => doc.DoctorId != Guid.Empty)
                                join doctor in this.dbContext.Doctors.AsNoTracking()
                                .Include(d => d.Contact).Include(d => d.PracticeSite).Include(x => x.Title)
                                on insertedRec.DoctorId equals doctor.Id
                                select doctor;
            var targetPatient = dbContext.Patients.AsNoTracking()
                .Where(patient => patient.UserId == currentUserId)
                .Include(p => p.Address)
                .Include(p => p.Contact);
             
            var targetDoctors = targetDoctorEntities
                .Select(doc => mapper.Map<DoctorDto>(doc));
            var targetPatients = targetPatient.Select(patient => mapper.Map<PatientDto>(patient));
            var appointmentTypes = (from appointmentType in dbContext.DataSource.AsNoTracking().Where(app =>
                                    app.TypeCode == DataSourceTypeCodeConstants.AppointmentType).ToList()
                                   join appointmentRec in inserted
                                   on appointmentType.Value equals appointmentRec.AppointmentTypeId
                                   select appointmentType).Select(aTypeE => mapper.Map<DataSourceDto>(aTypeE));

            inserted.ForEach(insert =>
            {
                if(targetDoctors.Any(target => target.Id == insert.DoctorId))
                {
                    insert.Doctor = targetDoctors.FirstOrDefault(tDoc => tDoc.Id == insert.DoctorId)!;
                   
                }
                if(targetPatients.Any())
                {
                    insert.Patient = targetPatients.FirstOrDefault()!;
                }
                if(appointmentTypes.Any(aType => aType.Value == insert.AppointmentTypeId))
                {
                    insert.AppointmentType = appointmentTypes.FirstOrDefault(x => x.Value == insert.AppointmentTypeId);
                }
            });
            return base.AddOrUpdate(inserted, currentUserId);
        }
        protected override IQueryable<AppointmentEntity> GetQueryable(AppointmentFilter filters)
        {
            var query =  base.GetQueryable(filters);
            if (!string.IsNullOrWhiteSpace(filters.CurrentUserId))
            {
                query = query.Where(appointment => appointment.Patient.UserId == filters.CurrentUserId);
            }
            return query
                .Include(appointment => appointment.Patient)
                .Include(appointment => appointment.AppointmentType)
                .Include(appointment => appointment.Doctor)
                .ThenInclude(doc => doc.Title)
                .Include(doc => doc.Doctor.PracticeSite)
                .Include(doc => doc.Doctor.Contact);
        }
        public override AppointmentDto? Map(AppointmentEntity? source)
        {
            var appointmentDto = base.Map(source);
            if(source is AppointmentEntity validSource && appointmentDto is AppointmentDto validDto )
            {
                if(validSource.AppointmentType is DataSourceEntity validAppointmentType)
                {
                   validDto.AppointmentType = validAppointmentType.CopyTo(validDto.AppointmentType);
                   validDto.AppointmentTypeId = validDto.AppointmentType?.Value ?? default;
                }

                if(validSource.Doctor is DoctorEntity validDoctorSource)
                {
                    validDto.Doctor = validDoctorSource.CopyTo(validDto.Doctor);
                    validDto.DoctorId = validDto.Doctor?.Id ?? default;
                    if (validDoctorSource.Title is DataSourceEntity validTitleSource && validDto.Doctor is DoctorDto validDoctorTarget)
                    {
                        validDoctorTarget.Title = validTitleSource.CopyTo(validDoctorTarget.Title);
                    }
                }

                if(validSource.Patient is PatientEntity validPatientSource)
                {
                    validDto.Patient = validPatientSource.CopyTo(validDto.Patient);
                    validDto.PatientId = validDto?.Patient?.Id ?? default;
                }
            }

            return appointmentDto;
        }
    }
}
