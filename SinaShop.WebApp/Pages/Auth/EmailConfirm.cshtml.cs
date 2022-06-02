using FrameWork.Application.Services.Localizer;
using FrameWork.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.UserAplication;

namespace SinaShop.WebApp.Pages.Auth
{
    public class EmailConfirmModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        private readonly ILocalizer _Localizer;

        public EmailConfirmModel(IUserApplication userApplication, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(_Localizer["Token can not be null"]);
            var Result = await _UserApplication.EmailConfirmationAsync(new InpEmailConfirmation()
            {
                Token = token
            });
            if (Result.IsSuccess)
            {
                Response.DeleteAuthCookie();
                IsSuccess = true;
                return Page();
            }
            else
            {
                IsSuccess = false;
                return Page();
            }
        }
        public bool IsSuccess { get; set; }
    }
}
