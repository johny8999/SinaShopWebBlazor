using FrameWork.Application.Services.Localizer;
using FrameWork.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.UserAplication;
using System.Globalization;

namespace SinaShop.WebApp.Pages.Auth.RecoveryPassword
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;

        public ResetPasswordModel(IServiceProvider serviceProvider, ILocalizer localizer, IUserApplication userApplication)
        {
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _UserApplication = userApplication;
        }

        public IActionResult OnGet(string returnUrl = null, string token = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/LogIn";
            input = new ViResetPassword()
            {
                Token = token
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try

            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation
                await _UserApplication.ResetPasswordAsync(new InpResetPassword()
                {
                    Password=input.Password,
                    RePassword=input.RePassword,
                    Token=input.Token
                });
                return Page();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [BindProperty]
        public ViResetPassword input { get; set; }
    }
}
