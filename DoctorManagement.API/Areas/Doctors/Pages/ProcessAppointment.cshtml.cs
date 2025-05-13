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
        public IAppointmentService AppointmentService { get; set; }
        public ProcessAppointmentModel(IAppointmentService appointmentService) { 
            this.AppointmentService = appointmentService;
        }
        public async Task OnGetAsync(Guid appointmentId)
        {
            this.Appointment = this.AppointmentService.Get(new()
            {
                Filter = new() { Id = appointmentId },
            })?.Data?.FirstOrDefault();
        }
    }
}
