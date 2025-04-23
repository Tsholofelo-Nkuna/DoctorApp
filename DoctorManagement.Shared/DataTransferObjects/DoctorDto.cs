using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class DoctorDto: DtoBase
    {
        [Required]
        public string? PracticeNumber { get; set; }
        [Required]
        public string? Specialty { get; set; }
        public AddressDto? PracticeSite { get; set; } 
        public ContactDto? Contact { get; set; }
        public string UserId { get; set; }
       
    }
}
