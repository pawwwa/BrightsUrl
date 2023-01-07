using BrightsUrl.Web.Models;

namespace BrightsUrl.Web.Services.Interfaces
{
    public interface IWebScraperService
    {
        Task<ScraperResult> ScrapeTitleAndStatusCode(string url);
    }
}
