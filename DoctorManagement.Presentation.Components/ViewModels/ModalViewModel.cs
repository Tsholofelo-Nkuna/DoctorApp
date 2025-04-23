using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class ModalViewModel<TDto>: BaseViewModel<TDto> where TDto: new()
    {
        public bool Show {  get; set; }
    }
}
