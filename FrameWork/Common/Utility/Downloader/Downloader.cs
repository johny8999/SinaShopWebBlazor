using FrameWork.Common.ExMethods;
using FrameWork.Common.Utility.Downloader.Dto;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace FrameWork.Common.Utility.Downloader;

public class Downloader : IDownloader
{
    private readonly ILogger _Logger;
    private readonly IServiceProvider _ServiceProvider;

    public Downloader(ILogger logger, IServiceProvider serviceProvider)
    {
        _Logger = logger;
        _ServiceProvider = serviceProvider;
    }
    public async Task<string> GetHtmlAsync(InpGetHtmlForPage input)
    {
        try
        {
            #region Validation
            {
                input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region GenerateUrl
            string UrlParameter = string.Empty;
            string Url = string.Empty;
            {
                if (input.Data is not null)
                  UrlParameter = UrlEncodedParametrGenerator(input.Data);

                Url = input.PageUrl + UrlParameter;
            }
            #endregion GenerateUrl

            #region Parameter Amount => مقدار دهی به پارامتر
            HttpWebRequest ObjRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
            {
                ObjRequest.ContentType = "text/html; charset=utf-8";
                ObjRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.115 Safari/537.36 OPR/88.0.4412.53";
                ObjRequest.Method = "GET";
            }
            #endregion Parameter Amount => مقدار دهی به پارامتر

            #region Add Headers
            {
                if (input.Headers is not null)
                    foreach (var Header in input.Headers)
                        ObjRequest.Headers.Add(Header.Key, Header.Value);

                ObjRequest.Headers.Add("Accept-charset", "ISO-8859-9,URF-8;q=0.7,*;q=0.7");
                ObjRequest.Headers.Add("Accept-Encoding", "deflate");
            }
            #endregion Add Headers

            HttpWebResponse Response = (HttpWebResponse)await ObjRequest.GetResponseAsync();

            string Result = null;
            using (StreamReader stream = new(Response.GetResponseStream(), Encoding.UTF8))
            {
                Result = await stream.ReadToEndAsync();
                stream.Close();
            }
            return Result;
        }

        catch (ArgumentNullException ex)
        {
            _Logger.Debug(ex);
            return null;
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return null;
        }
    }

    private string UrlEncodedParametrGenerator(object Data)
    {
        if (Data is null)
            return string.Empty;

        Dictionary<string, string> Parameter = GetModelParameter(Data);
        string UrlParameter = "?";

        foreach (var item in Parameter)
            if (item.Value is not null)
                UrlParameter += "&" + item.Key + "=" + item.Value.ToString();

        return UrlParameter;
    }

    private Dictionary<string, string> GetModelParameter(object data)
    {
        data = data ?? new Dictionary<string, string>();
        Type t = data.GetType();
        PropertyInfo[] Props = t.GetProperties();
        Dictionary<string, string> LstParameters = new();

        foreach (var Prop in Props)
        {
            object Value = Prop.GetValue(data, new object[] { });
            if (Value is not null)
            {
                if (Value.GetType() == typeof(string[]))
                    foreach (var item in (string[])Value)
                        LstParameters.Add(Prop.Name, item.ToString());

                if (Value.GetType() == typeof(string))
                    LstParameters.Add(Prop.Name, Value.ToString());

                if (Value.GetType() == typeof(int))
                    LstParameters.Add(Prop.Name, Value.ToString());

                if (Value.GetType() == typeof(double))
                    LstParameters.Add(Prop.Name, Value.ToString());

                if (Value.GetType() == typeof(float))
                    LstParameters.Add(Prop.Name, Value.ToString());

                if (Value.GetType() == typeof(long))
                    LstParameters.Add(Prop.Name, Value.ToString());

                if (Value.GetType() == typeof(bool))
                    LstParameters.Add(Prop.Name, Value.ToString());
            }
        }
        return LstParameters;


    }
}
