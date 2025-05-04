using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Patients
{
    public partial class AppointmentSchedularComponent
    {
        [Parameter]
        public EventCallback<AppointmentDto> OnValidSumit { get; set; }
        public async Task OnAppointmentFormSubmit()
        {
            if (this.ViewModel.EditContext.Validate())
            {
                await OnValidSumit.InvokeAsync(ViewModel.Data);
            }
        }

        public string CurrentAppointmentType =>
            this.ViewModel
            .AppointmentTypeList
            .FirstOrDefault(x => x.Value == ViewModel.Data.AppointmentTypeId)?.Description
            ?? string.Empty;
    }
}
