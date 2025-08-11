using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages.Doctor
{
    public partial class DoctorSettingsPage
    {
        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }

        public List<IUnauthorizedApiCallHandler> ApiConsumers { get; set; } = [];
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ApiConsumers = [
                    DoctorApiConsumer,
                ];
            await InitializeApiConsumers(ApiConsumers);
            
        }

        public async Task OnSettingsFormSubmitClicked(DoctorSettingsDto settings)
        {
            await DoctorApiConsumer.UpdateDoctorSettingsForCurrentUser(settings);
        }

        ~DoctorSettingsPage()
        {
            ReleaseApiConsumers(ApiConsumers);
        }
    }
}
