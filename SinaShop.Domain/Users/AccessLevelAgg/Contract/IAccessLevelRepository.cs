using FrameWork.Domain;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using SinaShop.Domain.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.AccessLevelAgg.Contract
{
    public interface IAccessLevelRepository: IRepository<tblAccessLevel>
    {

    }
}
