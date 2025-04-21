using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class PageRequestDto<TFilter> where TFilter: new()
    {
        public TFilter Filter { get; set; } = new();
        public int PageSize { get; set; }
        public int PageIndex { get; set; } = 1;
        public bool GetAllPages { get; set; } = false;

    }
}
