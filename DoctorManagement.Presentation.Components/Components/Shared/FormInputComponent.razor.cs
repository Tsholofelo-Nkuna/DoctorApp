using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class FormInputComponent<TDto>
    {
        EventCallback<object?> ValueChanged;
        [Parameter]
        public Dictionary<string, object?> AdditionalAttributes { get; set; } = new();
        public string GetDisplayName()
        {
           var dtoType = typeof(TDto);
           var displayName =  dtoType
                .GetProperty(ViewModel.BoundPropertyName)?.GetCustomAttribute<DisplayAttribute>()?.Name ?? ViewModel.BoundPropertyName;
            return displayName;
        }

        public string? GetValue()
        {
            var dtoType = typeof(TDto);
            var value = dtoType
                 .GetProperty(ViewModel.BoundPropertyName)?.GetValue(ViewModel.Data);
            return Convert.ToString(value, null);
        }

        public async Task OnInputChange(ChangeEventArgs eventArgs)
        {
            var val = eventArgs.Value;
            var propInfo = typeof(TDto).GetProperty(ViewModel.BoundPropertyName);
            var propType = propInfo?.PropertyType;
            if(propType == typeof(string))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToString(val, null));
            }
            else if(propType == typeof(int))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToInt32(val, null));
            }
            else if(propType == typeof(decimal))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToDecimal(val, null));
            }
            else if (propType == typeof(double))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToDouble(val, null));
            }
            else if (propType == typeof(long))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToInt64(val, null));
            }
            else if (propType == typeof(DateTime))
            {
                propInfo?.SetValue(ViewModel.Data, Convert.ToDateTime(val, null));
            }
            else if(propType == typeof(bool))
            {
                 propInfo?.SetValue(ViewModel.Data, Convert.ToBoolean(val, null));
            }
            
                await ValueChanged.InvokeAsync(val);
        }
    }
}
