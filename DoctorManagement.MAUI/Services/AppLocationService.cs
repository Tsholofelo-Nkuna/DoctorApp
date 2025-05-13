using DoctorManagement.MAUI.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Services
{
    public class AppLocationService : IAppLocationService
    {
        private readonly GeolocationRequest _geolocationRequest;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public AppLocationService(IOptions<GeolocationRequest> locationRequestOptions) { 
          this._geolocationRequest = locationRequestOptions.Value;
          this._cancellationTokenSource = new CancellationTokenSource();
        }
        public Task<Location?> GetAppCurrentLocation()
        {
            try
            {
               return Geolocation.Default.GetLocationAsync(this._geolocationRequest,this._cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                return null;
              
            }
        }

        public async Task ViewLocationOnMap(Location location, string label)
        {
           
            var options = new MapLaunchOptions { Name = label};

            try
            {
                await Map.Default.OpenAsync(location, options); 
            }
            catch (Exception ex)
            {
                // No map application available to open
            }
        }

        public async Task<Placemark?> GetReverseGeocodeData(double latitude = 47.673988, double longitude = -122.121513)
        {
            try
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

                Placemark placemark = placemarks?.FirstOrDefault();

                return placemark;
            }
            catch (Exception ex)
            {

                return null;
            }
           
        }
        public void CancelLocationRequest()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}
