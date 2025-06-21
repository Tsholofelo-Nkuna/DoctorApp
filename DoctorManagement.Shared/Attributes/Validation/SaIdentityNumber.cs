using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.Attributes.Validation
{
    public class SaIdentityNumber : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Invalid Id";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ValidationResult errorResponse = new(string.IsNullOrWhiteSpace(ErrorMessage) ? _defaultErrorMessage : ErrorMessage);
            if (value is string validStr)
            {
                var isValidId = Regex.Match(validStr, "[^0]").Success && this.ValidateLuhn(validStr);
                return isValidId ? ValidationResult.Success : errorResponse;
            }
            else
            {

                return errorResponse;
            }
        }

        private  bool ValidateLuhn(string number)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(number[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n = (n % 10) + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }
    }
}
