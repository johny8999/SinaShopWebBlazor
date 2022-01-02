using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Core.Configuration
{
    public static class IdentityConfig
    {
        public static IdentityBuilder AddCustomIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<tblUsers, tblRoles>(a =>
            {
                a.SignIn.RequireConfirmedAccount = true;// The Account must be confirm
                a.SignIn.RequireConfirmedEmail = true; // The Email must be confirm
                //a.SignIn.RequireConfirmedPhoneNumber = true;

                a.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";//for accepting character for UserName
                a.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 15, 0);//lock user account is locked for wrong password
                a.Lockout.MaxFailedAccessAttempts = 6;//The account is locked after certain number

                a.Password.RequireDigit = false;// for complex pasword digit
                a.Password.RequireLowercase = false; // for complex pasword digit
                a.Password.RequiredLength = 6; //the lenth of password
                a.Password.RequiredUniqueChars = 0;// unique character for entering password
                a.Password.RequireNonAlphanumeric = false; // for none althabet character
                a.Password.RequireUppercase = false; // upper case must be written

            }).AddEntityFrameworkStores<MainContext>()
              .AddDefaultTokenProviders();
        }
    }
}
