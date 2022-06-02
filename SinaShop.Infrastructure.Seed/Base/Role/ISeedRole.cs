using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Seed.Base.Role
{
    public interface ISeedRole
    {
        Task<bool> RunAsync();
    }
}
