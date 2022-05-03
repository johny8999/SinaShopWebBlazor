using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.UserAgg.Entities
{
    public class tblUserRole:IdentityUserRole<Guid>,IEntity
    {
        public Guid Id { get; set; }
    }
}
