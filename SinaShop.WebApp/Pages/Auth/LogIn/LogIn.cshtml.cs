using AutoMapper;
using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Localizer;
using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.JwtDTO;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.UserAplication;
using SinaShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;
using SinaShop.WebApp.Common.Utilities.MessageBox;

namespace SinaShop.WebApp.Pages.Auth.LogIn
{
    public class LogInModel : PageModel
    {
        private readonly FrameWork.Infrastructure.ILogger _logger;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IJwtBuilder _JwtBuilder;
        public LogInModel(FrameWork.Infrastructure.ILogger logger, ILocalizer localizer, IMsgBox msgBox, IUserApplication userApplication, IMapper mapper, IServiceProvider serviceProvider, IJwtBuilder jwtBuilder)
        {
            _logger = logger;
            _Localizer = localizer;
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Mapper = mapper;
            _ServiceProvider = serviceProvider;
            _JwtBuilder = jwtBuilder;
        }

        public Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation
                var MapperData = _Mapper.Map<InpLoginByEmailPassword>(input);
                var Result = await _UserApplication.LogInByEmailPasswordAsync(MapperData);

                if (Result.IsSuccess)
                {
                    var GetUser = await _UserApplication.FindUserByEmail(input.Email);
                    var GeneratedToken = await _JwtBuilder.CreateTokenAsync(new InpCreateToken() { UserId=GetUser.Id.ToString()});
                    Response.CreateAuthCookie(GeneratedToken,input.RmemberMe);
                    return _MsgBox.SucssessMsg(_Localizer[Result.Message]);
                }
                else
                    return _MsgBox.FailMsg(_Localizer[Result.Message]);


            }
            catch (ArgumentInvalidException ex)
            {
                _logger.Debug(ex.Message);
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return _MsgBox.ModelStateMsg(_Localizer["Error500"]);
            }
        }
        [BindProperty]
        public viLogIn input { get; set; }
    }

}
