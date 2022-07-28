using SinaShop.Application.Contract.ApplicationDTO.Result;
using SinaShop.Application.Contract.ApplicationDTO.Role;

namespace SinaShop.Application.Roles
{
    public interface IRoleApplication
    {
        Task<OperationResult<List<OutGetAllRolesByParentId>>> GetAllRolesByParentIdAsync(InpGetAllRolesByParentId Input);
        Task<IList<string>> GetRolesByUserAsync(InpGetRolesByUser input);
    }
}