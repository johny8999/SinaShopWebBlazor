using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.JwtDTO
{
    public class InpCreateToken
    {
        
        [GUID]
        public string UserId { get; set; }
    }
}
