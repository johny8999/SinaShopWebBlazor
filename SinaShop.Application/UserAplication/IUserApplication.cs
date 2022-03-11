using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;

namespace SinaShop.Application.UserAplication;
    public interface IUserApplication
    {
        Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input);
    }
