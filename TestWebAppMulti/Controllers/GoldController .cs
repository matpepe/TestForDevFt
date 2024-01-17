//using DevTestModel.Data;
//using DevTestModel.Models;
//using Microsoft.AspNetCore.Mvc;
////using System.Web.Mvc;
//using Newtonsoft.Json;

//namespace TestWebAppMulti.Controllers
//{
//    public class GoldController : Controller
//    {
//        ApplicationDbContext _dbcontext;
//        IConfiguration configuration;
//        private readonly string SqlConnectionString;
//        private readonly string ApiKey = "goldapi-31qbfcirlr8e8rux-io"; // Replace with your actual API key
//        private readonly string stringAPI = "x-access-token";
//        private readonly ILogger<GoldController> _logger;
//        public GoldController(ApplicationDbContext dbContext, IConfiguration configuration, ILogger<GoldController> logger)
//        {
//            _dbcontext = dbContext;
//            this.configuration = configuration;
//            SqlConnectionString = "Server=DESKTOP-N89DPOR\\SQLEXPRESS;Database=TestDevALL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
//            _logger = logger;
//        }



//        [HttpPost]
//        public async Task<ActionResult> Index()
//        {

//            string symbol = "XAU";
//            string curr = "USD";
//            string date = "";

//            using (HttpClient client = new HttpClient())
//            {
//                client.DefaultRequestHeaders.Add(stringAPI, ApiKey);

//                string url = $"https://www.goldapi.io/api/{symbol}/{curr}{date}";
//                var collectionItems = _dbcontext.GoldPriceModel.ToList();

//                try
//                {
//                    HttpResponseMessage response = await client.GetAsync(url);
//                    response.EnsureSuccessStatusCode();

//                    string result = await response.Content.ReadAsStringAsync();

//                    if (!string.IsNullOrEmpty(result))
//                    {
//                        GoldPriceModel goldPrice = JsonConvert.DeserializeObject<GoldPriceModel>(result);
//                        return View(goldPrice);
//                    }
//                    else
//                    {
//                        // Handle the case where the API response is empty
//                        ViewBag.ErrorMessage = "API response is empty.";
//                        return View("Error");
//                    }
//                }
//                catch (HttpRequestException ex)
//                {
//                    // Log the error for debugging purposes
//                    _logger.LogError($"Error making HTTP request: {ex.Message}");

//                    ViewBag.ErrorMessage = "Error making HTTP request to the API.";
//                    return View("Error");
//                }
//                catch (JsonException ex)
//                {
//                    // Log the error for debugging purposes
//                    _logger.LogError($"Error deserializing JSON response: {ex.Message}");

//                    ViewBag.ErrorMessage = "Error deserializing JSON response from the API.";
//                    return View("Error");
//                }
//                catch (Exception ex)
//                {
//                    // Log the error for debugging purposes
//                    _logger.LogError($"Unexpected error: {ex.Message}");

//                    ViewBag.ErrorMessage = "An unexpected error occurred.";
//                    return View("Error");
//                }
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult> SaveGoldPrice()
//        {
//            string symbol = "XAU";
//            string curr = "USD";
//            string date = "";

//            //List<GoldPriceModel> lastFourItems = _dbcontext.GoldPriceModel.ToList();

//            using (HttpClient client = new HttpClient())
//            {
//                client.DefaultRequestHeaders.Add(stringAPI, ApiKey);

//                string url = $"https://www.goldapi.io/api/{symbol}/{curr}{date}";

//                try
//                {
//                    HttpResponseMessage response = await client.GetAsync(url);
//                    response.EnsureSuccessStatusCode();

//                    string result = await response.Content.ReadAsStringAsync();
//                    GoldPriceModel goldPrice = JsonConvert.DeserializeObject<GoldPriceModel>(result);

//                    if (goldPrice != null)
//                    {
//                        // Set any additional properties before saving if needed
//                        goldPrice.Active = true;

//                        // Add the object to the DbSet and save changes
//                        _dbcontext.GoldPriceModel.Add(goldPrice);
//                        await _dbcontext.SaveChangesAsync();
//                    }
//                    else
//                    {
//                        // Handle the case where the deserialization failed or returned null
//                        ViewBag.ErrorMessage = "Error: Unable to deserialize API response or result is null.";
//                        return View("Error");
//                    }

//                    return RedirectToAction("ViewGoldFull", "Gold");
//                }
//                catch (Exception ex)
//                {
//                    ViewBag.ErrorMessage = "Error: " + ex.Message;
//                    return View("Error");
//                }
//            }

//            // Additional logic if needed
//        }

//        [HttpGet]
//        public ActionResult SmallGoldTable()
//        {
//            //List<GoldPriceModel> lastFourItems = _dbcontext.GoldPriceModel.GetLastFourItems();
//            List<GoldPriceModel> allGoldItems = _dbcontext.GoldPriceModel.ToList();

//            return View(allGoldItems);
//        }

//        private bool ArticleGoldExists(int articleId)
//        {
//            return _dbcontext.GoldPriceModel.Any(s => s.Id == articleId);
//        }

//        public ActionResult ViewGoldFull()
//        {
//            // Retrieve or create your single item
//            var singleItem = _dbcontext.GoldPriceModel.FirstOrDefault(); // Adjust as needed

//            // Fetch the last four items
//            var collectionItems = _dbcontext.GoldPriceModel
//                .OrderByDescending(x => x.DateOfUpload)
//                .Take(12)                   //12
//                .ToList();

//            // Create a wrapper model to pass both sets of data to the view
//            var viewModel = new MainViewWrapperModel
//            {
//                SingleItem = singleItem,
//                CollectionItems = collectionItems
//            };

//            return View(viewModel);
//        }
//        //public IActionResult ViewAction() { return View(); }
//        //public IActionResult Index2() { return View(); }
//    }
//}


////[Route("/Gold/SaveGoldPrice")] // Adjust the route based on your preference