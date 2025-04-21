
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? LastModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedByUserId { get; set; }
        public string? LastModifiedByUserId { get; set; }
    }
}
