using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class ContactDto : DtoBase
    {
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Phone"), Required]
        public string Phone { get; set; } = string.Empty;
        [Display(Name = "Email"), EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}
