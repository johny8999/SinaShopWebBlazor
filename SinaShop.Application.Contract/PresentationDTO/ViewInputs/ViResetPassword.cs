using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.PresentationDTO.ViewInputs
{
    public class ViResetPassword
    {
        [RequiredString]
        [Display(Name =nameof(Token))]
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
