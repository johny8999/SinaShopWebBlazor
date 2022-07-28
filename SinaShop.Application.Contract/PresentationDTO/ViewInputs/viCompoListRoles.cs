using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.PresentationDTO.ViewInputs;

public class viCompoListRoles
{
    [Display(Name = (nameof(AccessLevelId)))]
    [RequiredString]
    [GUID]
    public string AccessLevelId { get; set; }
}
