using System.Net;

namespace BrightsUrl.Web.ViewModels
{
    public class UrlDataViewModel
    {
        public string Url { get; set; } = null!;
        public string? Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
