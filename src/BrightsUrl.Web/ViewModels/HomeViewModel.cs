namespace BrightsUrl.Web.ViewModels
{
    public class HomeViewModel
    {
        public string? UrlsToProcess { get; set; }
        public List<UrlDataViewModel> ProcessedUrls { get; set; } = new();
    }
}
