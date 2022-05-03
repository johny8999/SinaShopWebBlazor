using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;

namespace SinaShop.Infrastructure.EfCore.Repository.AccessLevel
{
    public class AccessLevelRepository : BaseRepository<tblAccessLevel>, IAccessLevelRepository
    {
        public AccessLevelRepository(MainContext context) : base(context)
        {
        }
    }
}
