using DoctorManagement.DataAccessLayer.Entities.Base;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{
    public class AddressEntity: EntityBase
    {
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string StreetName {  get; set; } = string.Empty;
        public string HouseNumber {  get; set; } = string.Empty;    
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
