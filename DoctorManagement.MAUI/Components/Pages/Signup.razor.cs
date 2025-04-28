using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation.Components.Login;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorManagement.Presentation.Components;
namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class Signup
    {
        [Inject]
        public IAppLocationService AppLocationService { get; set; }
        public SignupComponent SComp { get; set; }
        private bool _currentLocationButtonLoading {  get; set; }
        public async Task OnCurrentLocationButtonClicked()
        {
            _currentLocationButtonLoading = true;
            var currentLocation = await AppLocationService.GetAppCurrentLocation();
            if(currentLocation is Location validLocation)
            {
                SComp.ViewModel.Data.Address.Latitude = validLocation.Latitude;
                SComp.ViewModel.Data.Address.Longitude = validLocation.Longitude;
                var placeMark = await AppLocationService.GetReverseGeocodeData(validLocation.Latitude, validLocation.Longitude);
                if(placeMark is Placemark validPlaceMark)
                {
                    SComp.ViewModel.Data.Address.City = placeMark.SubAdminArea;
                    SComp.ViewModel.Data.Address.State = placeMark.AdminArea;
                    SComp.ViewModel.Data.Address.HouseNumber = placeMark.SubLocality;
                    SComp.ViewModel.Data.Address.StreetName  = placeMark.Thoroughfare;
                    SComp.ViewModel.Data.Address.Zip = placeMark.PostalCode;
                   
                }
            }
            _currentLocationButtonLoading = false;
        }
    }
}
