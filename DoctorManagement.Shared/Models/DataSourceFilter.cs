using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.Models
{
    public class DataSourceFilter : BaseFilter
    {
      public string Name { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public int Value { get; set; }
      public string TypeCode { get; set; } = string.Empty;
    }
}
