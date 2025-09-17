using DoctorManagement.DataAccessLayer.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{
    public class DoctorEntity : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PracticeNumber { get; set; } = string.Empty;
        public ContactEntity? Contact { get; set; }
        public AddressEntity? PracticeSite { get; set; }
        public DataSourceEntity? Title { get; set; }
        public string UserId { get; set; }
        public DataSourceEntity? Specialty {  get; set; }
        public decimal ConsultationFee { get; set; }
        public bool AcceptHomeVisits { get; set; }
    }
}
