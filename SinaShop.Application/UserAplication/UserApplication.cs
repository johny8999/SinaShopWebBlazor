using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Email;
using FrameWork.Application.Services.Localizer;
using FrameWork.Consts;
using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Domain.Users.UserAgg.Entities;
using System.Text;

namespace SinaShop.Application.UserAplication;
public class UserApplication : IUserApplication
{
    private readonly ILogger _Logger;
    private readonly ILocalizer _Localizer;
    private readonly IEmailSender _EmailSender;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IUserRepository _UserRepository;
    private readonly IAccessLevelApplication _IAccessLevelApplication;
    public UserApplication(ILogger logger,
        IServiceProvider serviceProvider,
        IUserRepository userRepository,
        IAccessLevelApplication accessLevel, IEmailSender emailSender, ILocalizer localizer)
    {
        _Logger = logger;
        _ServiceProvider = serviceProvider;
        _UserRepository = userRepository;
        _IAccessLevelApplication = accessLevel;
        _EmailSender = emailSender;
        _Localizer = localizer;
    }

    public async Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input)
    {
        try
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion Validations

            #region Register
            {
                var _result = await RegisterAsync(new InpRegister()
                {
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Password = Input.Password,

                });
                if (_result.IsSuccess)
                {
                    #region Send Confirmation Email
                    {

                        string GenerateLink = "";
                        #region Generate Confirmation Link
                        {

                            var code = await _UserRepository.GenerateEmailConfirmationTokenAsync(await _UserRepository.FindByIdAsync(_result.Message));
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                            string EncryptedToken = $"{_result.Message},{code}".AesEncpypt(AuthConst.SecretKey);
                            EncryptedToken= WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptedToken));
                            GenerateLink = Input.ConfirmationLinkTemplate.Replace("[TOKEN]", EncryptedToken);

                        }
                        #endregion Generate Confirmation Link

                        #region Generate Email Template
                        string ConfirmationEmailTemplate = "<a href=[Link]>کلیک کنید</a>";
                        {
                            ConfirmationEmailTemplate = ConfirmationEmailTemplate.Replace("[Link]", GenerateLink);
                        }
                        #endregion Generate Email Template

                        #region Send Email
                        {
                            await _EmailSender.SendAsync(Input.Email, SiteSettingConst.SiteName + _Localizer["EmailConfirm"], ConfirmationEmailTemplate);
                        }
                        #endregion Send Email
                    }
                    #endregion Sen Confirmation Email
                    return new OperationResult().Successed();
                }
                else
                {
                    return new OperationResult().Failed(_result.Message);
                }
            }
            #endregion Register
            return new OperationResult().Successed();
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed("Error500");
        }
    }

    private async Task<OperationResult> RegisterAsync(InpRegister Input)
    {
        try
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion Validations

            #region Register new user
            tblUsers user = new();
            {
                user.Fullname = Input.FullName;
                user.Email = Input.Email;
                user.Date = DateTime.Now;
                user.IsActive = true;
                user.UserName = Input.Email;

                user.AccessLevelId = (await _IAccessLevelApplication
                    .GetIdByNameAsync(new InpGetByIdName() { Name= "NoConfirmUser" })).ToGuid();
            }

            var Result = await _UserRepository.AddAsync(user, Input.Password);
            if (Result.Succeeded)
            {
                return new OperationResult().Successed(user.Id.ToString());
            }
            else
            {
                return new OperationResult().Failed(String.Join(',', Result.Errors.Select(a => a.Description)));
            }
            #endregion Register new user

            return new OperationResult().Successed();
        }
        catch (ArgumentException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed("Error500");
        }
    }
}

