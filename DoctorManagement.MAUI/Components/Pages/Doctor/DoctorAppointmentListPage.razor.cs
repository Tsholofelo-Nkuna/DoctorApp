using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages.Doctor
{
    public partial class DoctorAppointmentListPage
    {
        [Inject]
        public IAppointmentApiConsumer AppointmentApiConsumer { get; set; }
        public List<IUnauthorizedApiCallHandler> ApiConsumers { get; set; } = [];
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ApiConsumers = [
                  AppointmentApiConsumer
                ];
            await InitializeApiConsumers(ApiConsumers);
            var currentUserId = (await AppointmentApiConsumer.GetUserId())?.Data;
            var appointmentListRequest = new PageRequestDto<AppointmentFilter>
            {
                GetAllPages = true,
                Filter = new()
                {
                    DoctorId = currentUserId ?? string.Empty,
                }
            };
           var doctorAppointmentsPageList =   await AppointmentApiConsumer.Get(appointmentListRequest);
           ViewModel.Data = doctorAppointmentsPageList?.Data?.ToList() ?? [];
        }

        ~DoctorAppointmentListPage() {
            ReleaseApiConsumers(ApiConsumers);
        }
    }
}
