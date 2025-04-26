
using DoctorManagement.MAUI.Services;
using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class Doctors
    {
        [Inject]
        public IAppLocationService LocationService { get; set; }
        public Location? AppLocation { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var location = await LocationService.GetAppCurrentLocation();
            this.AppLocation = location;
            
        }

    }
}
