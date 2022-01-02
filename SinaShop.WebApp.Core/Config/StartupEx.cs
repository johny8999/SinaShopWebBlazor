namespace SinaShop.WebApp.Core.Config
{
    public static class StartupEx
    {
        public static IMvcBuilder AddRazorPage(this IServiceCollection services)
        {
            return services.AddRazorPages(opt =>
            {
                opt.Conventions.AddPageRoute("/Home/Index", "");
            });

        }
    }
}
