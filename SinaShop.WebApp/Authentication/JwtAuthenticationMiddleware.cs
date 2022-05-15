using FrameWork.Consts;
using FrameWork.ExMethods;

namespace SinaShop.WebApp.Authentication
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _CookieName;
        private readonly string _SecretKey;

        public JwtAuthenticationMiddleware(RequestDelegate next, string cookieName, string secretKey)
        {
            _next = next;
            _CookieName = cookieName;
            _SecretKey = secretKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string EncryptedToken = null;

            for (int i = 0; i <= 10; i++)
                if (context.Request.Cookies.Any(a => a.Key == _CookieName+i))
                    EncryptedToken = context.Request.Cookies[_CookieName+i];

            if(EncryptedToken is not null)
                context.Request.Headers.Add("Authorization", EncryptedToken.AesDecrypt(_SecretKey));

            await _next(context);
        }
    }
}
