using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;

namespace SinaShop.WebApp.Pages.Admin.Components;

public class Compo_RolesListModel : PageModel
{
    [Authorize]
    public async Task<IActionResult> OnGetAsync()
    {

    }
    [BindProperty(SupportsGet =true)]
    public viCompoListRoles input { get; set; }
}
