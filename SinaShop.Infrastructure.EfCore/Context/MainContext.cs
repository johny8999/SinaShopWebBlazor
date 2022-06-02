using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Common.ExMethod;
using SinaShop.Infrastructure.EfCore.Config;
using SinaShop.Infrastructure.EfCore.Contracts;

namespace SinaShop.Infrastructure.EfCore.Context
{
    public class MainContext : IdentityDbContext<tblUsers, tblRoles, Guid, IdentityUserClaim<Guid>,
                                tblUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new tblUserConfiguration());

            var EntitiesAssambly = typeof(IEntity).Assembly;
            builder.RegisterAllEntities<IEntity>(EntitiesAssambly);

            var EntitiesConfAssambly = typeof(IEntityConf).Assembly;
            builder.RegisterEntityTypeConfiguration(EntitiesConfAssambly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Get());
        }


    }
}
