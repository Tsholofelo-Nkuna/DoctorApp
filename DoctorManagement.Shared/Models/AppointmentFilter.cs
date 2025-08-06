using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.Models
{
    public class AppointmentFilter: BaseFilter
    {
        /// <summary>
        /// PatientId
        /// </summary>
        public string CurrentUserId { get; set; }  = string.Empty;
        public string DoctorId { get; set; } = string.Empty ;
    }
}
