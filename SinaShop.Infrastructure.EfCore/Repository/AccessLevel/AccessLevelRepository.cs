using Microsoft.EntityFrameworkCore;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Repository.AccessLevel
{
    public class AccessLevelRepository : BaseRepository<tblAccessLevel>, IAccessLevelRepository
    {
        public AccessLevelRepository(MainContext context) : base(context)
        {
        }
    }
}
