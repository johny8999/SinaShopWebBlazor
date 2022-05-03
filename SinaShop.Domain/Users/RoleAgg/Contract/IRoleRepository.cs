using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.RoleAgg.Contract
{
    public interface IRoleRepository : IRepository<tblRoles>
    {
        Task<IList<string>> GetRolesAsync(tblUsers user);
    }
}
