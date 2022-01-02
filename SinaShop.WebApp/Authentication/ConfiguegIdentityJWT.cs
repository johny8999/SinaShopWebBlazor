using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SinaShop.WebApp.Authentication
{
    public static class ConfiguegIdentityJWT
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //change defuilt from identity to Token 
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //change defuilt from identity to Token
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //change defuilt from identity to Token
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;//for seting https 
                a.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero, //cheking JWT for Expire
                    RequireSignedTokens = true, //remove signure tokan     اگر حذف شود سبکتر میشه ولی امنیت میاد پایین
                    ValidateIssuerSigningKey = true, //cheking esteem signure tokan
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConst.SecretCode)),// a key for coding signigure
                    RequireExpirationTime = true, //mandatory expire time in token
                    ValidateLifetime = true, //cheking expire time
                    ValidateAudience = true, //cheking for audience JWT which who is use jwt
                    ValidAudience = AuthConst.Audience, //audience name
                    ValidateIssuer = true, //صادر کنندگان توکن
                    ValidIssuer = AuthConst.Issuer
                };
            });

        }

        //use token
        public static void UseJWTAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtAuthenticationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }

}