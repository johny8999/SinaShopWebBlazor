using SinaShop.Application.Contract.Languages;
using SinaShop.Application.Contract.Result;
using SinaShop.Domain.Region.LanguageAgg.Contract;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinaShop.Domain.Region.LanguageAgg.Entities;
using FrameWork.ExMethods;
using Microsoft.EntityFrameworkCore;
using FrameWork.Infrastructure;

namespace SinaShop.Application.Languages
{
    public class LanguagesApplication : ILanguagesApplication
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger _logger;
        private List<OutSiteLanguageCache> _siteLangCach;

        public LanguagesApplication(ILanguageRepository languageRepository, ILogger logger)
        {
            _languageRepository = languageRepository;
            _logger = logger;
        }

        public async Task<OperationResult> AddLanguageAsync(InpAddLanguage Input)
        {

            try
            {
                if (Input is null)
                {
                    throw new ArgumentNullException(nameof(Input));
                }

                if (await CheckExistAsync(Input.Name, Input.NativeName, Input.Code, Input.Abbr))
                    return new OperationResult().Failed("LanguageIsDuplicate");

                TblLanguages tblLanguages = new();
                tblLanguages.Id = new Guid().SequentialGuid();
                tblLanguages.Name = Input.Name;
                tblLanguages.Code = Input.Code;
                tblLanguages.IsRtl = Input.IsRtl;
                tblLanguages.IsActive = Input.IsActive;
                tblLanguages.Abbr = Input.Abbr;
                tblLanguages.NativeName = Input.NativeName;
                tblLanguages.UseForSiteLanguage = Input.UseForSiteLanguage;


                await _languageRepository.AddAsync(tblLanguages);
                return new OperationResult().Successed();
            }
            catch (ArgumentNullException ex)
            {

                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

       private async Task<bool> CheckExistAsync(string Name = null, string NativeName = null, string Code = null, string Abbr = null)
        {
            if (Name == null || NativeName == null || Code == null || Abbr == null)
                throw new ArgumentNullException("you must enter an argument");

            return await _languageRepository.GetNoTraking
                    .Where(a => Name != null ? a.Name == Name : true)
                    .Where(a => NativeName != null ? a.NativeName == NativeName : true)
                    .Where(a => Code != null ? a.Code == Code : true)
                    .Where(a => Abbr != null ? a.Abbr == Abbr : true)
                    .AnyAsync();
        }

        public async Task<string> GetCodeByAbbrAsync(string Abbr)
        {
            try
            {
                await LoadCacheAsync();
                return _siteLangCach.Where(a => a.Abbr.Equals(Abbr))
                             .Select(a => a.Code).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private async Task LoadCacheAsync()
        {
            if (_siteLangCach is null)
            {
                _siteLangCach = await _languageRepository.Get
                                                        .Where(a => a.IsActive)
                                                        .Where(a => a.UseForSiteLanguage)
                                                        .Select(a => new OutSiteLanguageCache
                                                        {
                                                            Id = a.Id.ToString(),
                                                            Abbr = a.Abbr,
                                                            Code = a.Code,
                                                            IsRtl = a.IsRtl,
                                                            Name = a.Name,
                                                            NativeName = a.NativeName,
                                                            FlagUrl = ""
                                                        }).ToListAsync();
            }
        }

    

        public async Task<List<OutSiteLanguageCache>> GetAllLanguageSiteLangAsync()
        {
            await LoadCacheAsync();
            return _siteLangCach;
        }
    }
}
