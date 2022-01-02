
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrameWork.Infrastructure;

namespace SinaShop.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly FrameWork.Infrastructure.ILogger _Logger;

        public IndexModel(FrameWork.Infrastructure.ILogger logger)
        {
            _Logger = logger;
        }

        public void OnGet()
        {
            try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
