using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Doctors
{
   
    public partial class DoctorSettingsComponent
    {
        [Parameter]
        public EventCallback<DoctorSettingsDto> SettingsFormSubmitClicked { get; set; }
        public async Task OnSumbmitSettings()
        {
            var settings = ViewModel.DoctorSettingsFormViewModel.Data;
            await SettingsFormSubmitClicked.InvokeAsync(settings);
            
        }
    }
}
