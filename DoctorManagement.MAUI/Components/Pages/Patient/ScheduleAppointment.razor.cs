using DoctorManagement.Api.Consumer;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.Components.Templates.Email;
using DoctorManagement.Presentation.Services.Interfaces;
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
       [Inject]
        public IMailTemplateMessageHandler MailTemplateMessageHandler { get; set; }
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
            var paymentMethodOptions = await DataSourceApiConsumer
              .Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.PaymentMethod } });

            ViewModel.AppointmentTypeList = appointmentTypeOptions?.Data ?? [];
            ViewModel.PaymentMethodOptions = paymentMethodOptions?.Data ?? [];
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
                PaymentMethodId = appointment.PaymentMethodId,
            });
            var message = results?.Data?.Any() ?? false ? "Appointment request created." : "Failed to create appointment request.";
            this.AppointmentSubmissionResponse  = (results?.Data?.Any() ?? false, message);
            if (results?.Data?.FirstOrDefault() is Guid validGuid && validGuid != Guid.Empty)
            {
                var newlyCreatedAppointmentData = await this.AppointmentApiConsumer.Get(new() { Filter = new() { Id = validGuid } });
                if(newlyCreatedAppointmentData?.Data?.FirstOrDefault() is AppointmentDto newAppointment 
                    && newAppointment.Doctor is { Contact : ContactDto})
                {
                   await this.MailTemplateMessageHandler.SendTemplateMessageAsync(
                        newAppointment.Doctor.Contact.Email,
                        "New Appointment Request",
                        typeof(AppointmentRequestTemplate),
                        new Dictionary<string, object?> {
                         {"ViewModel", new Presentation.ViewModels.Templates.Email.AppointmentRequestTemplateViewModel(){
                           Data = newAppointment
                         } }
                        }
                      );
                }
            }
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
