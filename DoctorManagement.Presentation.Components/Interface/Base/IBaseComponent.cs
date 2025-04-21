using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Interface.Base
{
    public interface IBaseComponent<TViewModel, TDto> where TDto: new() where TViewModel : IBaseViewModel<TDto>, new()
    {
        public TViewModel ViewModel { get; set; } 
    }
}
