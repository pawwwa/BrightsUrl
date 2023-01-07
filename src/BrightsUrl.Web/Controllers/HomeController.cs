using BrightsUrl.Web.Models;
using BrightsUrl.Web.Services.Interfaces;
using BrightsUrl.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrightsUrl.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IWebScraperService webScraperService;

        public HomeController(ILogger<HomeController> logger,
                              IWebScraperService webScraperService)
        {
            this.logger = logger;
            this.webScraperService = webScraperService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessUrls(HomeViewModel viewModel)
        {
            var urls = viewModel.UrlsToProcess
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var url in urls)
            {
                if(Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    var result = await webScraperService.ScrapeTitleAndStatusCode(url);

                    viewModel.ProcessedUrls.Add(new UrlDataViewModel
                    {
                        Url = url,
                        Title = result.Title,
                        StatusCode = result.StatusCode
                    });
                }
            }

            return View("Index", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}