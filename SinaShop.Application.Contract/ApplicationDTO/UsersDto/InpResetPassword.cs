using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpResetPassword
    {
        [RequiredString]
        [Display(Name = nameof(Token))]
        public string Token { get; set; }

        [RequiredString]
        [Display(Name = nameof(Password))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RequiredString]
        [Display(Name = nameof(Password))]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string RePassword { get; set; }
    }
}
