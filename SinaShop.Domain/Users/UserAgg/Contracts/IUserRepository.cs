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
    }
}
