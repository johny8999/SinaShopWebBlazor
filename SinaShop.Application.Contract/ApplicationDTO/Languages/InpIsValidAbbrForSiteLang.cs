using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;

namespace SinaShop.Application.Contract.ApplicationDTO.Languages
{
    public class InpIsValidAbbrForSiteLang
    {
        [Display(Name = "Abbr")]
        [RequiredString]
        [MaxLengthString(10)]
        public string Abbr { get; set; }
    }
}
