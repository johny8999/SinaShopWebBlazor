
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrameWork.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SinaShop.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
