using FrameWork.Application.Services.Localizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.ApplicationDTO.Role;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.Roles;
using SinaShop.WebApp.Common.Utilities.MessageBox;
using ILogger = FrameWork.Infrastructure.ILogger;

namespace SinaShop.WebApp.Pages.Admin.Components;

public class Compo_RolesListModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IMsgBox _msgBox;
    private readonly ILocalizer _Localizer;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IRoleApplication _RoleApplication;

    public Compo_RolesListModel(ILogger logger
        , IMsgBox msgBox
        , ILocalizer localizer
        , IServiceProvider serviceProvider
        , IRoleApplication roleApplication)
    {
        _logger = logger;
        _msgBox = msgBox;
        _Localizer = localizer;
        _ServiceProvider = serviceProvider;
        _RoleApplication = roleApplication;
    }

    [Authorize]
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {

            if (input.AccessLevelId != null)
            {
                var Result = await _RoleApplication.GetAllRolesByParentIdAsync(new InpGetAllRolesByParentId
                {
                    ParentId = input.AccessLevelId
                });

                if (Result.IsSuccess)
                    ViewData["SelectedRoles"] = Result.Data;
                else
                    return BadRequest(Result.Message);
            }

            return Page();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    [BindProperty(SupportsGet = true)]
    public viCompoListRoles input { get; set; }
}
