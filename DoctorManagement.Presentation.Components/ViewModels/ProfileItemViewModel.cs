using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class ProfileItemViewModel : BaseViewModel<DtoBase>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool EnableContextMenu { get; set; }
    }
}
