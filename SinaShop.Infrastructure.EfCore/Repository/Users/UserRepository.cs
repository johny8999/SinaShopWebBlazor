using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Domain.Users.UserAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;

namespace SinaShop.Infrastructure.EfCore.Repository.Users
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {

        public UserRepository(MainContext context) : base(context)
        {

        }
    }
}
