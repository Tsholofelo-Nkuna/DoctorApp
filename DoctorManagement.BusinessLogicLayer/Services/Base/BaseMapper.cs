using DoctorManagement.BusinessLogicLayer.Interfaces.Base;
using DoctorManagement.Shared;
using Mapster;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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
                return Map<TSource, TDestination > (source);
            }
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
          where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            TDestination destination = new TDestination();

            var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(p => p.Name == destProp.Name
                                                                     && destProp.CanWrite
                                                                     && p.PropertyType == destProp.PropertyType);
                if (sourceProp != null)
                {
                    var value = sourceProp.GetValue(source, null);
                    destProp.SetValue(destination, value, null);
                }
            }

            return destination;
        }

    }
}
