using FrameWork.Common.Utility.Downloader.Dto;

namespace FrameWork.Common.Utility.Downloader
{
    public interface IDownloader
    {
        Task<string> GetHtmlAsync(InpGetHtmlForPage input);
    }
}