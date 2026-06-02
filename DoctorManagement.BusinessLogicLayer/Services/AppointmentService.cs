using MapsterMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Integrations.PayFast.Commands;
using DoctorManagement.Shared;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class AppointmentService : ServiceBase<AppointmentDto, AppointmentEntity, AppointmentFilter>, IAppointmentService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediatr;
        public AppointmentService(WebDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager, IMediator mediatr) : base(dbContext, mapper)
        {
            _userManager = userManager;
            _mediatr = mediatr;
        }
        public override IEnumerable<Guid> AddOrUpdate(List<AppointmentDto> inserted, string? currentUserId)
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
                .Select(doc => doc.Adapt<DoctorDto>());
            var targetPatients = targetPatient.Select(patient => patient.Adapt<PatientDto>());
            var appointmentTypes = (from appointmentType in dbContext.DataSource.AsNoTracking().Where(app =>
                                    app.TypeCode == DataSourceTypeCodeConstants.AppointmentType).ToList()
                                   join appointmentRec in inserted
                                   on appointmentType.Value equals appointmentRec.AppointmentTypeId
                                   select appointmentType).Select(aTypeE => aTypeE.Adapt<DataSourceDto>());

            var paymentTypes = (from paymentType in dbContext.DataSource.AsNoTracking().Where(app =>
                                    app.TypeCode == DataSourceTypeCodeConstants.PaymentMethod).ToList()
                                join appointmentRec in inserted
                                on paymentType.Value equals appointmentRec.PaymentMethodId
                                select paymentType).Select(aTypeE => aTypeE.Adapt<DataSourceDto>());

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
                if (paymentTypes.Any(aType => aType.Value == insert.PaymentMethodId))
                {
                    insert.PaymentMethod = paymentTypes.FirstOrDefault(x => x.Value == insert.PaymentMethodId);
                }
            });
            var results = base.AddOrUpdate(inserted, currentUserId);
            return results;
        }
        protected override IQueryable<AppointmentEntity> GetQueryable(AppointmentFilter filters)
        {
            var query =  base.GetQueryable(filters);
          
            if (!string.IsNullOrWhiteSpace(filters.CurrentUserId))
            {
                var currentUser = _userManager.Users.FirstOrDefault(u => u.Id == filters.CurrentUserId);
                if(currentUser is IdentityUser validUser && _userManager.IsInRoleAsync(validUser, RoleConstants.Patient).Result)
                {
                 
                    query = query.Where(appointment => appointment.Patient.UserId == filters.CurrentUserId);
                }
               
            }
            if (!string.IsNullOrWhiteSpace(filters.DoctorId))
            {
                query = query.Where(appointment => appointment.Doctor.UserId == filters.DoctorId);
            }

            return query
                .Include(appointment => appointment.Patient)
                .ThenInclude(patient => patient.Address)
                .Include(appointment => appointment.Patient.Contact)
                .Include(appointment => appointment.AppointmentType)
                .Include(appointment => appointment.Doctor)
                .ThenInclude(doc => doc.Title)
                .Include(doc => doc.Doctor.PracticeSite)
                .Include(doc => doc.Doctor.Contact)
                .Include(appointment => appointment.AppointmentStatus)
                .Include(appointment => appointment.PaymentMethod);
              
        }
        public override AppointmentDto? Map(AppointmentEntity? source)
        {
            var appointmentDto = base.Map(source);
            if(source is AppointmentEntity validSource && appointmentDto is AppointmentDto validDto )
            {
                if(validSource.AppointmentType is DataSourceEntity validAppointmentType)
                {
                   validDto.AppointmentType = validAppointmentType.Adapt(validDto.AppointmentType);
                   validDto.AppointmentTypeId = validDto.AppointmentType?.Value ?? default;
                }

                if(validSource.PaymentMethod is DataSourceEntity paymentMethod)
                {
                    validDto.PaymentMethod = paymentMethod.Adapt(validDto.PaymentMethod);
                    validDto.PaymentMethodId = validDto.PaymentMethod?.Value ?? default;
                }

                if(validSource.Doctor is DoctorEntity validDoctorSource)
                {
                    validDto.Doctor = validDoctorSource.Adapt(validDto.Doctor);
                    validDto.DoctorId = validDto.Doctor?.Id ?? default;
                    if (validDoctorSource.Title is DataSourceEntity validTitleSource && validDto.Doctor is DoctorDto validDoctorTarget)
                    {
                        validDoctorTarget.Title = validTitleSource.Adapt(validDoctorTarget.Title);
                    }
                    if(validDoctorSource.Contact is ContactEntity validContactEntity && validDto.Doctor is DoctorDto validTargetDoctot)
                    {
                        validTargetDoctot.Contact = validContactEntity.Adapt(validTargetDoctot.Contact);
                    }

                    if(validDoctorSource.PracticeSite is AddressEntity && validDto is { Doctor : DoctorDto })
                    {
                        validDto.Doctor.PracticeSite = validDoctorSource.PracticeSite.Adapt(validDto.Doctor.PracticeSite);
                    }
                }

                if(validSource.Patient is PatientEntity validPatientSource)
                {
                    validDto.Patient = validPatientSource.Adapt(validDto.Patient);
                    validDto.PatientId = validDto?.Patient?.Id ?? default;
                    if(validPatientSource is { Address : AddressEntity } patientWithAddress && validDto is { Patient : PatientDto})
                    {
                        validDto.Patient.Address = patientWithAddress.Address.Adapt(validDto.Patient.Address);
                    }
                }

                if(validSource.AppointmentStatus is DataSourceEntity validAppointmentStatusSource)
                {
                    validDto.AppointmentStatus = validAppointmentStatusSource.Adapt(validDto.AppointmentStatus);
                }
            }

            return appointmentDto;
        }
        protected override IEnumerable<Guid> Add(List<AppointmentDto> inserted, string? currentUserId)
        {
           var pendingDs = dbContext.DataSource.AsNoTracking().FirstOrDefault(ds =>
            ds.TypeCode == DataSourceTypeCodeConstants.AppointmentStatus
            && ds.Name == "Pending"
            );
            inserted.ForEach(x => x.AppointmentStatus = pendingDs.Adapt(new DataSourceDto()));
            return base.Add(inserted, currentUserId);
        }

        public async Task<AppointmentDto?> Accept(Guid appointmentId)
        {
            var updatedAppointment = this.dbContext.Appointments.AsNoTracking()
                .Where(appointment => appointment.Id == appointmentId)
                .FirstOrDefault();
            if(updatedAppointment is AppointmentEntity validAppointment)
            {
                validAppointment.AppointmentStatus = dbContext.DataSource.AsNoTracking()
                    .Where(x => x.Name == "Accepted" && x.TypeCode == DataSourceTypeCodeConstants.AppointmentStatus)
                    .FirstOrDefault();
                this.dbContext.Update(validAppointment);
                await this.dbContext.SaveChangesAsync();
            }

            return this.Get(new() { Filter = new() { Id = appointmentId } })?.Data?.FirstOrDefault();
        }

        public async Task<AppointmentDto?> Reject(Guid appointmentId)
        {
            var updatedAppointment = this.dbContext.Appointments.AsNoTracking()
                .Where(appointment => appointment.Id == appointmentId)
                .FirstOrDefault();
            if (updatedAppointment is AppointmentEntity validAppointment)
            {
                validAppointment.AppointmentStatus = dbContext.DataSource.AsNoTracking()
                    .Where(x => x.Name == "Rejected" && x.TypeCode == DataSourceTypeCodeConstants.AppointmentStatus)
                    .FirstOrDefault();
                this.dbContext.Update(validAppointment);
                await this.dbContext.SaveChangesAsync();
            }

            return this.Get(new() { Filter = new() { Id = appointmentId } })?.Data?.FirstOrDefault();
        }

        public IEnumerable<(string paymentContent, Guid appointmentId)> AddOrUpdateWithPayment(List<AppointmentDto> appointments, string? currentUserId)
        {
            var updatedAppointmentIdentifiers = this.AddOrUpdate(appointments, currentUserId);
            var updatedAppointments = updatedAppointmentIdentifiers
                .Select(id => this.Get(new() { PageSize = 1, Filter = new() { Id = id } })?.Data?.FirstOrDefault())
                .ToList();
            var results = updatedAppointments?.Select(app => {
                return (_mediatr.Send(new AppointmentPaymentCommand(app)).Result,app.Id);
            });
            return results ?? [];
        }
    }
}
