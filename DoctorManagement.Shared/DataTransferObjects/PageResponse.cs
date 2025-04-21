using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class PageResponse<TData> : ResponseDto<IEnumerable<TData>>
    {
        
        public int TotalRecordCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; } = 1;
    }
}
