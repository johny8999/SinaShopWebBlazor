using SinaShop.Application.Languages;
using SinaShop.WebApp.Common.Utilities.IpAddress;
using System;
namespace SinaShop.WebApp.Middlewares
{
    public class RedirectToValidLangMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToValidLangMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method.ToLower() is "get")
            {
                // string Paths = context.Request.Path.HasValue ? context.Request.Path.Value:"";
                string Path = context.Request.Path.Value ?? "";
                string[] Paths = Path.Trim().Split('/').ToArray();
                if (Paths.Where(a => a != "" && a != null).Any())
                {
                    var LangAbbr = Paths[0];
                    var _IpAddressChecker = context.RequestServices.GetService<ILanguagesApplication>();
                    bool? IsValid=
                }
                else
                {
                    string SiteUrl = context.Request.Scheme + "://" + context.Request.Host.Value;
                    string LangAbbr = GetLangByIpAddress(context);

                    context.Response.Redirect(SiteUrl + "/" + LangAbbr);
                }
            }
            await _next(context);
        }

        private string GetLangByIpAddress(HttpContext context)
        {
            var _IpAddressChecker = context.RequestServices.GetService<IIpAddressChecker>();
            var _LangAbbr = _IpAddressChecker.GetLangAbbr(context.Connection.LocalIpAddress.ToString());
            if (_LangAbbr is null)
            {
                return "fa";
            }
            else
            {
                return _LangAbbr;
            }
        }
    }
}

