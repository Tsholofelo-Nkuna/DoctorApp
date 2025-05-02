using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Doctors
{
    public partial class AppointmentComponent
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
    }
}
