using DoctorManagement.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{
    public class AppointmentEntity : EntityBase
    {
        public DateTime? ScheduledDate { get; set; }
        public DataSourceEntity? AppointmentType { get; set; }
        public DoctorEntity Doctor { get; set; }
        public PatientEntity Patient { get; set; }
        
    }
}
