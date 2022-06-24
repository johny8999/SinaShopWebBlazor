using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Localizer;
using FrameWork.Common.ExMethods;
using FrameWork.Common.Utility.Paging;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAccessLevelRoleRepository _AccessLevelRoleRepository;

        public AccessLevelApplication(IAccessLevelRepository accessLevelRepository, IServiceProvider serviceProvider, ILogger logger, ILocalizer localizer, IAccessLevelRoleRepository accessLevelRoleRepository)
        {
            _AccessLevelRepository = accessLevelRepository;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _Localizer = localizer;
            _AccessLevelRoleRepository = accessLevelRoleRepository;
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
        public async Task<List<string>> GetUserRollesByAccessLevelAsync(InpGetUserRollesByAccessLevel input)
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation

                return await _AccessLevelRoleRepository.GetNoTraking
                    .Where(a => a.AccessLevelId==input.AccessLevelId.ToGuid())
                    .Select(a=>a.RoleId.ToString() ).ToListAsync();
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
        public async Task<string> GetAccessLevelNameByIdAsync(InpGetAccessLevelNameById input)
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation
                return await _AccessLevelRepository.GetNoTraking
                    .Where(a => a.Name == input.AccessLevelName).Select(a => a.Id.ToString()).FirstOrDefaultAsync();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public async Task<(OutPagingData PageData, List<OutGetAccessLevelForAdmin> LstItems)> GetAccessLevelForAdminAsync(InpGetAccessLevelForAdmin input)
        {
            try
            {
                #region Validation
                {
                    input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region GetAccessLevel
                IQueryable<OutGetAccessLevelForAdmin> qData = null;
                {
                    qData = _AccessLevelRepository.GetNoTraking
                                    .Select(a => new OutGetAccessLevelForAdmin
                                    {
                                        Id = a.Id.ToString(),
                                        Name = a.Name,
                                        UserCount = a.tblUsers.Count()
                                    });

                }
                #endregion GetAccessLevel

                #region PagingDate
                OutPagingData PagingData = null;
                {
                    int CountAllItem = await qData.CountAsync();
                    PagingData = FrameWork.Common.Utility.Paging.PagingData.Calculate(CountAllItem, input.Page, input.Take);
                }
                #endregion PagingDate
                return (PagingData, await (qData.OrderBy(a => a.Name).Skip(PagingData.Skip).Take(PagingData.Take)).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

    }
}
