using FrameWork.Domain;
using SinaShop.Domain.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevel:IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<tblUsers> tblUsers { get; set; }
    }
}
