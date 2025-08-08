using DoctorManagement.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class FormViewModel<TDto> : BaseViewModel<TDto> where TDto : new()
    {
        public List<FormInputViewModel<TDto>> InputViewModels { get; set; } = [];
        public int InputsPerRowCount { get; set; } = 1;
        public bool ShowSubmitButtons { get; set; } = true;
    }
}
