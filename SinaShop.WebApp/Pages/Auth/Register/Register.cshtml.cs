using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using FrameWork.Infrastructure;
using ILogger = FrameWork.Infrastructure.ILogger;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.WebApp.Common.Utilities.MessageBox;
using FrameWork.ExMethods;
using FrameWork.Application.Services.Localizer;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Application.UserAplication;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using FrameWork.Consts;

namespace SinaShop.WebApp.Pages.Auth.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;

        public RegisterModel(ILogger logger, IMsgBox msgBox, IServiceProvider serviceProvider,
            ILocalizer localizer, IUserApplication userApplication)
        {
            _Logger = logger;
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _UserApplication = userApplication;
        }
        public IActionResult OnGet(string ReturnUrl = null)
        {
            //ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Name}/Auth/Login";
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/Login";
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(viRegister input)
        {
            try
            {
                input.CheckModelState(_ServiceProvider);
                var _result =await _UserApplication.RegisterByEmailPasswordAsync(new InpRegisterByEmailPassword()
                {
                    Email = input.Email,
                    FullName = input.FullName,
                    Password=input.Password,
                    ConfirmationLinkTemplate= $"{SiteSettingConst.SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/EmailConfirm?Token=[TOKEN]"
                });
                if (_result.IsSuccess)
                {
                    return _MsgBox.SucssessMsg(_result.Message,
                        $"Location.href='{Url.Page("~/Pages/Auth/LogIn.Cshtml", new { Culture = CultureInfo.CurrentCulture.Parent.Name })}'");
                }
                else
                {
                    return _MsgBox.FailMsg(_result.Message);
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FailDefultMsg(_Localizer["Error500"]);
            }
        }


        //[BindProperty(SupportsGet =true)] //for get in worked
        [BindProperty]
        public viRegister Input { get; set; }
    }
}
