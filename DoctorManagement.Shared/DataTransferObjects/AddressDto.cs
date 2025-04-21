using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class AddressDto : DtoBase
    {
        [Display(Name = "City"), Required]
        public string City { get; set; } = string.Empty;
        [Display(Name ="State/Province"), Required]
        public string State { get; set; } = string.Empty;
        [Display(Name = "Zip/Postal Code"), Required]
        public string Zip { get; set; } = string.Empty;
        [Display(Name = "Street"), Required]
        public string StreetName { get; set; } = string.Empty;
        [Display(Name = "Building/House No."), Required]
        public string HouseNumber { get; set; } = string.Empty;
    }
}
