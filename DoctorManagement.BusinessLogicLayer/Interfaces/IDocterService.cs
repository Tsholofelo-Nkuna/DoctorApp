using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Interfaces
{
    public interface IDocterService : IServiceBase<DoctorDto, DoctorEntity,DoctorFilter>
    {
        public Task<ResponseDto<DoctorSettingsDto>> UpdateDoctorSettingsForCurrentUser(DoctorSettingsDto settings);
    }
}
