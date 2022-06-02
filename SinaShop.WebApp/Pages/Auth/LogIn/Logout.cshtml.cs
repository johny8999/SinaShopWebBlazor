using FrameWork.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.UserAplication;
using System.Globalization;

namespace SinaShop.WebApp.Pages.Auth.LogIn
{
    public class LogoutModel : PageModel
    {
        private readonly IUserApplication _UserApplication;

        public LogoutModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
               Response.DeleteAuthCookie();
                return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
    }
}
