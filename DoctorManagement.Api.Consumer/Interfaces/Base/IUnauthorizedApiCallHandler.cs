using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces.Base
{
    public interface IUnauthorizedApiCallHandler
    {
        public event EventHandler<ResponseDto<bool>> Unauthorized;
        public string AccessToken { get; set; }
    }
}
