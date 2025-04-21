using DoctorManagement.Shared.DataTransferObjects.Base;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Interface.Base
{
    public interface IBaseViewModel<TDto> where TDto : new()
    {
        public TDto Data { get; set; }
        public EditContext? EditContext { get; }
    }
}
