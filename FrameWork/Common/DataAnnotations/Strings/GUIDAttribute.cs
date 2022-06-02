using FrameWork.Application.Services.Localizer;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace FrameWork.Common.DataAnnotations.Strings
{
    public class GUIDAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return ValidationResult.Success;

                if (value is not string)
                    return ValidationResult.Success;
                if (value.ToString().Contains(','))
                {
                    foreach (var item in value.ToString().Split(','))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var _Guid = Guid.Parse((string)item);
                        }
                    }
                    return ValidationResult.Success;
                }
                else
                {
                    var _Guid = Guid.Parse((string)value);
                    return ValidationResult.Success;
                }


            }
            catch (Exception ex)
            {
                return new ValidationResult(GetMessage(validationContext));
            }
        }

        private string GetMessage(ValidationContext validationContext)
        {
            if (ErrorMessage is null)
                ErrorMessage = "GUIDMsg";

            var _ServiceProvider = validationContext.GetService<IServiceProvider>();
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();
            ErrorMessage = _Localizer[ErrorMessage];

            if (ErrorMessage.Contains("{0}"))
                ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            return ErrorMessage;
        }
    }
}
