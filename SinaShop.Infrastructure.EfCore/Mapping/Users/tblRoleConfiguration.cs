using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Infrastructure.EfCore.Contracts;

namespace SinaShop.Infrastructure.EfCore.Mapping.Users;

public class tblRoleConfiguration : IEntityTypeConfiguration<tblRoles>, IEntityConf
{
    public void Configure(EntityTypeBuilder<tblRoles> builder)
    {
        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.PageName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
        builder.Property(a => a.ParentId).HasMaxLength(450);

        builder.HasOne(a => a.tblRoleParent)
               .WithMany(a => a.tblRolesChilds)
               .HasPrincipalKey(a => a.ParentId)
               .HasForeignKey(a => a.ParentId);
    }

}
