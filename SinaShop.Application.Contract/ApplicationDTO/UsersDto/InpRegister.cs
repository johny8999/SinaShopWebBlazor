﻿using FrameWork.Common.DataAnnotations.Strings;
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
    public class InpRegister
    {
        [Display(Name = "FullName")]
        [RequiredString]
        [MaxLengthString(100)]
        [FullName]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [Email]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
