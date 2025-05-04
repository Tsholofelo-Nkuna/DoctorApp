using AutoMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
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
        public DoctorService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
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

            if (!string.IsNullOrEmpty(filters.Specialty))
            {
                query = query.Where(docRec => docRec.Specialty.Contains(filters.Specialty));
            }

            return  query.Include(x => x.Contact)
                .Include(x => x.Title)
                .Include(x => x.PracticeSite);
        }

        public override bool AddOrUpdate(List<DoctorDto> records, string? currentUserId)
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
            return doctor;  
        }

    }
}
