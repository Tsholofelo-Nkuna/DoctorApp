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
            catch (Exception)
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
