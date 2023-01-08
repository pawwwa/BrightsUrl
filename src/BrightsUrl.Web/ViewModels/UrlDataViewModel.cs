using System.Net;

namespace BrightsUrl.Web.ViewModels
{
    public class UrlDataViewModel
    {
        public string Url { get; set; } = null!;
        public string? Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Succeeded { get; set; }

        public static UrlDataViewModel FromSuccess(string url, string? title, HttpStatusCode statusCode)
            => new UrlDataViewModel { Url = url, Title = title, StatusCode= statusCode, Succeeded = true };

        public static UrlDataViewModel FromError(string url) 
            => new UrlDataViewModel { Url = url, Succeeded = false };
    }
}
