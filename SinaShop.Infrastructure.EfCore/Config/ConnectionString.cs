using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Config
{
    public static class ConnectionString
    {
        public static string Get()
        {
            const string SqlConnection= "Server=.;Database=SinaShopDB;Trusted_Connection=True;";
            //const string SqlConnection= "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"; 
            return SqlConnection;
        }
    }
}
