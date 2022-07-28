using FrameWork.Application.Arguments;
using FrameWork.Application.Services.Localizer;
using FrameWork.Common.ExMethods;
using FrameWork.Infrastructure;
using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.Role;
using SinaShop.Application.UserAplication;
using SinaShop.Domain.Users.RoleAgg.Contract;
using SinaShop.Domain.Users.UserAgg.Entities;

namespace SinaShop.Application.Roles;

public class RoleApplication : IRoleApplication
{
    private readonly ILogger _Logger;
    private readonly IServiceProvider _ServiceProvider;
    private readonly ILocalizer _Localizer;
    private readonly IRoleRepository _RoleRepository;
    private readonly IUserApplication _UserApplication;

    public RoleApplication(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IRoleRepository roleRepository, IUserApplication userApplication)
    {
        _Logger = logger;
        _ServiceProvider = serviceProvider;
        _Localizer = localizer;
        _RoleRepository = roleRepository;
        _UserApplication = userApplication;
    }

    public async Task<IList<string>> GetRolesByUserAsync(InpGetRolesByUser input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation

            #region Get user
            tblUsers qUser = null;
            {
                qUser  = await _UserApplication.FindUserById(input.UserId);

                if (!qUser.IsActive)
                    throw new ArgumentInvalidException(_Localizer["your Account is disable"]);
                else if (qUser is null)
                    throw new ArgumentInvalidException(_Localizer["The user has not been finded"]);
            }
            #endregion Get user

            #region Get role
            IList<string> qRoles = null;
            {
               qRoles =await _RoleRepository.GetRolesAsync(qUser);
            }
            #endregion Get role
            return qRoles;
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

    public async Task<OperationResult<List<OutGetAllRolesByParentId>>> GetAllRolesByParentIdAsync(InpGetAllRolesByParentId Input)
    {
        try
        {

            #region Validation
            {
                Input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region Get Roles By ParentId
            List<OutGetAllRolesByParentId> qData = null;
            {
                qData = await _RoleRepository.GetNoTraking
                                        .Where(a => Input.ParentId != null ? a.ParentId == Input.ParentId.ToGuid() : true)
                                         .Select(a => new OutGetAllRolesByParentId
                                         {
                                             Id = a.Id.ToString(),
                                             name = a.Name,
                                             Description = a.Description
                                         })
                                         .ToListAsync();


            }
            #endregion Get Roles By ParentId
            return new OperationResult<List<OutGetAllRolesByParentId>>().Succeeded(qData);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult<List<OutGetAllRolesByParentId>>().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult<List<OutGetAllRolesByParentId>>().Failed("Error500");
        }
    }
}


