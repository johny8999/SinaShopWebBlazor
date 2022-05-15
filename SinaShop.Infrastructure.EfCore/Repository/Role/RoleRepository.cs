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
        public RoleRepository(MainContext context, UserManager<tblUsers> userManager) : base(context)
        {
            _UserManager = userManager;
        }
        public async Task<IList<string>> GetRolesAsync(tblUsers user)
        {
            try
            {
                return await _UserManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
