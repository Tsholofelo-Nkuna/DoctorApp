using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Doctors
{
 
    public partial class DoctorAppointmentListComponent
    {
        [Inject]
        public IAppointmentApiConsumer AppointmentApiConsumer { get; set; }

        public async Task OnRejectAppointment(Guid id)
        {
           var response =  await AppointmentApiConsumer.Reject(id);
           if(response is { Data: AppointmentDto } appointmentData)
            {
              var index =  ViewModel.Data.FindIndex(0, ViewModel.Data.Count, ele => ele.Id == id);
              if(index >= 0)
                {
                    ViewModel.Data[index] = appointmentData.Data;
                }
            }
        }

        public async Task OnAcceptAppointment(Guid id)
        {
            var response = await AppointmentApiConsumer.Accept(id);
            if (response is { Data: AppointmentDto } appointmentData)
            {
                var index = ViewModel.Data.FindIndex(0, ViewModel.Data.Count, ele => ele.Id == id);
                if (index >= 0)
                {
                    ViewModel.Data[index] = appointmentData.Data;
                }
            }
        }
    }
}
