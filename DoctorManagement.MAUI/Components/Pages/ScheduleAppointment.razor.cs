using DoctorManagement.Api.Consumer;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class ScheduleAppointment
    {
        [Inject]
        public IDataSourceApiConsumer DataSourceApiConsumer { get; set; }
        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }
        [Parameter]
         public Guid DoctorId { get; set; } 
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            DataSourceApiConsumer.Unauthorized += this.OnUnauthorized;
            DoctorApiConsumer.Unauthorized += this.OnUnauthorized;
            var accessToken = (await SecureStorage.Default.GetAsync(LocalStorageKeys.BearerToken)) ?? string.Empty;
            DataSourceApiConsumer.AccessToken =  accessToken;
            DoctorApiConsumer.AccessToken = accessToken;
           
            var appointmentTypeOptions = await DataSourceApiConsumer
                .Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.AppointmentType } }, "DataSource");
            ViewModel.AppointmentTypeList = appointmentTypeOptions?.Data ?? [];
            ViewModel.Data.DoctorId = DoctorId;
            ViewModel.Data.Doctor = (await DoctorApiConsumer.Get(new() { Filter = new() { Id = DoctorId} }, "Doctors"))?.Data?.FirstOrDefault() ?? new();
        }
        ~ ScheduleAppointment() { 
           if(DataSourceApiConsumer is not null)
            {
                DataSourceApiConsumer.Unauthorized -= this.OnUnauthorized;
            }
        }
    }
}
