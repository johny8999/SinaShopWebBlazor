using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpLoginByEmailPassword
    {
        [RequiredString]
        [Display(Name = nameof(Email))]
        [Email]
        public string Email { get; set; }
        [RequiredString]
        [Display(Name = nameof(Email))]
        public string Password { get; set; }
    }
}
