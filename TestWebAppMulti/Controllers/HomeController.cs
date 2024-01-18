using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebAppMulti.Models;

namespace TestWebAppMulti.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuRandom() { return View(); }

        public async Task<ActionResult> ExternalContentDrugsWarning()
        {
            string url = "https://www.drogart.org/kategorija-rezultati/opozorila/";
            string url2 = "https://psychonautwiki.org/wiki/Alprazolam";
            string url3 = "https://www.erowid.org/chemicals/lsd/lsd.shtml";


            // Make a request to the external URL and get the HTML content
            string htmlContent = await _httpClient.GetStringAsync(url);
            string htmlContent2 = await _httpClient.GetStringAsync(url2);
            string htmlContent3 = await _httpClient.GetStringAsync(url3);

            // Pass the HTML content to the view
            ViewBag.ExternalContent = htmlContent;
            ViewBag.ExternalContent2 = htmlContent2;
            ViewBag.ExternalContent3 = htmlContent3;

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}