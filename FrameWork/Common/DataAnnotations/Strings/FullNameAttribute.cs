using FrameWork.Application.Services.Localizer;
using FrameWork.ExMethods;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;


namespace FrameWork.Common.DataAnnotations.Strings
{
    public class FullNameAttribute : ValidationAttribute
    {
        private ILocalizer _Localizer;

        public FullNameAttribute()
        {
            if (ErrorMessage is null)
            {
                ErrorMessage = "FullNameMsg";
            }
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _serviceProvider = validationContext.GetService<IServiceProvider>();
            _Localizer = _serviceProvider.GetService<ILocalizer>();

            

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new ArgumentException("FullName only work on string datatype");

            string FullName = value.ToString();
            if (FullName.IsMatch(@"^[A-Za-zا-ی?؟ئءأإؤيةـآۀًٌٍَُِّ\sآا-ی]*$"))
                return ValidationResult.Success;

            else
                return new ValidationResult(GetMessage(validationContext));
        }
        private string GetMessage(ValidationContext validationContext)
        {
            if (_Localizer is null)
                return ErrorMessage.Replace("{0}", validationContext.DisplayName);
            else
                return _Localizer[ErrorMessage, validationContext.DisplayName];
        }
    }
}
