using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Interfaces
{
    public interface IDataSourceService : IServiceBase<DataSourceDto, DataSourceEntity, DataSourceFilter >
    {
    }
}
