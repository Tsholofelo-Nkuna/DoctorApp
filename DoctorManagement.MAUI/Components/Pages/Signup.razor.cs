using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation.Components.Login;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorManagement.Presentation.Components;
using Microsoft.JSInterop;
namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class Signup
    {
        [Inject]
        public IAppLocationService AppLocationService { get; set; }
        public SignupComponent SComp { get; set; }
        private bool _currentLocationButtonLoading {  get; set; }
        [Inject]
        public IJSRuntime JsRuntime {  get; set; }
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

        public async Task OnTakePhotoClicked()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Launches the native OS camera UI
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Save or process the local file path
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream sourceStream = await photo.OpenReadAsync();
                    var photoBytes = new byte[sourceStream.Length];
                    sourceStream.Read(photoBytes, 0, photoBytes.Length);
                    SComp.ViewModel.Data.PhotoContents = photoBytes;
                    SComp.ViewModel.Data.PhotoFileName = photo.FileName;
                    SComp.ViewModel.Data.PhotoMimeType = photo.ContentType;
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);
                   
                }
            }

        }
    }
}
