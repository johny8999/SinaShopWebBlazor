using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.ApplicationDTO.AccessLevel
{
    public class InpGetByIdName
    {
        [DisplayName]
        [RequiredString]
        public string Name { get; set; }

    }
}
