using SinaShop.Application.Contract.JwtDTO;

namespace SinaShop.Infrastructure.EfCore.Identity.JWT.JwtBuild
{
    public interface IJwtBuilder
    {
        Task<string> CreateTokenAsync(InpCreateToken input);
    }
}