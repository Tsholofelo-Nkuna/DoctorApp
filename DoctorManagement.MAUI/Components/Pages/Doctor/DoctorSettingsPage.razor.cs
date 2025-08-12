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
            var currentUserId = (await DoctorApiConsumer.GetUserId())?.Data;
            if(currentUserId is string validUserId)
            {
                var doctorCollectionResult = (await DoctorApiConsumer.Get(new()
                {
                    Filter = new()
                    {
                        DoctorUserId = validUserId
                    },
                    PageSize = 1
                }))?.Data;

                if(doctorCollectionResult?.FirstOrDefault() is DoctorDto validDoctorDto)
                {
                    ViewModel.DoctorSettingsFormViewModel.Data = new()
                    {
                        ConsultationFee = validDoctorDto.ConsultationFee,
                    };
                }
            }
        }

        public async Task OnSettingsFormSubmitClicked(DoctorSettingsDto settings)
        {
          var apiResponse =  await DoctorApiConsumer.UpdateDoctorSettingsForCurrentUser(settings);
          ViewModel.Data = apiResponse?.Data ?? new();
        }

        ~DoctorSettingsPage()
        {
            ReleaseApiConsumers(ApiConsumers);
        }
    }
}
