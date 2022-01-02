using FrameWork.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Common.ExMethod;
using SinaShop.Infrastructure.EfCore.Config;
using SinaShop.Infrastructure.EfCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Context
{
    public class MainContext : IdentityDbContext<tblUsers, tblRoles, Guid>
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
