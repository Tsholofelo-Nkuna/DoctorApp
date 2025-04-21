using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class SignupDoctorDto
    {
        public LoginCredentialsDto Credentials { get; set; } = new();
        public DoctorDto DoctorDetails { get; set; } = new();
    }
}
