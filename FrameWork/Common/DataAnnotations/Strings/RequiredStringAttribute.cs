using FrameWork.Application.Services.Localizer;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.WebApp.Common.DataAnnotations.Strings
{
    public class RequiredStringAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (ErrorMessage is null)
                ErrorMessage = "ReguiredStringMsg";

            if (value is null)
                return new ValidationResult(GetMessage(validationContext));
            else
                return ValidationResult.Success;
        }
        private string GetMessage(ValidationContext validationContext)
        {
            var _serviceProvider = validationContext.GetService<IServiceProvider>();
            var _Localizer = _serviceProvider.GetService<ILocalizer>();

            if (_Localizer is null)
            {
                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", validationContext.DisplayName);
            }
            else
            {

                ErrorMessage = _Localizer[ErrorMessage];
                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);
            }

            return ErrorMessage;
        }
    }
}
