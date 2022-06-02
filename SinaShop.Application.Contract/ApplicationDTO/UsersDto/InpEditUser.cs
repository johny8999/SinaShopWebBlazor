using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpEditUser
    {
        [Display(Name = nameof(UserId))]
        [RequiredString]
        [GUID]
        [MaxLengthString(450)]
        public string UserId { get; set; }

        [RequiredString]
        [MaxLengthString(150)]
        [Email]
        public string Email { get; set; }

        [RequiredString]
        [MaxLengthString(150)]
        public string FullName { get; set; }

        [RequiredString]
        [MaxLengthString(11)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [RequiredString]
        [MaxLengthString(150)]
        public string UserName { get; set; }
    }
}
