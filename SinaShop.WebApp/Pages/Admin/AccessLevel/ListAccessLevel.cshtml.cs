using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.Contract.PresentationDTO.ViewModels;

namespace SinaShop.WebApp.Pages.Admin.AccessLevel
{
    public class ListAccessLevelModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest Request)
        {

        }

        public viListAccessLevelModel input { get; set; }
        public vmListAccessLevelModel Data { get; set; }
    }
}
