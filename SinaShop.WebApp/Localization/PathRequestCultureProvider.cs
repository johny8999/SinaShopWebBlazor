using Microsoft.AspNetCore.Localization;
using SinaShop.Application.Languages;
using SinaShop.WebApp.Common.Utilities.IpAddress;
using Microsoft.AspNetCore.Http;



namespace SinaShop.WebApp.Localization
{
    public class PathRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {

            if (httpContext == null)
                throw new ArgumentNullException();

            var _LanguageApplication = httpContext.RequestServices.GetService<ILanguagesApplication>();
            //as ILanguagesApplication;


            string Path = httpContext.Request.Path;
            string CultureName = Path.Trim().Trim('/').Split("/").First().ToLower();

            var LangCode = await _LanguageApplication.GetCodeByAbbrAsync(CultureName);

            if (LangCode == null)
            {
                var _IpAddressCheker = httpContext.RequestServices.GetService<IIpAddressChecker>();
                var _UserIpAddress = httpContext.Connection.RemoteIpAddress.ToString();

                if (_IpAddressCheker.CheckIp(_UserIpAddress) == "ir")
                {
                    LangCode = "fa-IR";
                }
                else if (_IpAddressCheker.CheckIp(_UserIpAddress) == "us")
                {
                    LangCode = "en-US";
                }
                else
                {
                    LangCode = "fa-IR";
                }
            }

            return new ProviderCultureResult(LangCode, LangCode);
        }

    }
}
