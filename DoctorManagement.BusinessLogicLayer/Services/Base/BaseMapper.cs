using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services.Base
{
    public class BaseMapper<TSource, TDestination> : IBaseMapper<TSource, TDestination> where TDestination : new()
    {
        public virtual TDestination? Map(TSource? source)
        {
            if(source is null)
            {
                return default;
            }
            else
            {
                return source.CopyTo(new TDestination());
            }
        }

     
    }
}
