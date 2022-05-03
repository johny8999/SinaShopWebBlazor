using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Domain.Users.UserAgg.Entities;

namespace SinaShop.Application.UserAplication;
    public interface IUserApplication
    {
    Task<OperationResult> ChangeUserAccesslevelAsync(InpChangeUserAccesslevel input);
    Task<OperationResult> ChangeUserRolByAccessLevelIdAsync(InpChangeUserRolByAccessLevelId input);
    Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation input);
    Task<tblUsers> FindUserByEmail(string Email);
    Task<tblUsers> FindUserById(string Id);
    Task<OutGetAllDetails> GetAllDetailsAsync(InpGetAllDetails input);
    Task<OperationResult> LogInByEmailPasswordAsync(InpLoginByEmailPassword input);
    Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input);
    }
