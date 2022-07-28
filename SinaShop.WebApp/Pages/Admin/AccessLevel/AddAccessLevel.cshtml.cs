using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;

namespace SinaShop.WebApp.Pages.Admin.AccessLevel
{
    public class AddAccessLevelModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            return Page();
        }

        [BindProperty]
        public viAddAccessLevel Input { get; set; }
    }
}
