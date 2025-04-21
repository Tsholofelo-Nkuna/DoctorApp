
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects.Base
{
    public class DtoBase
    {

        public Guid Id { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedByUserId { get; set; }
        public string? LastModifiedByUserId { get; set; }
    }
}
