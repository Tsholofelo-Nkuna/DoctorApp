using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class PatientDto: DtoBase
    {
        public ContactDto? Contact { get; set; }
        public AddressDto? Address { get; set; }
    }
}
