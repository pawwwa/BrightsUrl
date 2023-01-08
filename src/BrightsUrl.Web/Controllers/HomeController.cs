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
            if (string.IsNullOrEmpty(viewModel.UrlsToProcess))
                return View("Index", viewModel);

            var urls = viewModel.UrlsToProcess
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToList();

            await Parallel.ForEachAsync(urls, async (url, token) =>
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    viewModel.ProcessedUrls.Add(UrlDataViewModel.FromError(url, $"'{url}' is bad url."));
                    return;
                }

                try
                {
                    var result = await webScraperService.ScrapeTitleAndStatusCode(url);

                    viewModel.ProcessedUrls.Add(UrlDataViewModel.FromSuccess(url, result.Title, result.StatusCode));
                }
                catch (Exception ex)
                {
                    viewModel.ProcessedUrls.Add(UrlDataViewModel.FromError(url, ex.Message));
                }
            });

            viewModel.ProcessedUrls = viewModel.ProcessedUrls.OrderByDescending(x => x.StatusCode).ToList();

            return View("Index", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}