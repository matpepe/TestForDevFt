using DevTestModel.Data;
using DevTestModel.Models.ScanModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//Get a list of 'Internal' Transactions by Address
namespace TestWebAppMulti.Controllers
{
    public class EthScanController : Controller
    {
        ApplicationDbContext _dbcontext;
        IConfiguration configuration;

        private readonly string SqlConnectionString;
        //private readonly EtherscanService _etherscanService;

        private readonly string ApiPage = "1";
        private readonly string ApiOffset = "10"; // numbers per page 
        private readonly string ApiTestAddress = "0xb23360CCDd9Ed1b15D45E5d3824Bb409C8D7c460";
        private readonly string ApiKeyToken = "1KP5JAP3UH1Q8VQD8G5EW43B61TV8UKSI5";
        private readonly string ApiKeyURL = "https://api.etherscan.io/api";

        private readonly EtherscanService _etherscanService;
        public EthScanController(ApplicationDbContext dbContext, IConfiguration configuration, EtherscanService etherscanService)
        {
            _dbcontext = dbContext;
            this.configuration = configuration;
            SqlConnectionString = "Server=DESKTOP-N89DPOR\\SQLEXPRESS;Database=TestDevALL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
            _etherscanService = etherscanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = ApiKeyURL;
                string module = "account";
                string action = "txlistinternal";
                string apiKeyToken = ApiKeyToken;
                string startBlock = "13481773";
                string endBlock = "13491773";
                string offset = "10";
                string sort = "asc";

                string url = $"{apiUrl}?module={module}&action={action}&startblock={startBlock}&endblock={endBlock}&offset={offset}&sort={sort}&apikey={apiKeyToken}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    var ethScanModel = JsonConvert.DeserializeObject<EthScanModelOne>(result);

                    return View(ethScanModel); // Pass the entire EthScanModelOne instance to the view
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string startBlock, string endBlock)
        {
            // Validate the input and perform necessary actions
            if (string.IsNullOrEmpty(startBlock) || string.IsNullOrEmpty(endBlock))
            {
                ModelState.AddModelError("startBlock", "Start Block is required.");
                ModelState.AddModelError("endBlock", "End Block is required.");
                return View("Index"); // Return the Index view to display the form again
            }

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = ApiKeyURL;
                string module = "account";
                string action = "txlistinternal";
                string apiKeyToken = ApiKeyToken;
                string offset = "10";
                string sort = "asc";

                string url = $"{apiUrl}?module={module}&action={action}&startblock={startBlock}&endblock={endBlock}&offset={offset}&sort={sort}&apikey={apiKeyToken}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    var ethScanModel = JsonConvert.DeserializeObject<EthScanModelOne>(result);

                    return View("Index", ethScanModel); // Return the Index view with the new data
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                    return View("Error");
                }
            }
        }
    }
}
