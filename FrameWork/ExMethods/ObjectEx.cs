using FrameWork.Application.Arguments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.ExMethods
{
    public static class ObjectEx
    {
        public static void CheckModelState<T>(this T input, IServiceProvider serviceProvider)
        {
            if (input is null)
                throw new ArgumentInvalidException($"{nameof(input)} Can not be null.");

            List<ValidationResult> validationResults = new();
            ValidationContext validationContext = new(input,serviceProvider,default);
            validationContext.InitializeServiceProvider(a => serviceProvider);

            if (Validator.TryValidateObject(input, validationContext, validationResults,true))
            {
                if (validationContext is not null)
                {
                    string concat=String.Join("",validationResults.Select(x => x.ErrorMessage));
                    throw new ArgumentInvalidException(concat);
                }
            }
        }
    }
}
