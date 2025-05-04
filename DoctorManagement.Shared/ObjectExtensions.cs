using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoctorManagement.Shared
{
    public static class ObjectExtensions
    {

        public static string ToJson(this object obj) { 
           return JsonSerializer.Serialize(obj, new JsonSerializerOptions()
           {
               AllowTrailingCommas = true,
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
               
           });
        }


        public static TDestination? CopyTo<TDestination>(this object? obj, TDestination? destination) where
            TDestination: new()
        {
            if (obj is null)
            {
                return default(TDestination);
            }

            if(destination is null)
            {
                destination = new TDestination();
            }
           
            var sourceType = obj.GetType();
            var destinationType = typeof(TDestination);


            foreach (var source in sourceType?.GetProperties() ?? [])
            {
                if (source is { GetMethod.IsPublic: true })
                {
                    var targetProp = destinationType.GetProperty(source.Name);
                    if ( targetProp is { SetMethod.IsPublic : true }
                        && targetProp.PropertyType.FullName == source.PropertyType.FullName
                        )
                    {
                        targetProp.SetValue(destination, source.GetValue(obj));
                    }
                }

            }

            return destination;

        }
    }
}
