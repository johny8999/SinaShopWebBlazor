using Microsoft.Extensions.Localization;
using System.Reflection;

namespace SinaShop.WebApp.Localization
{
    public class FactoryLocalizer
    {
        public IStringLocalizer Set(IStringLocalizerFactory factory, Type type)
        {
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResources", assemblyName.Name);
        }

        public IStringLocalizer Set(IServiceCollection Services, Type type)
        {
            var factory = Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResources", assemblyName.Name);
        }
    }
}
