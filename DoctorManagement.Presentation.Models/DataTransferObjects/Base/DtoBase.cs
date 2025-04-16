using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Models.DataTransferObjects.Base
{
    public class DtoBase
    {

        public Guid Id { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public IdentityUser? LastModifiedBy { get; set; }
    }
}
