using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace SinaShop.WebApp.Common.Types
{
    public class JsResult : ContentResult
    {
        public JsResult(string Script)
        {
            Content = Script;
            ContentType = "application/javascript";
        }
    }
}
