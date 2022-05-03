using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.UserAgg.Contracts
{
    public interface IUserRepository : IRepository<tblUsers>
    {
        Task<IdentityResult> AddAsync(tblUsers entity, string Password);
        Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user);
        Task<tblUsers> FindByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(tblUsers user, string token);
        Task<IList<string>> GetRolesAsync(tblUsers user);
        Task<IdentityResult> RemoveFromRolesAsync(tblUsers user, IEnumerable<string> roles);
        Task<IdentityResult> AddToRolesAsync(tblUsers user, IEnumerable<string> roles);
        Task<tblUsers> FindByEmailAsync(string email);
        Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
