using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.Languages
{
    public class InpIsValidAbbrForSiteLang
    {
        [Display(Name = "Abbr")]
        //[RequiredString]
       // [MaxLengthString(10)]
        public string Abbr { get; set; }
    }
}
