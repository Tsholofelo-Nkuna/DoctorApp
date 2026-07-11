using Mapster;
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
using MapsterMapper;

namespace DoctorManagement.BusinessLogicLayer.Services.Base
{
    public class ServiceBase<TDto, TEntity, TFilter> : BaseMapper<TEntity, TDto>, IBaseMapper<TEntity, TDto>, IServiceBase<TDto, TEntity, TFilter> where TDto : DtoBase, new() where TEntity : EntityBase where TFilter : BaseFilter, new()
    {
        protected readonly WebDbContext dbContext;
        private readonly DbSet<TEntity> _entitySet;
       // [Obsolete(@"Use this.Map and the object.Adapt methods")]
        protected readonly IMapper mapper;
        public ServiceBase(WebDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._entitySet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }

        public virtual IEnumerable<Guid> AddOrUpdate(List<TDto> records, string? currentUserId)
        {
           var newRecords = records.Where(x => x.Id == Guid.Empty).ToList();
           var updatedRecords = records.Where(x => x.Id != Guid.Empty).ToList();
           var updated = this.Update(updatedRecords, currentUserId);
           var inserted = this.Add(newRecords, currentUserId);
          // var affectedRecords = updated.Concat(inserted);
           return (updated ?? []).Concat(inserted ?? []);
        }

        protected virtual IEnumerable<Guid> Add(List<TDto> inserted, string? currentUserId)
        {
            try
            {
                var insertedEntities = inserted.Adapt<List<TEntity>>();
                var currentDateTime = DateTime.Now;
                inserted.ForEach(x =>
                {
                    x.CreatedByUserId = currentUserId;
                    x.CreatedOn = currentDateTime;
                });
                _entitySet.UpdateRange(insertedEntities);
                return dbContext.SaveChanges() > 0 ? insertedEntities.Select(rec => rec.Id) : [];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected virtual IEnumerable<Guid> Update(List<TDto> updated, string? currentUserId)
        {
            try
            {
                var updates = updated.Adapt<List<TEntity>>();
                var currentDateTime = DateTime.Now;
                updates.ForEach(x =>
                {
                    x.LastModifiedOn = currentDateTime;
                    x.LastModifiedByUserId = currentUserId;
                });
                _entitySet.UpdateRange(updates);
                return dbContext.SaveChanges() > 0 ? updates.Select(up => up.Id) : [];
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
            try
            {

                var query = this.GetQueryable(pageRequest.Filter);
                var totalRecordCount = query.Count();
                if (!pageRequest.GetAllPages)
                {
                    query = query
                        .Skip((pageRequest.PageIndex - 1) * pageRequest.PageSize)
                        .Take(pageRequest.PageSize);
                }
                else
                {

                }
                var results = query?.ToList()?.Select( x => this.Map(x));

                return new()
                {
                    PageIndex = pageRequest.PageIndex,
                    PageSize = pageRequest.PageSize,
                    Data = results,
                    TotalRecordCount = totalRecordCount,
                };
            }
            catch (Exception ex)
            {

                throw;
            }
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
