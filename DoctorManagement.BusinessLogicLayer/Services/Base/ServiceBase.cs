using AutoMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services.Base
{
    public class ServiceBase<TDto, TEntity, TFilter> : IServiceBase<TDto, TEntity, TFilter> where TDto : DtoBase, new() where TEntity : EntityBase where TFilter : BaseFilter, new()
    {
        protected readonly WebDbContext dbContext;
        private readonly DbSet<TEntity> _entitySet;
        protected readonly IMapper mapper;
        public ServiceBase(WebDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._entitySet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }

        public virtual bool AddOrUpdate(List<TDto> records, string? currentUserId)
        {
           var newRecords = records.Where(x => x.Id == Guid.Empty).ToList();
           var updatedRecords = records.Where(x => x.Id != Guid.Empty).ToList();
           var updated = updatedRecords.Any() ? this.Update(newRecords, currentUserId) : true;
           var inserted = newRecords.Any() ? this.Add(newRecords, currentUserId) : true;
           return inserted && updated;
        }

        private  bool Add(List<TDto> inserted, string? currentUserId)
        {
            try
            {
                var insertedEntities = this.mapper.Map<List<TEntity>>(inserted);
                var currentDateTime = DateTime.Now;
                inserted.ForEach(x =>
                {
                    x.CreatedByUserId = currentUserId;
                    x.CreatedOn = currentDateTime;
                });
                _entitySet.UpdateRange(insertedEntities);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private  bool Update(List<TDto> updated, string? currentUserId)
        {
            try
            {
                var updates = this.mapper.Map<List<TEntity>>(updated);
                var currentDateTime = DateTime.Now;
                updates.ForEach(x =>
                {
                    x.LastModifiedOn = currentDateTime;
                    x.LastModifiedByUserId = currentUserId;
                });
                _entitySet.UpdateRange(updates);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public virtual bool Delete(IEnumerable<Guid> identifiers, string? currentUserId)
        {
            
            var removed = this._entitySet.Where(x => identifiers.Contains(x.Id)).ToList();
            _entitySet.RemoveRange(removed);
            return removed.Any() ? dbContext.SaveChanges() > 0 : false;
        }

        public PageResponse<TDto> Get(PageRequestDto<TFilter> pageRequest)
        {
            var query = this.GetQueryable(pageRequest.Filter);
            var totalRecordCount = query.Count();
            if (!pageRequest.GetAllPages)
            {
                query =  query
                    .Skip((pageRequest.PageIndex - 1)*pageRequest.PageSize)
                    .Take(pageRequest.PageSize);
            }
            else
            {

            }
            var results = query.ToList();

            return new()
            {
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
                Data = this.mapper.Map<List<TDto>>(results),
                TotalRecordCount = totalRecordCount,
            };
        }
      
        protected virtual  IQueryable<TEntity> GetQueryable(TFilter filters)
        {
            var query = this._entitySet.AsNoTracking();
            if(filters.Id != Guid.Empty)
            {
                query = query.Where(x => x.Id == filters.Id);
            }
            return query;
        }
    }
}
