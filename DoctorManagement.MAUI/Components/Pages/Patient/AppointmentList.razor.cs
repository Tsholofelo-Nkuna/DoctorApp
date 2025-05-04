using DoctorManagement.Api.Consumer.Interfaces;
using Microsoft.AspNetCore.Components;


namespace DoctorManagement.MAUI.Components.Pages.Patient
{
    public partial class AppointmentList
    {
        [Inject]
        public required IAppointmentApiConsumer AppointmentApiConsumer { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.InitializeApiConsumers([
                  this.AppointmentApiConsumer,
            ]);
           ViewModel.Data =  (await this.AppointmentApiConsumer.Get(new() { GetAllPages = true }))?.Data?.ToList() ?? [];
        }

        ~AppointmentList() {
            this.ReleaseApiConsumers(
                [
                  this.AppointmentApiConsumer
                ]);
        }
    }
}
