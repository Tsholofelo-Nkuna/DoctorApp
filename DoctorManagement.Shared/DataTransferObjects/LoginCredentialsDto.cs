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
       
        [EmailAddress, DataType(DataType.EmailAddress), Required]
        public string Username { get; set; } = string.Empty;
       
        [Required]
        public string Password { get; set; } = string.Empty;    
    }
}
