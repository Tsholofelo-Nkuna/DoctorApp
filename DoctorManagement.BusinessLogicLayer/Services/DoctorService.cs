using MapsterMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class DoctorService : ServiceBase<DoctorDto, DoctorEntity, DoctorFilter>, IDocterService
    {
        private readonly IUserContextService _userContextService;
        public DoctorService(WebDbContext dbContext, IMapper mapper, IUserContextService userContextService) : base(dbContext, mapper)
        {
            _userContextService = userContextService;
        }

        protected override IQueryable<DoctorEntity> GetQueryable(DoctorFilter filters)
        {
            var address = $"{filters.City}, {filters.State} {filters.Zip}";
            var query = base.GetQueryable(filters);

            if (!string.IsNullOrEmpty(filters.City))
            {
                query = query.Where(docRec => docRec.PracticeSite.City.Contains(filters.City));
            }

            if (!string.IsNullOrEmpty(filters.State))
            {
                query = query.Where(doc => doc.PracticeSite.State.Contains(filters.State));
            }

            if (!string.IsNullOrEmpty(filters.Zip))
            {
                query = query.Where(doc => doc.PracticeSite.Zip == (filters.Zip));
            }

            if (filters.SpecialtyValue > 0)
            {
                query = query.Where(docRec => docRec.Specialty.Value == filters.SpecialtyValue);
            }

            if (!string.IsNullOrWhiteSpace(filters.DoctorUserId))
            {
                query = query.Where(doc => doc.UserId == filters.DoctorUserId);
            }

            return  query.Include(x => x.Contact)
                .Include(x => x.Title)
                .Include(x => x.PracticeSite)
                .Include(x => x.Specialty);
        }

        public override IEnumerable<Guid> AddOrUpdate(List<DoctorDto> records, string? currentUserId)
        {
            records.ForEach(rec =>
            {
                rec.Title = this.mapper.Map<DataSourceDto>(this.dbContext.DataSource.AsNoTracking().FirstOrDefault(ds => ds.Value == rec.TitleDatasourceId));
                
            });
            return base.AddOrUpdate(records, currentUserId);
        }

        public override DoctorDto? Map(DoctorEntity? source)
        {
            var doctor =  base.Map(source);
            if(source is { PracticeSite : AddressEntity } && doctor is not null)
            {
               doctor.PracticeSite = source.PracticeSite.CopyTo(doctor.PracticeSite);
            }

            if(source is { Contact : ContactEntity } && doctor is not null)
            {
                doctor.Contact = source.Contact.CopyTo(doctor.Contact);
            }
            if(source is { Title : DataSourceEntity } && doctor is not null)
            {
                doctor.Title = source.Title.CopyTo(doctor.Title);  
            }
            if(source is { Specialty : DataSourceEntity } && doctor is not null)
            {
                doctor.Specialty = source.Specialty.CopyTo(doctor.Specialty);
            }
            return doctor;  
        }

        public async Task<ResponseDto<DoctorSettingsDto>> UpdateDoctorSettingsForCurrentUser(DoctorSettingsDto settings)
        {
          
           
            var roles = await _userContextService.GetCurrentUserRoles();
            var currrentUser = await _userContextService.GetCurrentUserAsync();
            if((roles?.Contains(Shared.Constants.RoleConstants.Doctor) ?? false) && currrentUser is IdentityUser validCurrentUser)
            {
               var pageResponse = this.Get(new()
                {
                    Filter = new()
                    {
                        DoctorUserId = currrentUser?.Id ?? string.Empty
                    },
                    PageSize = 1
                });
                if((pageResponse?.Data?.Count() ?? 0) > 0 && pageResponse!.Data!.FirstOrDefault() is DoctorDto validDoctorDto)
                {
                   validDoctorDto.ConsultationFee = settings.ConsultationFee;
                   validDoctorDto.AcceptHomeVisits = settings.AcceptHomeVisits;
                   var results = Update([validDoctorDto], validCurrentUser.Id);
                   var updatedDoctorId = results.FirstOrDefault();
                    var doctorRec =  Get(new() {
                        Filter = new() { DoctorUserId = validCurrentUser.Id },
                        PageSize = 1
                    })?.Data?.FirstOrDefault();
                    return new()
                    {
                        Data = new() { 
                            ConsultationFee = doctorRec?.ConsultationFee ?? 0,
                            AcceptHomeVisits = doctorRec?.AcceptHomeVisits ?? false,
                            
                        },
                    };
                   
                }
                else
                {
                    return new();
                }
                    
            }
            else
            {
                return new();
            }
        }
    }
}
