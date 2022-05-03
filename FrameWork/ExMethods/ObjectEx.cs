﻿using FrameWork.Application.Arguments;
using System.ComponentModel.DataAnnotations;

namespace FrameWork.ExMethods
{
    public static class ObjectEx
    {
        public static void CheckModelState<T>(this T input, IServiceProvider serviceProvider)
        {
            if (input is null)
                throw new ArgumentInvalidException($"{nameof(input)} Can not be null.");

            var validationResult = new List<ValidationResult>();
            ValidationContext validationContext = new(input);
            validationContext.InitializeServiceProvider(a => serviceProvider);

            if (!Validator.TryValidateObject(input, validationContext, validationResult, true))
            {
                if (validationResult is not null)
                {
                    string concat = string.Join(" ", validationResult.Select(a => a.ErrorMessage));
                    throw new ArgumentInvalidException(concat);
                }
            }
        }
    }
}
