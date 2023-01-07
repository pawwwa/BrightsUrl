using System.Net;
using System.Text.RegularExpressions;
using BrightsUrl.Web.Models;
using BrightsUrl.Web.Services.Interfaces;

namespace BrightsUrl.Web.Services
{
    public class WebScraperService : IWebScraperService
    {
        public async Task<ScraperResult> ScrapeTitleAndStatusCode(string url)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

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
