using DoctorManagement.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{
    public class PatientEntity : EntityBase
    {
        public ContactEntity? Contact { get; set; }
        public AddressEntity? Address { get; set; }
    }
}
