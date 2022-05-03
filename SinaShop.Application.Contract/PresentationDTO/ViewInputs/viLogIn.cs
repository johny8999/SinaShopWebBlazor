using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.PresentationDTO.ViewInputs
{
    public class viLogIn
    {
        [RequiredString]
        [Display(Name =nameof(Email))]
        [StringLength(100)]
        [Email]
        public string Email { get; set; }

        [RequiredString]
        [Display(Name = nameof(Password))]
        [StringLength(100)]
        public string Password { get; set; }

        public bool RmemberMe { get; set; }
    }
}
