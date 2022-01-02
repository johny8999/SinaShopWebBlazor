using FrameWork.Infrastructure;
using SinaShop.Infrastructure.Seed.Base.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Seed.Base.Main
{
    public class Seed_main: ISeed_main
    {
        private readonly ISeed_Language _Seed_Language;
        private readonly ILogger _Logger;


        public Seed_main(ISeed_Language seed_Language, ILogger logger)
        {
            _Seed_Language = seed_Language;
            _Logger = logger;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                await _Seed_Language.RunAsync();
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
