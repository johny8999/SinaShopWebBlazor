using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.RoleAgg.Entities
{
    public class tblRoles:IdentityRole<Guid>,IEntity
    {
        public string PageName { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }
    }
}
