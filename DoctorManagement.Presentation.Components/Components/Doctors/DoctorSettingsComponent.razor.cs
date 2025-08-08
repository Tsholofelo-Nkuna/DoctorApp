using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Doctors
{
    public partial class DoctorSettingsComponent
    {
        public Task OnSumbmitSettings()
        {
            var settings = ViewModel.DoctorSettingsFormViewModel.Data;
            return Task.CompletedTask;
        }
    }
}
