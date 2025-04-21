using DoctorManagement.Shared.DataTransferObjects.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class LoginCredentialsDto
    {
        public LoginCredentialsDto() { }
        [DisplayName("Password")]
        public string Password { get; set; } = string.Empty;
        [EmailAddress, DataType(DataType.EmailAddress), DisplayName("Email")]
        public string Username { get; set; } = string.Empty;
    }
}
