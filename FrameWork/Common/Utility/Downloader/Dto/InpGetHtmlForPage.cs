using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace FrameWork.Common.Utility.Downloader.Dto;

public class InpGetHtmlForPage
{
    [Display(Name = nameof(PageUrl))]
    [RequiredString]
    [MaxLengthString(200)]
    public string PageUrl { get; set; }

    [Display(Name = nameof(Data))]
    public object Data { get; set; }

    [Display(Name = nameof(Headers))]
    public Dictionary<string, string> Headers { get; set; }
}
