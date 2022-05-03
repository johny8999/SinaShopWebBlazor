using FrameWork.Application.Services.Email;
using FrameWork.Application.Services.IpList;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Comman.ExMethod;
using SinaShop.Application.Languages;
using SinaShop.Application.Roles;
using SinaShop.Application.UserAplication;
using SinaShop.Domain.Region.LanguageAgg.Contract;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.Domain.Users.RoleAgg.Contract;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Infrastructure.EfCore.Config;
using SinaShop.Infrastructure.EfCore.Context;
using SinaShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;
using SinaShop.Infrastructure.EfCore.Repository.AccessLevel;
using SinaShop.Infrastructure.EfCore.Repository.Languages;
using SinaShop.Infrastructure.EfCore.Repository.UserRole;
using SinaShop.Infrastructure.EfCore.Repository.Users;
using SinaShop.Infrastructure.Logger.SeriLoger;
using SinaShop.Infrastructure.Seed.Base.AccessLevel;
using SinaShop.Infrastructure.Seed.Base.Languages;
using SinaShop.Infrastructure.Seed.Base.Main;
using SinaShop.Infrastructure.Seed.Base.Role;
using SinaShop.Infrastructure.Seed.Base.User;
using SinaShop.Infrastructure.Services.Email;

namespace SinaShop.Infrastructure.Core.Configuration
{
    public static class Bootstraper
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddCustomAutoMapper();

            #region Add Services
            {
                services.AddDbContext<MainContext>(opt =>
                {
                    opt.UseSqlServer(ConnectionString.Get());
                });
                services.AddSingleton<ILogger, SeriLogger>();
                services.AddSingleton<IIPList, IPList>();
                services.AddSingleton<IEmailSender, GmailSender>();
                services.AddSingleton<IJwtBuilder, JwtBuilder>();
            }
            #endregion Add Services

            #region Add Repositories
            {
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ILanguageRepository, LanguageRepository>();
                services.AddScoped<IAccessLevelRepository, AccessLevelRepository>();
                services.AddScoped<IAccessLevelRoleRepository, AccessLevelRoleRepository>();
                services.AddScoped<IRoleRepository, RoleRepository>();
            }
            #endregion Add Repositories

            #region Add Applications 
            {
                services.AddScoped<ILanguagesApplication, LanguagesApplication>();
                services.AddScoped<IUserApplication, UserApplication>();
                services.AddScoped<IAccessLevelApplication, AccessLevelApplication>();
                services.AddScoped<IRoleApplication, RoleApplication>();
            }
            #endregion Add Applications 

            #region Seeds
            {
                services.AddTransient<ISeedAccessLevel, SeedAccessLevel>();
                services.AddTransient<ISeed_Language, Seed_Language>();
                services.AddTransient<ISeed_main, Seed_main>();
                services.AddTransient<ISeed_User, Seed_User>();
                services.AddTransient<ISeedRole, SeedRole>();


            }
            #endregion Seeds
        }
    }
}
