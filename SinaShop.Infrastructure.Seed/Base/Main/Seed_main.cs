using FrameWork.Infrastructure;
using SinaShop.Infrastructure.Seed.Base.AccessLevel;
using SinaShop.Infrastructure.Seed.Base.Languages;
using SinaShop.Infrastructure.Seed.Base.Role;
using SinaShop.Infrastructure.Seed.Base.User;

namespace SinaShop.Infrastructure.Seed.Base.Main
{
    public class Seed_main : ISeed_main
    {
        private readonly ISeed_Language _Seed_Language;
        private readonly ISeedAccessLevel _SeedAccessLevel;
        private readonly ISeed_User _Seed_User;
        private readonly ISeedRole _SeedRole;
        private readonly ILogger _Logger;


        public Seed_main(ISeed_Language seed_Language, ILogger logger,
            ISeedAccessLevel seedAccessLevel, ISeed_User seed_User, ISeedRole seedRole)
        {
            _Seed_Language = seed_Language;
            _Logger = logger;
            _SeedAccessLevel = seedAccessLevel;
            _Seed_User = seed_User;
            _SeedRole = seedRole;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                await _Seed_Language.RunAsync();
                await _SeedAccessLevel.RunAsync();
                await _Seed_User.RunAsync();
                await _SeedRole.RunAsync();
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
