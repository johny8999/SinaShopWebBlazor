using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Email;
using FrameWork.Application.Services.Localizer;
using FrameWork.Consts;
using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Domain.Users.UserAgg.Entities;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword input)
    {
        try
        {
            #region Validations
            input.CheckModelState(_ServiceProvider);
            #endregion Validations

            #region Register
            {
                var _result = await RegisterAsync(new InpRegister()
                {
                    Email = input.Email,
                    FullName = input.FullName,
                    Password = input.Password,

                });
                if (_result.IsSuccess)
                {
                    #region Send Confirmation Email
                    {

                        string GenerateLink = "";
                        #region Generate Confirmation Link
                        {

                            var user = await _UserRepository.FindByIdAsync(_result.Message);
                            var code = await _UserRepository.GenerateEmailConfirmationTokenAsync(user);

                            string EncryptedToken = $"{_result.Message},{code}".AesEncpypt(AuthConst.SecretKey);
                            EncryptedToken = WebUtility.UrlEncode(EncryptedToken);
                            GenerateLink = input.ConfirmationLinkTemplate.Replace("[TOKEN]", EncryptedToken);

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
                            await _EmailSender.SendAsync(input.Email, SiteSettingConst.SiteName + _Localizer["EmailConfirm"], ConfirmationEmailTemplate);
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
                    .GetIdByNameAsync(new InpGetByIdName() { Name = "NotConfirmedUser" })).ToGuid();
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

    public async Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation

            #region Decrypted Token
            string DecryptedToken = input.Token.AesDecrypt(AuthConst.SecretKey);
            string UserId = DecryptedToken.Split(',')[0];
            string Token = DecryptedToken.Split(',')[1];
            #endregion Decrypted Token

            #region Find User
            tblUsers qUser = null;
            {
                qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser is null)
                    return new OperationResult().Failed("Token is invalid");
            }
            #endregion Find User

            #region Confirm Email
            {
                var Result = await _UserRepository.ConfirmEmailAsync(qUser, Token);
                if (!Result.Succeeded)
                    return new OperationResult().Failed(String.Join(',', Result.Errors));
            }
            #endregion Confirm Email

            #region Change user access level to confirmedUser
            {
                var ConfirmAccessLevelId = await _IAccessLevelApplication.GetAccessLevelNameByIdAsync(new InpGetAccessLevelNameById()
                {
                    AccessLevelName = "ConfirmedUser"
                });

                await ChangeUserAccesslevelAsync(new InpChangeUserAccesslevel()
                {
                    UserId = UserId,
                    AccessLevelId = ConfirmAccessLevelId
                });
            }
            #endregion Change user access level to confirmedUser
            return new OperationResult().Successed("Email Confirmation has been Succssed");
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }

    public async Task<OperationResult> ChangeUserAccesslevelAsync(InpChangeUserAccesslevel input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation

            #region Get User
            tblUsers qUser = new();
            {
                qUser = await _UserRepository.FindByIdAsync(input.UserId);
                if (qUser is null)
                    return new OperationResult().Failed(_Localizer["The user has not been found"]);
            }
            #endregion Get User

            #region Change user access level
            {
                qUser.AccessLevelId = input.AccessLevelId.ToGuid();
                await _UserRepository.UpdateAsync(qUser);
            }
            #endregion Change user access level

            #region Change user role
            {
                var qUserRole = await ChangeUserRolByAccessLevelIdAsync(new InpChangeUserRolByAccessLevelId()
                {
                    UserId = input.UserId,
                    AccessLevelId = input.AccessLevelId
                });
            }
            #endregion Change user role

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
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }

    public async Task<OperationResult> ChangeUserRolByAccessLevelIdAsync(InpChangeUserRolByAccessLevelId input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation

            #region Get user
            tblUsers tUser = new();
            {
                tUser = await _UserRepository.FindByIdAsync(input.UserId);
            }
            #endregion Get user

            #region Remove old roles
            {
                var qOldRoles = await _UserRepository.GetRolesAsync(tUser);
                var Result = await _UserRepository.RemoveFromRolesAsync(tUser, qOldRoles);
                if (!Result.Succeeded)
                    return new OperationResult().Failed(String.Join(',', Result.Errors.Select(a => a.Description)));
            }
            #endregion Get roles

            #region Add New Roles
            {
                var qNewRoles = await _IAccessLevelApplication
                .GetUserRollesByAccessLevelAsync(new InpGetUserRollesByAccessLevel()
                {
                    AccessLevelId = input.AccessLevelId,
                });

                var Result = await _UserRepository.AddToRolesAsync(tUser, qNewRoles);
                if (!Result.Succeeded)
                    return new OperationResult().Failed(String.Join(',', Result.Errors.Select(a => a.Description)));
            }
            #endregion Add New Roles

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
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }

    public async Task<OperationResult> LogInByEmailPasswordAsync(InpLoginByEmailPassword input)
    {
        try
        {
            var qUser = await _UserRepository.FindByEmailAsync(input.Email);
            if (qUser is null)
                return new OperationResult().Failed(_Localizer["Username or password is incorect"]);

            var Result = await LogInAsync(new InpLgIn() { UserId = qUser.Id.ToString(), Password = input.Password });

            if (Result.IsSuccess)
                return Result;

            else
                return new OperationResult().Failed(Result.Message);

        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }
    private async Task<OperationResult> LogInAsync(InpLgIn input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation
            var quser = await _UserRepository.FindByIdAsync(input.UserId);
            if (quser is null)
                return new OperationResult().Failed(_Localizer["Username or password is incorect"]);

            if (quser.IsActive == false)
                return new OperationResult().Failed(_Localizer["Your Account is disable"]);

            var Result = await _UserRepository.PasswordSignInAsync(quser, input.Password, false, true);
            if (Result.Succeeded)
                return new OperationResult().Successed(quser.Id.ToString());

            else if (Result.IsLockedOut)
                return new OperationResult().Failed(_Localizer["Your Account is IsLockedOut"]);

            else
                return new OperationResult().Failed(_Localizer["Username or password is incorect"]);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }

    public async Task<OutGetAllDetails> GetAllDetailsAsync(InpGetAllDetails input)
    {
        try
        {
            #region Validation
            {
                input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            return await _UserRepository.GetNoTraking.Where(a => a.Id == input.UserId.ToGuid()).Select(a => new OutGetAllDetails()
            {
                Id = a.Id.ToString(),
                UserName = a.UserName,
                AccessLevelId = a.AccessLevelId.ToString(),
                Date = a.Date,
                Email = a.Email,
                FullName = a.Fullname,
                IsActive = a.IsActive,
                PhoneNumber = a.PhoneNumber,
                AccessLevelTitle = a.tblAccessLevel.Name

            }).SingleOrDefaultAsync();

        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            throw new ArgumentInvalidException();
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            throw new Exception();
        }
    }
    public async Task<tblUsers> FindUserById(string Id)
    {
        try
        {
            return await _UserRepository.FindByIdAsync(Id);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            throw new ArgumentInvalidException();
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            throw new Exception();
        }
    }

    public async Task<tblUsers> FindUserByEmail(string Email)
    {
        return await _UserRepository.FindByEmailAsync(Email);
    }
    public async Task<OperationResult> ForgetPasswordAsync(InpForgetPassword input)
    {
        try
        {
            #region Validation
            {
                input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region Get user
            tblUsers quser = null;
            {
                quser = await _UserRepository.FindByEmailAsync(input.Email);

                if (quser is null)
                    return new OperationResult().Failed("Email not found");

                if (!quser.IsActive)
                    return new OperationResult().Failed("Email not found");

                if (!quser.EmailConfirmed)
                    return new OperationResult().Failed("Email not found");
            }
            #endregion Get user

            #region Generated Token
            string Token = null;
            
            {
                Token = await _UserRepository.GeneratePasswordResetTokenAsync(quser);
                Token = quser.Id + ", " + Token;
                Token = Token.AesEncpypt(AuthConst.SecretKey);
                Token = WebUtility.UrlEncode(Token);
            }
            #endregion Generated Token

            #region GenerateLink
            string GenerateLink = null;
            {
                GenerateLink = input.ForgetPasswordUrl.Replace("[TOKEN]".ToUpper(), Token);
            }
            #endregion GenerateLink

            #region Generate Email Template
            string GenerateEmailTemplate = $"<a href=\"[Link]\">{_Localizer["ClickMe"]}</a>";
            {
                GenerateEmailTemplate = GenerateEmailTemplate.Replace("[Link]", GenerateLink);
            }
            #endregion Generate Email Template

            #region SendEmail
            {
                await _EmailSender.SendAsync(input.Email, AuthConst.Issuer + _Localizer["PasswordRecovery"], GenerateEmailTemplate);
            }
            #endregion SendEmail
            return new OperationResult().Successed("Email has been sent and you should click link");
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new  OperationResult().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult().Failed(_Localizer["Error500"]);
        }
    }

    public async Task<OperationResult> ResetPasswordAsync(InpResetPassword input)
    {
        try

        {
            #region Validation
            {
                input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region GenerateToken
            string Token = null;
            string UserId = null;
            {
                Token = input.Token.AesDecrypt(AuthConst.SecretKey);
                UserId = Token.Split(", ")[0];
                Token = Token.Split(", ")[1];
            }
            #endregion GenerateToken

            #region GenerateUser
            tblUsers qUser = null;
            {
                qUser = await _UserRepository.FindByIdAsync(UserId);
            }
            #endregion GenerateUser

            #region Reset password
            {
                var Result = await _UserRepository.ResetPasswordAsync(qUser, Token, input.Password);
                if (Result.Succeeded)
                    return new OperationResult().Successed(_Localizer["Your password has been changed successly"]);

                else
                    return new OperationResult().Failed(String.Join(',', Result.Errors.Select(a => a.Description)));
            }
            #endregion Reset password
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return null;
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return null;
        }
    }

    public async Task SignOut()
    {
        try
        {
            
            await _UserRepository.SignOutAsync();
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
        }
    }
}

