using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblUserConfiguration : IEntityTypeConfiguration<tblUsers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUsers> builder)
        {
            builder.Property(conf => conf.Fullname).IsRequired().HasMaxLength(100);
            builder.Property(a => a.AccessLevelId).HasMaxLength(450);//foreignkey

            builder.HasOne(a => a.tblAccessLevel)
                    .WithMany(a => a.tblUsers)
                    .HasPrincipalKey(a => a.Id)
                    .HasForeignKey(a => a.AccessLevelId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
