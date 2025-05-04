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
        public string CurrentUserId { get; set; }  = string.Empty;
    }
}
