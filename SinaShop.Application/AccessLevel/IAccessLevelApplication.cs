using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;

namespace SinaShop.Application.AccessLevel
{
    public interface IAccessLevelApplication
    {
        Task<string> GetIdByNameAsync(InpGetByIdName input);
    }
}