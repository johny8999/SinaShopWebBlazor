using FrameWork.Application.Services.Localizer;
using FrameWork.Common.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SinaShop.Domain.Users.RoleAgg.Contract;
using SinaShop.Domain.Users.RoleAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SinaShop.Infrastructure.Seed.Base.Role
{
    public class SeedRole : ISeedRole
    {
        private readonly ILogger _Logger;
        private readonly IRoleRepository _RoleRepository;
        public SeedRole(ILogger logger, IRoleRepository RoleRepository)
        {
            _Logger = logger;
            _RoleRepository = RoleRepository;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "Manager"))
                {
                    await _RoleRepository.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "Manager",
                        PageName = "Manager",
                        Description = "Manager",
                        NormalizedName = "Manager".ToUpper()
                    });
                }
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "Seller"))
                {
                    await _RoleRepository.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "Seller",
                        PageName = "Seller",
                        Description = "Seller",
                        NormalizedName = "Seller".ToUpper()
                    });
                }
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "Customer"))
                {
                    await _RoleRepository.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "Customer",
                        PageName = "Customer",
                        Description = "Customer",
                        NormalizedName = "Customer".ToUpper()
                    });
                }
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "Test"))
                {
                    await _RoleRepository.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "Test",
                        PageName = "Test",
                        Description = "Test",
                        NormalizedName = "Test".ToUpper()
                    });
                }
                await _RoleRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

    }
}
