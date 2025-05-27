using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.Models
{
    public class DoctorFilter : BaseFilter
    {
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SpecialtyId { get; set; }
    }
}
