using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SinaShop.Domain.Region.LanguageAgg.Contract;
using SinaShop.Domain.Region.LanguageAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Seed.Base.Languages
{
    public class Seed_Language : ISeed_Language
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger _Logger;
        public Seed_Language(ILanguageRepository languageRepository, ILogger logger)
        {
            _languageRepository = languageRepository;
            _Logger = logger;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                if (!await _languageRepository.GetNoTraking.AnyAsync(a => a.Name == "Persian(IR)"))
                {
                    TblLanguages tblLanguages = new();
                    tblLanguages.Id = new Guid().SequentialGuid();
                    tblLanguages.Name = "Persian(IR)";
                    tblLanguages.NativeName = "فارسی(ایران)";
                    tblLanguages.Code = "fa-IR";
                    tblLanguages.Abbr = "fa";
                    tblLanguages.IsActive = true;
                    tblLanguages.IsRtl = true;
                    tblLanguages.UseForSiteLanguage = true;

                    await _languageRepository.AddAsync(tblLanguages);
                }

                if (!await _languageRepository.GetNoTraking.AnyAsync(a => a.Name == "English(US)"))
                {
                    await _languageRepository.AddAsync(new TblLanguages
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "English(US)",
                        NativeName = "English(USA)",
                        Code = "en-US",
                        Abbr = "en",
                        IsActive = true,
                        IsRtl = true,
                        UseForSiteLanguage = true
                    });
                }
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
