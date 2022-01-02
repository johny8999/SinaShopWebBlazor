using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Application.Services.IpList
{
    public interface IIPList
    {
        void AddRange(string fromIP, string toIP);
        bool CheckNumber(string ipNumber);
    }
}
