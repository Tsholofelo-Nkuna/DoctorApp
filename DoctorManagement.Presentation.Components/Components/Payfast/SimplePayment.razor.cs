using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Payfast
{
    public partial class SimplePayment
    {
        [Parameter]
        public AppointmentDto? Appointment { get; set; }
    }
}
