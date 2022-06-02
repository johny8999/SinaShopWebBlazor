using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Services.ReCaptcha
{
    public class GoogleCapchaConfig
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
    }
}
