using FrameWork.Application.Services.Localizer;
using Kendo.Mvc.UI.Fluent;

namespace SinaShop.WebApp.Common.ExMethod
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> builder, ILocalizer localizer) where T : class
        {
            return builder.Pageable(a =>
            {
                a.Messages(msg =>
                {
                    msg.Display(localizer["TelerikPageToFrom"]);
                    msg.Empty(localizer["ItemNotFound"]);
                    msg.ItemsPerPage(localizer["TelerikItemsPerPage"]);
                    msg.Of(localizer["TelerikOf"]);
                    msg.MorePages(localizer["TelerikMorePages"]);
                    msg.Refresh(localizer["Refresh"]);
                    msg.Previous(localizer["Previous"]);
                    msg.Next(localizer["Next"]);
                    msg.First(localizer["First"]);
                    msg.Page(localizer["Page"]);
                    msg.Last(localizer["Last"]);
                });
                a.AlwaysVisible(true);
                a.ButtonCount(5);
                a.Input(false);
                a.PreviousNext(true);
                a.Responsive(true);
            });
        }
    }
}
