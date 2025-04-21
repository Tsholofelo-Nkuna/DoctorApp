using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
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
    }
}
