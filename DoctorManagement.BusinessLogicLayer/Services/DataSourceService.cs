using MapsterMapper;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Services.Base;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class DataSourceService : ServiceBase<DataSourceDto, DataSourceEntity, DataSourceFilter>, IDataSourceService
    {
        public DataSourceService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override IQueryable<DataSourceEntity> GetQueryable(DataSourceFilter filters)
        {
            var query = base.GetQueryable(filters);
            if (!string.IsNullOrEmpty(filters.TypeCode))
            {
                query = query.Where(ds => ds.TypeCode == filters.TypeCode);
            }
            return query;
        }
    }
}
