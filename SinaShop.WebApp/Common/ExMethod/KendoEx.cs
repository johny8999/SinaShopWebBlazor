using FrameWork.Application.Services.Localizer;
using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.WebApp.Common.ExMethod
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> Builder, ILocalizer Localizer) where T : class
        {
            return null;
        }
    }
}
