using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation.Components.Login;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class DoctorSignup
    {
        [Inject]
        public IAppLocationService AppLocationService { get; set; }
        public SignupDoctorComponent DocSignupComponent { get; set; }
        public async Task OnUseCurrentLocationClicked()
        {
            var currentLocation = await this.AppLocationService.GetAppCurrentLocation();
            var marker = await this.AppLocationService.GetReverseGeocodeData(currentLocation?.Latitude ?? 0, currentLocation?.Longitude ?? 0);
            if(marker is Placemark validPlacemark)
            {
                DocSignupComponent.ViewModel.Data.PracticeSite.City = marker.SubAdminArea;
                DocSignupComponent.ViewModel.Data.PracticeSite.State = marker.AdminArea;
                DocSignupComponent.ViewModel.Data.PracticeSite.Zip = marker.PostalCode;
                DocSignupComponent.ViewModel.Data.PracticeSite.HouseNumber = marker.SubLocality;
            }
        }
    }
}
