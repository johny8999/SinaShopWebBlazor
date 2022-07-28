using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;

namespace SinaShop.Application.Contract.ApplicationDTO.Role;

public class InpGetRolesByUser
{
    [GUID]
    [RequiredString]
    public string UserId { get; set; }
}
