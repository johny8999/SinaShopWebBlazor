using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using FrameWork.Infrastructure;
using ILogger = FrameWork.Infrastructure.ILogger;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.WebApp.Common.Utilities.MessageBox;

namespace SinaShop.WebApp.Pages.Auth.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;

        public RegisterModel(ILogger logger, IMsgBox msgBox)
        {
            _Logger = logger;
            _MsgBox = msgBox;
        }
        public IActionResult OnGet(string ReturnUrl = null)
        {
            //ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Name}/Auth/Login";
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/Login";
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return _MsgBox.SucssessDefultMsg();
        }


        //[BindProperty(SupportsGet =true)] //for get in worked
        [BindProperty]
        public viRegister Input { get; set; }
    }
}
