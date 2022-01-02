namespace SinaShop.WebApp.Localization
{
    public interface ILocalizer
    {
        public string this[string name] { get; } // Localizer["Hi"]
        public string this[string name, params object[] arguments] { get; } // Localizer["Hi","Ali","Hassn"]
    }
}
