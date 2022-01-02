using SinaShop.Application.Contract.Languages;
using SinaShop.Application.Contract.Result;

namespace SinaShop.Application.Languages
{
    public interface ILanguagesApplication
    {
        Task<OperationResult> AddLanguageAsync(InpAddLanguage Input);
        Task<string> GetCodeByAbbrAsync(string Abbr);
        Task<List<OutSiteLanguageCache>> GetAllLanguageSiteLangAsync();

    }
}