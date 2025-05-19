using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DoctorManagement.Shared.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PresentOrFutureDate : ValidationAttribute
    {
        private readonly string _errorMessage = "must be a present or future date";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var errorMessage = string.IsNullOrEmpty(this.ErrorMessage) ? $"{validationContext.DisplayName} {_errorMessage}" : this.ErrorMessage;
           if(value is null)
            {
                return ValidationResult.Success;
            }
            else
            {
                if(value is DateTime dateTimeInstance)
                {
                   return dateTimeInstance.Date >= DateTime.Now.Date ? ValidationResult.Success : new(errorMessage);
                }
                else
                {
                    return new(errorMessage);
                }
            }
        }
    
        
    }
}
