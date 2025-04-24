using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces
{
    public interface IIdentityApiConsumer : IApiConsumerBase<SignupDto, BaseFilter>
    {
        public Task<ResponseDto<bool>?> PatientSignup(SignupDto patientSignUp);
        public Task<ResponseDto<bool>?> DoctorSignup(SignupDoctorDto patientSignUp);
        public Task<ResponseDto<bool>?> Login(string userName, string password);
    }
}
