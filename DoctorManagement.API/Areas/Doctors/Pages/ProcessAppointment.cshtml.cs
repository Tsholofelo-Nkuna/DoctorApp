using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorManagement.API.Areas.Doctors.Pages
{
    public class ProcessAppointmentModel : PageModel
    {
        public AppointmentDto? Appointment { get; set; }
        public IAppointmentApiConsumer AppointmentApiConsumer { get; set; }

        public string AppointmentStatusClass(string statusName) => statusName switch
        { 
          "Pending" => "warning",
          "Accepted" => "success",
          "Rejected" => "danger",
           _ => "secondary"
        };
            
    
        public ProcessAppointmentModel(IAppointmentApiConsumer appointmentApiConsumer) { 
            this.AppointmentApiConsumer = appointmentApiConsumer;
        }
        public async Task OnGetAsync(Guid appointmentId)
        {
            this.Appointment = (await this.AppointmentApiConsumer.Get(new()
            {
                Filter = new() { Id = appointmentId },
            }))?.Data?.FirstOrDefault();
        }


        public async Task OnGetAccept(Guid appointmentId)
        {
           var response =  await this.AppointmentApiConsumer.Accept(appointmentId);
            this.Appointment = response?.Data;
           
        }

        public async Task OnGetReject(Guid appointmentId)
        {
            this.Appointment = (await this.AppointmentApiConsumer.Reject(appointmentId))?.Data;
        }
    }
}
