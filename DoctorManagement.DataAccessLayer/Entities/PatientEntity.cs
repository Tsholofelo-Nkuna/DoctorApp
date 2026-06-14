using DoctorManagement.DataAccessLayer.Entities.Base;
using Microsoft.AspNetCore.Identity;
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
        public string UserId { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;
        public string MedicalAidNumber { get; set; } = string.Empty;
        public string MedicalAidPlanName { get; set; } = string.Empty;
        public string MedicalAidProvider { get; set; } = string.Empty;
        public string PhotoFileName { get; set; } = string.Empty;
        public byte[] PhotoContents { get; set; } = [];
        public string PhotoMimeType { get; set; } = string.Empty;
    }
}
