using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class DoctorSettingsComponentViewModel : BaseViewModel<DoctorSettingsDto>
    {
        public FormViewModel<DoctorSettingsDto> DoctorSettingsFormViewModel { get; set; } = new()
        {
            InputsPerRowCount = 1,
            InputViewModels = [
                 new(){
                     BoundPropertyName = nameof(DoctorSettingsDto.ConsultationFee),
                     InputTextType = Shared.Constants.FormInputTextTypes.Number,
                 }
             ],
        };
    }
}
