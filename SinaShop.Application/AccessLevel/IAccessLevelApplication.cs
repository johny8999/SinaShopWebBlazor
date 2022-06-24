using FrameWork.Common.Utility.Paging;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;

namespace SinaShop.Application.AccessLevel
{
    public interface IAccessLevelApplication
    {
        Task<(OutPagingData PageData, List<OutGetAccessLevelForAdmin> LstItems)> GetAccessLevelForAdminAsync(InpGetAccessLevelForAdmin input);
        Task<string> GetAccessLevelNameByIdAsync(InpGetAccessLevelNameById input);
        Task<string> GetIdByNameAsync(InpGetByIdName input);
        Task<List<string>> GetUserRollesByAccessLevelAsync(InpGetUserRollesByAccessLevel input);
    }
}