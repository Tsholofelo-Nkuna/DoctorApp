using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class SignupDoctorDto : DoctorDto
    {
        public LoginCredentialsDto Credentials { get; set; } = new();
        public string PhotoMimeType { get; set; } = string.Empty;
        public string PhotoFileName { get; set; } = string.Empty;
        public byte[] PhotoContents { get; set; } = [];

    }
}
