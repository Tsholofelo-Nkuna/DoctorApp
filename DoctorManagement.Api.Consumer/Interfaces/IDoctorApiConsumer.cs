using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces
{
    public interface IDoctorApiConsumer : IApiConsumerBase<DoctorDto, DoctorFilter>
    {
        public Task<ResponseDto<DoctorSettingsDto>> UpdateDoctorSettingsForCurrentUser(DoctorSettingsDto settings);
    }
}
