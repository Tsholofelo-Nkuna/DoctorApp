using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class DataSourceDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Abbr {  get; set; } = string.Empty;
    }
}
