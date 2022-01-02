using FrameWork.Application.Services.IpList;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SinaShop.Application.Languages;
using SinaShop.Domain.Region.LanguageAgg.Contract;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Infrastructure.EfCore.Config;
using SinaShop.Infrastructure.EfCore.Context;
using SinaShop.Infrastructure.EfCore.Repository.Languages;
using SinaShop.Infrastructure.EfCore.Repository.Users;
using SinaShop.Infrastructure.Logger.SeriLoger;
using SinaShop.Infrastructure.Seed.Base.Languages;
using SinaShop.Infrastructure.Seed.Base.Main;

namespace SinaShop.Infrastructure.Core.Configuration
{
    public static class Bootstraper
    {
        public static void Config(this IServiceCollection services)
        {
            //Add Services
            services.AddDbContext<MainContext>(opt =>
            {
                opt.UseSqlServer(ConnectionString.Get());
            });
            services.AddScoped<ILogger, SeriLogger>();
            services.AddScoped<IIPList, IPList>();

            //Add Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();


            //Add Applications 
            services.AddScoped<ILanguagesApplication,LanguagesApplication>();


            //Seeds
            services.AddTransient<ISeed_Language, Seed_Language>();
            services.AddTransient<ISeed_main, Seed_main>();
        }
    }
}
