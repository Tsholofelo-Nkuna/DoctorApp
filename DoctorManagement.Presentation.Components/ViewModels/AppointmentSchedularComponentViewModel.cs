using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class AppointmentSchedularComponentViewModel : BaseViewModel<AppointmentDto>
    {
        public IEnumerable<DataSourceDto> AppointmentTypeList { get; set; } = [];
        public bool AppointmentSubmissionInProgress { get; set; }
    }
}
