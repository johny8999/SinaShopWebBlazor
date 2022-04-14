using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.UserAplication;

namespace SinaShop.WebApp.Pages.Auth
{
    public class EmailConfirmModel : PageModel
    {
        private readonly IUserApplication _UserApplication;

        public EmailConfirmModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public void OnGet()
        {

        }
    }
}
