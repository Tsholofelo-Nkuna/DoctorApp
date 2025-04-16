using DoctorManagement.Presentation.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Models.DataTransferObjects
{
    public class PatientDto: DtoBase
    {
        public ContactDto? Contact { get; set; }
        public AddressDto? Address { get; set; }
    }
}
