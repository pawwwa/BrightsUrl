using System.Net;
using System.Text.RegularExpressions;
using BrightsUrl.Web.Models;
using BrightsUrl.Web.Services.Interfaces;

namespace BrightsUrl.Web.Services
{
    public class WebScraperService : IWebScraperService
    {
        private HttpClient HttpClient { get;  }

        public WebScraperService()
        {
            HttpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<ScraperResult> ScrapeTitleAndStatusCode(string url)
        {

            var response = await HttpClient.GetAsync(url);

            var content = await response.Content?.ReadAsStringAsync();

            var title = Regex.Match(content, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                    RegexOptions.IgnoreCase).Groups["Title"].Value;

            return new ScraperResult
            {
                Title = title,
                StatusCode = response.StatusCode
            };
        }

    }
}
