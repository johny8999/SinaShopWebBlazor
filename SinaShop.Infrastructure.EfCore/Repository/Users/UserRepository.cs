using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Repository.Users
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {
        private readonly UserManager<tblUsers> _UserManager;
        public UserRepository(MainContext context, UserManager<tblUsers> userManager) : base(context)
        {
            _UserManager = userManager;
        }
        public async Task<IdentityResult> AddAsync(tblUsers entity, string Password)
        {
            return await _UserManager.CreateAsync(entity, Password);
        }
        public async Task<tblUsers> FindByIdAsync(string userId)
        {
            return  await _UserManager.FindByIdAsync(userId);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user)
        {
            return await _UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

    }
}
