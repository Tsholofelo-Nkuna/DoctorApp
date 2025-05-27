using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class DoctorListViewModel : BaseViewModel<List<DoctorDto>> 
    {
        public ModalViewModel<DoctorFilter> DocSearchModalViewModel { get; set; } = new();
        public IEnumerable<DataSourceDto> DoctorSpecialtyList { get; set; } = [];
    }
}
