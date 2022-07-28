using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using FrameWork.Domain;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinaShop.Domain.Users.RoleAgg.Entities;

public class tblRoles : IdentityRole<Guid>, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string PageName { get; set; }
    public int Sort { get; set; }
    public string Description { get; set; }


    public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }

    public virtual ICollection<tblRoles> tblRolesChilds { get; set; }
    public virtual tblRoles tblRoleParent { get; set; }
}
