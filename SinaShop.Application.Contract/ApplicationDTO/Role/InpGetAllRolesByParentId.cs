using FrameWork.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.Role;

public class InpGetAllRolesByParentId
{
    [Display(Name = nameof(ParentId))]
    [GUID]
    public string ParentId { get; set; }
}
