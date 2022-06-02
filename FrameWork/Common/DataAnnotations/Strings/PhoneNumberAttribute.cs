using FrameWork.Application.Services.Localizer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrameWork.Common.DataAnnotations.Strings
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not string)
                return new ValidationResult(GetMessage(validationContext));

            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(GetMessage(validationContext));

            if (Regex.IsMatch(value.ToString(), "\\A[0-9]{11}\\z"))
                return ValidationResult.Success;

            else
                return new ValidationResult(GetMessage(validationContext));

        }

        private string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = validationContext.GetService<IServiceProvider>();
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();
            if (_Localizer is null)
            {
                if (ErrorMessage.Contains("{0}"))
                {
                    ErrorMessage = ErrorMessage.Replace("{0}", validationContext.DisplayName);
                }
                if (ErrorMessage.Contains("{1}"))
                {
                    ErrorMessage = ErrorMessage.Replace("{1}", "sa");
                }
            }
            else
            {

                ErrorMessage = _Localizer[ErrorMessage];
                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

                if (ErrorMessage.Contains("{1}"))
                    ErrorMessage = ErrorMessage.Replace("{1}","sa");
            }
            return ErrorMessage;
        }
    }
}
