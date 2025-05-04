using DoctorManagement.Api.Consumer;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages.Patient
{
    public partial class ScheduleAppointment
    {
        [Inject]
        public IDataSourceApiConsumer DataSourceApiConsumer { get; set; }
        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }
        [Inject]
        public IAppointmentApiConsumer AppointmentApiConsumer { get; set; }
        [Parameter]
         public Guid DoctorId { get; set; } 
        public (bool Success, string Message ) AppointmentSubmissionResponse { get; set; } = (false, string.Empty);
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            DataSourceApiConsumer.Unauthorized += this.OnUnauthorized;
            DoctorApiConsumer.Unauthorized += this.OnUnauthorized;
            AppointmentApiConsumer.Unauthorized += this.OnUnauthorized;
            var accessToken = (await SecureStorage.Default.GetAsync(LocalStorageKeys.BearerToken)) ?? string.Empty;
            DataSourceApiConsumer.AccessToken =  accessToken;
            DoctorApiConsumer.AccessToken = accessToken;
            AppointmentApiConsumer.AccessToken = accessToken;
            var appointmentTypeOptions = await DataSourceApiConsumer
                .Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.AppointmentType } });
            ViewModel.AppointmentTypeList = appointmentTypeOptions?.Data ?? [];
            ViewModel.Data.DoctorId = DoctorId;
            ViewModel.Data.Doctor = (await DoctorApiConsumer.Get(new() { Filter = new() { Id = DoctorId} }))?.Data?.FirstOrDefault() ?? new();
           
        }

        public async Task OnSubmitAppointment(AppointmentDto appointment)
        {
            ViewModel.AppointmentSubmissionInProgress = true;
            var results = await this.AppointmentApiConsumer.Add(new()
            {
                DoctorId = appointment.DoctorId,
                ScheduledDate = appointment.ScheduledDate,
                AppointmentTypeId = appointment.AppointmentTypeId,
            });
            var message = results?.Data ?? false ? "Appointment request created." : "Failed to create appointment request.";
            this.AppointmentSubmissionResponse  = (results?.Data ?? false, message);
            ViewModel.AppointmentSubmissionInProgress = false;
           
        }
        ~ ScheduleAppointment() { 
           if(DataSourceApiConsumer is not null || true)
            {
                DataSourceApiConsumer.Unauthorized -= this.OnUnauthorized;
                DoctorApiConsumer.Unauthorized -= this.OnUnauthorized;
                AppointmentApiConsumer.Unauthorized -= this.OnUnauthorized;
            }
        }
    }
}
