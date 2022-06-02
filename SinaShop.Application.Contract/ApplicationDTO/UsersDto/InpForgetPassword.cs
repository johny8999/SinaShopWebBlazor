using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpForgetPassword
    {
        [RequiredString]
        [Email]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Display(Name = nameof(ForgetPasswordUrl))]
        [RequiredString]
        [MaxLength(200)]
        public string ForgetPasswordUrl { get; set; }
    }
}
