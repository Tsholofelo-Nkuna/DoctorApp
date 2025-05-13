using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Patients
{
    public partial class AppointmentListComponent
    {
        public string BadgeColor(AppointmentDto appointment) => appointment switch {
            { AppointmentStatus.Name : AppointmentStatusConstants.Pending } => "bg-warning-subtle text-warning",
            { AppointmentStatus.Name: AppointmentStatusConstants.Accepted } => "bg-success-subtle text-success",
            { AppointmentStatus.Name: AppointmentStatusConstants.Rejected } =>"bg-danger-subtle text-danger",
            { AppointmentStatus.Name: AppointmentStatusConstants.Cancelled } => "bg-secondary-subtle text-secondary",
            _ => "bg-info"
        };
       
    }

   
}
