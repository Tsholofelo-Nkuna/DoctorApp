using DoctorManagement.Shared.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Integrations.PayFast.Commands
{
    public class AppointmentPaymentCommand(AppointmentDto appointment) : IRequest<string>
    {
        public AppointmentDto Appointment => appointment;
    }
}
