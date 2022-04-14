using FrameWork.Application.Services.Localizer;
using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.AccessLevel
{
    public class AccessLevelApplication : IAccessLevelApplication
    {
        private readonly IAccessLevelRepository _AccessLevelRepository;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;

        public AccessLevelApplication(IAccessLevelRepository accessLevelRepository, IServiceProvider serviceProvider, ILogger logger, ILocalizer localizer)
        {
            _AccessLevelRepository = accessLevelRepository;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _Localizer = localizer;
        }
        public async Task<string> GetIdByNameAsync(InpGetByIdName input)
        {
            try
            {
                input.CheckModelState(_ServiceProvider);
                 return _AccessLevelRepository.GetNoTraking.Where(a => a.Name == input.Name).SingleOrDefault().Id.ToString();
            }
            catch (ArgumentException ex)
            {
                _Logger.Debug(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(_Localizer["Error500"]);
                return null;
            }
        }
    }
}
