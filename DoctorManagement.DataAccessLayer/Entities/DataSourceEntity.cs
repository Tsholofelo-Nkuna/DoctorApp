using DoctorManagement.DataAccessLayer.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.DataAccessLayer.Entities
{

    [Index(nameof(TypeCode))]
    public class DataSourceEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Abbr { get; set; } = string.Empty;
    }
}
