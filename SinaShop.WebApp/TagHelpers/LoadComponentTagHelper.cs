using FrameWork.Common.Utility.Downloader;
using FrameWork.Consts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameWork.Common.Utility.Downloader.Dto;

namespace SinaShop.WebApp.TagHelpers;

[HtmlTargetElement("LoadComponet")]
public class LoadComponentTagHelper : TagHelper
{
    private readonly IDownloader _Downloader;

    public LoadComponentTagHelper(IDownloader downloader)
    {
        _Downloader = downloader;
    }
    #region Inputs
    public string Id { get; set; }
    public string Class { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
    public HttpContext HttpContext { get; set; }
    #endregion Inputs

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(nameof(Url), "Url can not be null");
        ArgumentNullException.ThrowIfNull(nameof(context), "context can not be null");

        Url = SiteSettingConst.SiteUrl + Url;
        string HtmlData = await _Downloader.GetHtmlAsync(new InpGetHtmlForPage
        {
            Data = Data,
            PageUrl = Url,
            Headers = HttpContext.Request.Headers.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToDictionary(k => k.Key, v => v.Value)
        });
        ArgumentNullException.ThrowIfNull(nameof(HtmlData), "Source data is null");
        output.TagName = "div";
        output.Content.SetContent(HtmlData);

        if (string.IsNullOrEmpty(Id))
            output.Attributes.SetAttribute("id", Id);

        if (string.IsNullOrEmpty(Class))
            output.Attributes.SetAttribute("class", Class);


    }
}
