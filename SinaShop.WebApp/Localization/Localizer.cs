using FrameWork.Application.Services.Localizer;
using Microsoft.Extensions.Localization;
using SinaShop.WebApp.Localization.Resources;

namespace SinaShop.WebApp.Localization
{
    public class Localizer : ILocalizer
    {
        public string this[string name] => Get(name);

        public string this[string name, params object[] arguments] => Get(name, arguments);
        private readonly IStringLocalizer _localizer;
   

        public Localizer(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
        public Localizer(IStringLocalizerFactory factory)
        {
            _localizer = new FactoryLocalizer().Set(factory, typeof(SharedResources));
        }

        private string Get(string Name)
        {
            return _localizer[Name];
        }
        private string Get(string Name, params object[] arguments)
        {
            return _localizer[Name, arguments];
        }
    }
}
