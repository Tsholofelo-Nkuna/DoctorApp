using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces
{
    public interface IAppointmentApiConsumer : IApiConsumerBase<AppointmentDto, AppointmentFilter>
    {
        public Task<ResponseDto<AppointmentDto?>?> Accept(Guid appointmentId);
        public Task<ResponseDto<AppointmentDto?>?> Reject(Guid appointmentId);
        public Task<ResponseDto<IEnumerable<(string paymentContent, Guid appointmentId)>>?> AddAppointmentWithPayment(AppointmentDto rec);

    }
}
