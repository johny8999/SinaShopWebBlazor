global using FrameWork.Infrastructure;
using FrameWork.Application.Arguments;
using FrameWork.ExMethods;
using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Domain.Users.UserAgg.Contracts;

namespace SinaShop.Application.UserAplication;
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserRepository _UserRepository;
        public UserApplication(ILogger logger, IServiceProvider serviceProvider, IUserRepository userRepository)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _UserRepository = userRepository;
        }

    public async Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input)
    {
        try
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion Validations

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

    private async Task<OperationResult> RegisterAsync(InpRegisterByEmailPassword Input)
    {
        try
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion Validations

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

