using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpEmailConfirmation
    {
        [Display(Name = nameof(Token))]
        [RequiredString]
        public string Token { get; set; }
    }
}
