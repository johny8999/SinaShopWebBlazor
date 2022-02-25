using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using FrameWork.Infrastructure;
using ILogger = FrameWork.Infrastructure.ILogger;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;

namespace SinaShop.WebApp.Pages.Auth.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _Logger;

        public RegisterModel(ILogger logger)
        {
            _Logger = logger;
        }
        public IActionResult OnGet(string ReturnUrl = null)
        {
            //ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Name}/Auth/Login";
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/Login";
            return Page();
        }
        public void OnPost()
        {

        }
        public viRegister Input { get; set; }
    }
}
