using DoctorManagement.Shared.Attributes.Validation;
using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class SignupDto 
    {
        public LoginCredentialsDto Credentials { get; set; } = new();
        public ContactDto Contact { get; set; } = new();
        public AddressDto Address { get; set; } = new();
        public GeneralInfo GeneralInfo { get; set; } = new();
    }

    public class GeneralInfo
    {
        [Required, SaIdentityNumber]
        public string IdentityNumber { get; set; } = string.Empty;
        public string MedicalAidNumber { get; set; } = string.Empty ;
    }
}
