using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;

namespace SinaShop.Application.AccessLevel
{
    public interface IAccessLevelApplication
    {
        Task<string> GetAccessLevelNameByIdAsync(InpGetAccessLevelNameById input);
        Task<string> GetIdByNameAsync(InpGetByIdName input);
        Task<List<string>> GetUserRollesByAccessLevelAsync(InpGetUserRollesByAccessLevel input);
    }
}