using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Services.Interfaces
{
    public interface IAppLocationService
    {
        public Task<Location?> GetAppCurrentLocation();
        Task<Placemark?> GetReverseGeocodeData(double latitude = 47.673988, double longitude = -122.121513);
        public Task ViewLocationOnMap(Location location, string label);
    }
}
