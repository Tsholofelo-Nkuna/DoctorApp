using DoctorManagement.DataAccessLayer.Entities.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Interfaces.Base
{
    public interface IServiceBase<TDto, TEntity, TFilter> where TDto: DtoBase, new() where TEntity : EntityBase where TFilter: BaseFilter, new()
    {
        public bool Delete(IEnumerable<Guid> identifiers, string? currentUserId);
        public bool AddOrUpdate(List<TDto> records, string? currentUserId);
        public PageResponse<TDto> Get(PageRequestDto<TFilter> pageRequest);
      
    }
}
