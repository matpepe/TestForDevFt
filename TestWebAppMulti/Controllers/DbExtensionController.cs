using DevTestModel.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestWebAppMulti.Controllers
{
    public class DbExtensionController : Controller
    {
        ApplicationDbContext _dbcontext;
        IConfiguration configuration;

        //private readonly string ApiKey = "084bc70312e28d503d9f262efacaecde28ea34bfe4bae7ad90b83ce4d33bf761";
        //private readonly string ApiKeyURL = "https://min-api.cryptocompare.com/data/v2/news/?api_key=";
        //private readonly string SqlConnectionString;
        public DbExtensionController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            this.configuration = configuration;
            //SqlConnectionString = "Server=DESKTOP-N89DPOR\\SQLEXPRESS;Database=TestDevALL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        }

        public ActionResult Index() { return View(); }

        [HttpGet]
        public async Task<IActionResult> ViewAction()
        {
            // Use Database.ExecuteSqlRaw
            //string c = _dbcontext.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM dbo.DataHistoryArticle").ToString();

            ViewBag.CountNewsArticle = _dbcontext.NewsArticle.Count().ToString();
            ViewBag.CountDataHistoryArticle = _dbcontext.DataHistoryArticle.Count().ToString();
            ViewBag.NewsApiResponse = _dbcontext.NewsApiResponse.Count().ToString();
            ViewBag.DC_NewsCategoryCR = _dbcontext.DC_NewsCategoryCR.Count().ToString();
            ViewBag.SourceInfoModel = _dbcontext.SourceInfoModel.Count().ToString();

            return View();
        }
    }
}
