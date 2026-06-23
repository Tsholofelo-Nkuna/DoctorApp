using DoctorManagement.MAUI.Helpers;
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
        private bool _currentLocationButtonLoading = false;
        public async Task OnUseCurrentLocationClicked()
        {
            _currentLocationButtonLoading = true;
            var currentLocation = await this.AppLocationService.GetAppCurrentLocation();
            if(currentLocation is Location validLocation)
            {
                DocSignupComponent.ViewModel.Data.PracticeSite.Latitude = validLocation.Latitude;
                DocSignupComponent.ViewModel.Data.PracticeSite.Longitude = validLocation.Longitude;
                var marker = await this.AppLocationService.GetReverseGeocodeData(validLocation.Latitude, validLocation.Longitude);
                if (marker is Placemark validPlacemark)
                {
                    DocSignupComponent.ViewModel.Data.PracticeSite.City = marker.SubAdminArea;
                    DocSignupComponent.ViewModel.Data.PracticeSite.State = marker.AdminArea;
                    DocSignupComponent.ViewModel.Data.PracticeSite.Zip = marker.PostalCode;
                    DocSignupComponent.ViewModel.Data.PracticeSite.HouseNumber = marker.SubLocality;
                    DocSignupComponent.ViewModel.Data.PracticeSite.StreetName = marker.Thoroughfare;

                }
            }

            _currentLocationButtonLoading = false;
           
        }

        public async Task OnTakePhotoClicked() { 
          (string photoFileName, string photoMimeType, byte[] photoContents) = await  MediaHelper.TakePhoto();
          DocSignupComponent.ViewModel.Data.PhotoFileName = photoFileName;
          DocSignupComponent.ViewModel.Data.PhotoMimeType = photoMimeType;
          DocSignupComponent.ViewModel.Data.PhotoContents = photoContents;
          
        }


    }
}
