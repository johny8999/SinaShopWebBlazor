using FrameWork.Application.Services.Email;
using FrameWork.Application.Services.IpList;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Languages;
using SinaShop.Application.UserAplication;
using SinaShop.Domain.Region.LanguageAgg.Contract;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Infrastructure.EfCore.Config;
using SinaShop.Infrastructure.EfCore.Context;
using SinaShop.Infrastructure.EfCore.Repository.AccessLevel;
using SinaShop.Infrastructure.EfCore.Repository.Languages;
using SinaShop.Infrastructure.EfCore.Repository.Users;
using SinaShop.Infrastructure.Logger.SeriLoger;
using SinaShop.Infrastructure.Seed.Base.AccessLevel;
using SinaShop.Infrastructure.Seed.Base.Languages;
using SinaShop.Infrastructure.Seed.Base.Main;
using SinaShop.Infrastructure.Seed.Base.User;
using SinaShop.Infrastructure.Services.Email;

namespace SinaShop.Infrastructure.Core.Configuration
{
    public static class Bootstraper
    {
        public static void Config(this IServiceCollection services)
        {
            #region Add Services
            {
                services.AddDbContext<MainContext>(opt =>
                {
                    opt.UseSqlServer(ConnectionString.Get());
                });
                services.AddScoped<ILogger, SeriLogger>();
                services.AddScoped<IIPList, IPList>();
                services.AddScoped<IEmailSender, GmailSender>();
            }
            #endregion Add Services

            #region Add Repositories
            {
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ILanguageRepository, LanguageRepository>();
                services.AddScoped<IAccessLevelRepository, AccessLevelRepository>();
            }
            #endregion Add Repositories

            #region Add Applications 
            {
                services.AddScoped<ILanguagesApplication, LanguagesApplication>();
                services.AddScoped<IUserApplication, UserApplication>();
                services.AddScoped<IAccessLevelApplication, AccessLevelApplication>();
            }
            #endregion Add Applications 

            #region Seeds
            {
                services.AddTransient<ISeedAccessLevel, SeedAccessLevel>();
                services.AddTransient<ISeed_Language, Seed_Language>();
                services.AddTransient<ISeed_main, Seed_main>();
                services.AddTransient<ISeed_User, Seed_User>();

            }
            #endregion Seeds
        }
    }
}
