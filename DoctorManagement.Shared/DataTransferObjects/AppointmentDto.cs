using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class AppointmentDto: DtoBase
    {
        [Required]
        public DateTime? ScheduledDate { get; set; }
        public DataSourceDto? AppointmentType { get; set; }
        public DoctorDto Doctor { get; set; }
        public PatientDto Patient { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int AppointmentTypeId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
