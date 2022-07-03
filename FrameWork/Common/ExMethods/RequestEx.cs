using Microsoft.AspNetCore.Http;
using System.Net;

namespace FrameWork.Common.ExMethods;

public static class RequestEx
{
    public static string GetCurrentEncodeUrl(this HttpRequest request)
    {
        return WebUtility.UrlEncode(request.GetCurrentUrl());
    }

    public static string GetCurrentUrl(this HttpRequest request)
    {
        string Url = request.Scheme + "://" + request.Host + "/" + request.Path;

        if (request.QueryString.HasValue)
            Url += "?" + request.QueryString.Value;

        return Url;
    }
}
