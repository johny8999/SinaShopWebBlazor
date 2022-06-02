using FrameWork.Common.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;

namespace SinaShop.Infrastructure.Seed.Base.AccessLevel
{
    public class SeedAccessLevel : ISeedAccessLevel
    {
        private readonly ILogger _logger;
        private readonly IAccessLevelRepository _AccessLevelRepository;
        public SeedAccessLevel(ILogger logger, IAccessLevelRepository accessLevelRepository)
        {
            _logger = logger;
            _AccessLevelRepository = accessLevelRepository;
        }
        public async Task<bool> RunAsync()
        {
            try
            {
                if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name.Equals("GeneralManager")))
                {
                    await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "GeneralManager",
                    });
                }

                if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name.Equals("NotConfirmedUser")))
                {
                    await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "NotConfirmedUser",
                    });

                    if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name.Equals("ConfirmedUser")))
                    {
                        await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                        {
                            Id = new Guid().SequentialGuid(),
                            Name = "DefaultUser",
                        });
                    }
                }
                await _AccessLevelRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
    }
}
