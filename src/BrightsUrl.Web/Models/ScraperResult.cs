using System.Net;

namespace BrightsUrl.Web.Models
{
    public class ScraperResult
    {
        public string? Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
