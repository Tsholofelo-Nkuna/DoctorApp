using DoctorManagement.Presentation.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Models.DataTransferObjects
{
    public class DoctorDto: DtoBase
    {
        public string PracticeNumber { get; set; } = string.Empty;
        public ContactDto? Contact { get; set; }
    }
}
