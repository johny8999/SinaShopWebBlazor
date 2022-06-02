using SinaShop.Application.Contract.ApplicationDTO.Languages;
using SinaShop.Application.Contract.ApplicationDTO.Result;

namespace SinaShop.Application.Languages
{
    public interface ILanguagesApplication
    {
        Task<OperationResult> AddLanguageAsync(InpAddLanguage Input);
        Task<string> GetCodeByAbbrAsync(string Abbr);
        Task<List<OutSiteLanguageCache>> GetAllLanguageSiteLangAsync();
        Task<bool?> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang input);
    }
}