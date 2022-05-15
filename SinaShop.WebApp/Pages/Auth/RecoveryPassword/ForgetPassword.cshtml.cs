using AutoMapper;
using FrameWork.Application.Services.Localizer;
using FrameWork.Consts;
using FrameWork.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.JwtDTO;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.UserAplication;
using SinaShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;
using SinaShop.WebApp.Common.Utilities.MessageBox;
using System.Globalization;
using ILogger = FrameWork.Infrastructure.ILogger;

namespace SinaShop.WebApp.Pages.Auth.RecoveryPassword
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        private readonly IMapper _Mapper;
        private readonly IJwtBuilder _JwtBuilder;

        public ForgetPasswordModel(IServiceProvider serviceProvider, ILogger logger, ILocalizer localizer, 
            IMsgBox msgBox, IMapper mapper, IJwtBuilder jwtBuilder, IUserApplication userApplication)
        {
            _serviceProvider = serviceProvider;
            _Logger = logger;
            _Localizer = localizer;
            _MsgBox = msgBox;
            _Mapper = mapper;
            _JwtBuilder = jwtBuilder;
            _UserApplication = userApplication;
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/LogIn";
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                {
                    input.CheckModelState(_serviceProvider);
                }
                #endregion Validation
                var qUser =await _UserApplication.FindUserByEmail(input.Email);
                //var GeneratedToken = await _JwtBuilder.CreateTokenAsync(new InpCreateToken() { UserId = qUser.Id.ToString() });

                
                var Result = await _UserApplication.ForgetPasswordAsync(new InpForgetPassword()
                {
                    Email = input.Email,
                    ForgetPasswordUrl = $"{SiteSettingConst.SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/ResetPassword?Token=[TOKEN]"
                });

                if (Result.IsSuccess)
                    return _MsgBox.SucssessMsg(_Localizer[Result.Message], "goToReturnUrl()");

                else
                    return _MsgBox.FailDefultMsg(_Localizer[Result.Message]);
            }
            catch (ArgumentNullException  ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.FailMsg(_Localizer["Error500"]);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FailMsg(ex.Message);
            }
        }
        [BindProperty]
        public viForgetPassword input { get; set; }
    }
}
