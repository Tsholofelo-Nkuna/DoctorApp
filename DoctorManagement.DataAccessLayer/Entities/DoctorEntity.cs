using DoctorManagement.DataAccessLayer.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{
    public class DoctorEntity : EntityBase
    {
       
        public string PracticeNumber { get; set; } = string.Empty;
        public ContactEntity? Contact { get; set; }
        public AddressEntity? PracticeSite { get; set; }
        public DataSourceEntity? Title { get; set; }
        public string UserId { get; set; }
        public string Specialty { get; set; } = string.Empty;
    }
}
