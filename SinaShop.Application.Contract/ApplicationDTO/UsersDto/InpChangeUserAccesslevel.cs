using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpChangeUserAccesslevel
    {
        [Display(Name = nameof(UserId))]
        [RequiredString]
        [StringLength(450)]
        public string UserId { get; set; }

        [Display (Name =nameof(AccessLevelId))]
        [RequiredString]
        [StringLength (150)]
        public string AccessLevelId { get; set; }

    }
}
