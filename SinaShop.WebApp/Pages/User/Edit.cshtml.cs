using AutoMapper;
using FrameWork.Application.Services.Localizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.UserAplication;
using SinaShop.WebApp.Common.Types;
using SinaShop.WebApp.Common.Utilities.MessageBox;
using System.Globalization;

namespace SinaShop.WebApp.Pages.User
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        public EditModel(IUserApplication userApplication,
            IMapper mapper, IMsgBox msgBox, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _Mapper = mapper;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var UserId = User.Identities.Select(a => a.Claims).First()
                .First().Value;
            var CurrentUser = await _UserApplication.FindUserById(UserId);
            input = new viEditUser()
            {
                UserId = UserId,
                Email = CurrentUser.Email,
                FullName = CurrentUser.Fullname,
                PhoneNumber = CurrentUser.PhoneNumber,
                UserName = CurrentUser.UserName
            };

            return Page();
            return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/LogIn");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var MapperData = _Mapper.Map<InpEditUser>(input);
            var Result = await _UserApplication.EditUser(MapperData);

            if (Result.IsSuccess)
                return new JsResult($"location.href ='/{CultureInfo.CurrentCulture.Parent.Name}'");
            else
                return _MsgBox.FailMsg(_Localizer[Result.Message ?? ""]);

        }

        [BindProperty]
        public viEditUser input { get; set; }
    }
}
