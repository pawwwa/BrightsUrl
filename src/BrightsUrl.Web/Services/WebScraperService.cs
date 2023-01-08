using System.Net;
using System.Text.RegularExpressions;
using BrightsUrl.Web.Models;
using BrightsUrl.Web.Services.Interfaces;

namespace BrightsUrl.Web.Services
{
    public class WebScraperService : IWebScraperService
    {
        private readonly ILogger<WebScraperService> logger;

        private HttpClient HttpClient { get; }

        public WebScraperService(ILogger<WebScraperService> logger)
        {
            HttpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            this.logger = logger;
        }

        public async Task<ScraperResult> ScrapeTitleAndStatusCode(string url)
        {
            var response = await HttpClient.GetAsync(url);

            string responseContent = string.Empty;

            try
            {
                responseContent = await response.Content?.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while receiving title from {url}.");
                throw;
            }

            var title = Regex.Match(responseContent, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                    RegexOptions.IgnoreCase).Groups["Title"].Value;

            var result = new ScraperResult
            {
                Title = title,
                StatusCode = response.StatusCode
            };

            logger.LogInformation($"Successfully received title {result.Title} from {url}, with status code {result.StatusCode}");

            return result;
        }

    }
}
