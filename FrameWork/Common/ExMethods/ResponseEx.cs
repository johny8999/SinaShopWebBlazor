using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Consts;
using static System.Collections.Specialized.BitVector32;

namespace FrameWork.Common.ExMethods
{
    public static class ResponseEx
    {
        public static void DeleteAuthCookie(this HttpResponse response)
        {
            response.Cookies.Delete("AspNetCore.Identity.Application");

            for (int i = 0; i <= 10; i++)
            {
                response.Cookies.Delete("AspNetCore.Identity.ApplicationC" + i);
                response.Cookies.Delete(AuthConst.CookieName + i);
            }
        }
        public static void CreateAuthCookie(this HttpResponse response, string authToken, bool rememberMe)
        {
            #region Remove Cookie
            {
                response.DeleteAuthCookie();
            }
            #endregion Remove Cookie

            #region Add new cookie
            {
                short Counter = 0;
                int LimitCount = 2000;
                while (authToken is not null)
                {
                    if (authToken.Length > LimitCount)
                    {
                        string Section = authToken.Substring(0, LimitCount);
                        authToken = authToken.Remove(0, LimitCount);
                        response.Cookies.Append(AuthConst.CookieName + Counter, Section,
                            rememberMe ? new CookieOptions() { Expires = DateTime.Now.AddDays(2) } : new CookieOptions());
                    }
                    else
                    {
                        response.Cookies.Append(AuthConst.CookieName + Counter, authToken,
                            rememberMe ? new CookieOptions() { Expires = DateTime.Now.AddDays(2) } : new CookieOptions());
                        authToken = null;
                    }
                    Counter++;
                }
            }
            #endregion Add new cookie
        }
    }
}
