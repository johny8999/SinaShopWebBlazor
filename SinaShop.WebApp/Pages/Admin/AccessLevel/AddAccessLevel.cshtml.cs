using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Localizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.WebApp.Common.Utilities.MessageBox;
using ILogger = FrameWork.Infrastructure.ILogger;

namespace SinaShop.WebApp.Pages.Admin.AccessLevel
{
    [Authorize]
    public class AddAccessLevelModel : PageModel
    {
        private readonly IAccessLevelApplication _AccessLevelApplication;
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        public AddAccessLevelModel(IAccessLevelApplication accessLevelApplication
            , ILogger logger
            , ILocalizer localizer
            , IMsgBox msgBox
            , IServiceProvider serviceProvider)
        {
            _AccessLevelApplication = accessLevelApplication;
            _Logger = logger;
            _Localizer = localizer;
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                {

                }
                #endregion Validation

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.FailMsg(_Localizer[ex.Message]);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FailMsg(_Localizer["Error500"]);
            }
        }

        public viAddAccessLevel input { get; set; }
    }
}
