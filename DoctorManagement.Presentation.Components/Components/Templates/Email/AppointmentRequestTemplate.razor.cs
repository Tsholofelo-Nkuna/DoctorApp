using DoctorManagement.Api.Consumer;
using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Templates.Email;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace DoctorManagement.Presentation.Components.Templates.Email
{
    public partial class AppointmentRequestTemplate 
    {
        [Parameter]
        public AppointmentRequestTemplateViewModel ViewModel { get; set; } = new();
        [Inject]
        public IOptions<ApiOptions> ApiOptions { get; set; }
    }
}
