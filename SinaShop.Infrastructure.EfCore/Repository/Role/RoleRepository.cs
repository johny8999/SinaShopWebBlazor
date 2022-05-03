using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.RoleAgg.Contract;
using SinaShop.Domain.Users.RoleAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;

namespace SinaShop.Infrastructure.EfCore.Repository.UserRole
{
    public class RoleRepository : BaseRepository<tblRoles>, IRoleRepository
    {
        private readonly UserManager<tblUsers> _UserManager;
        public RoleRepository(MainContext context) : base(context) { }
        public async Task<List<string>> GetRolesAsync(tblUsers user)
        {
            var q= await _UserManager.GetRolesAsync(user);
            return q.ToList();
        }


    }
}
