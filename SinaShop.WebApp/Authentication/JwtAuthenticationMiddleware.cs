using FrameWork.Consts;

namespace SinaShop.WebApp.Authentication
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.Any(a => a.Key == AuthConst.CookieName))
            {
                context.Request.Headers.Add("Authorization", context.Request.Cookies[AuthConst.CookieName]);
            }
            await _next(context);
        }
    }
}
