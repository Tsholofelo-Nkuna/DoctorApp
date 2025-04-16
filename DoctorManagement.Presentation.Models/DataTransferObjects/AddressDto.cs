using DoctorManagement.Presentation.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Models.DataTransferObjects
{
    public class AddressDto : DtoBase
    {
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;
        [Display(Name ="State/Province")]
        public string State { get; set; } = string.Empty;
        [Display(Name = "Zip/Postal Code")]
        public string Zip { get; set; } = string.Empty;
        [Display(Name = "Street")]
        public string StreetName { get; set; } = string.Empty;
        [Display(Name = "Building/House No.")]
        public string HouseNumber { get; set; } = string.Empty;
    }
}
