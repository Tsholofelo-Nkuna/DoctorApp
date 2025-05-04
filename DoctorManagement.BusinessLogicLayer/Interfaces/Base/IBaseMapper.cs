using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Interfaces.Base
{
    public interface IBaseMapper<TSource, TDestination> where TDestination: new()
    {
        public TDestination? Map(TSource? source);
       
    }
}
