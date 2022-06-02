using SinaShop.Application.Contract.ApplicationDTO.Role;

namespace SinaShop.Application.Roles
{
    public interface IRoleApplication
    {
        Task<IList<string>> GetRolesByUserAsync(InpGetRolesByUser input);
    }
}