using DevTestModel.Data;
using DevTestModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
namespace TestWebAppMulti.Controllers
{
    public class CryptoArticleController : Controller
    {
        ApplicationDbContext _dbcontext;
        IConfiguration configuration;

        private readonly string ApiKey = "084bc70312e28d503d9f262efacaecde28ea34bfe4bae7ad90b83ce4d33bf761";
        private readonly string ApiKeyURL = "https://min-api.cryptocompare.com/data/v2/news/?api_key=";
        private readonly string SqlConnectionString;
        public CryptoArticleController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            this.configuration = configuration;
            SqlConnectionString = "Server=DESKTOP-N89DPOR\\SQLEXPRESS;Database=TestDevALL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{ApiKeyURL}+{ApiKey}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(result);

                    return View(newsResponse.Data.ToList());
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public ActionResult SaveArticle()
        {
            var client = new RestClient(ApiKeyURL + ApiKey);
            string xapikey = "x-api-key";
            var request = new RestRequest();

            request.AddHeader(xapikey, ApiKey);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                try
                {
                    // Deserialize the response to your ApiResponse
                    var apiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(response.Content);

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        // Filter out articles with existing IDs
                        var newArticles = apiResponse.Data.Where(article => !ArticleExists(article.Id)).ToList();

                        DataTable dataTableNewsArticle = ConvertToDataTableNewArticle(newArticles);

                        // Save data to SQL Server
                        SaveDataToSqlServerNewsArticle(dataTableNewsArticle);

                        return RedirectToAction("ViewAction", "DbExtension"); // Redirect to the Index action after saving
                        //return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error: Invalid API response format.";
                        return View("Error");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    ViewBag.ErrorMessage = "Error deserializing API response: " + ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Error calling API: " + response.ErrorMessage;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> JustSaveNewArticle()
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                using (SqlCommand command = new SqlCommand("PR_InsertHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        // You can handle the result if needed
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return View("Error");
                    }
                }
                connection.Close(); //closing 
            }
            return RedirectToAction("ViewAction", "DbExtension"); // Redirect to the Index action after saving
            //return RedirectToAction("Index");
        }

        private void SaveDataToSqlServerNewsArticle(DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "NewsArticle"; // Your SQL Server table name

                    // Mapping DataTable columns to SQL Server table columns
                    foreach (DataColumn dataTableColumn in dataTable.Columns)
                    {
                        // Find the matching SQL Server column by considering case differences
                        foreach (DataColumn sqlServerColumn in GetSqlServerTableColumns())
                        {
                            if (string.Equals(dataTableColumn.ColumnName, sqlServerColumn.ColumnName, StringComparison.OrdinalIgnoreCase))
                            {
                                bulkCopy.ColumnMappings.Add(dataTableColumn.ColumnName, sqlServerColumn.ColumnName);
                                break;
                            }
                        }
                    }

                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }

        // Helper method to check if an article with the given ID already exists
        private bool ArticleExists(string articleId)
        {
            return _dbcontext.NewsArticle.Any(article => article.Id == articleId);
        }

        // ##START
        [HttpPost]
        public ActionResult InsertSourceTableData()
        {
            var client = new RestClient(ApiKeyURL + ApiKey);
            string xapikey = "x-api-key";
            var request = new RestRequest();
            request.AddHeader(xapikey, ApiKey);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                try
                {
                    // Deserialize the response to your ApiResponse
                    var apiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(response.Content);

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        // Filter out articles with existing IDs
                        var newArticles = apiResponse.Data
                            .Where(article => !ArticleExists(article.Id))
                            .ToList();

                        // Save data to SQL Server
                        SaveDataToSqlServerSourceInfo(newArticles);

                        return RedirectToAction("ViewAction", "DbExtension"); // Redirect to the Index action after saving
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error: Invalid API response format.";
                        return View("Error");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    ViewBag.ErrorMessage = "Error deserializing API response: " + ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Error calling API: " + response.ErrorMessage;
                return View("Error");
            }
        }

        private void SaveDataToSqlServerSourceInfo(List<NewsArticleModel> newArticles)
        {
            try
            {
                if (newArticles != null && newArticles.Any())
                {
                    using (var dbContext = new ApplicationDbContext(/* Pass DbContextOptions if needed */))
                    {
                        foreach (var article in newArticles)
                        {
                            var sourceInfo = new SourceInfoModel
                            {
                                Id = article.Id, // Assuming Id property in SourceInfoModel corresponds to article Id
                                Name = article.SourceInfo?.Name,
                                Img = article.SourceInfo?.Img,
                                Source = article.SourceInfo?.Source
                            };

                            // Check if the SourceInfoModel with the given Id already exists in the database
                            var existingSourceInfo = dbContext.SourceInfoModel.FirstOrDefault(dbSourceInfo => dbSourceInfo.Id == sourceInfo.Id);

                            if (existingSourceInfo == null)
                            {
                                // SourceInfoModel with the given Id doesn't exist, so add it
                                dbContext.SourceInfoModel.Add(sourceInfo);
                            }
                            else
                            {
                                // SourceInfoModel with the given Id already exists, update it if needed
                                existingSourceInfo.Name = sourceInfo.Name;
                                existingSourceInfo.Img = sourceInfo.Img;
                                existingSourceInfo.Source = sourceInfo.Source;
                            }
                        }

                        // Save changes to the database
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the exception)
                Console.WriteLine("Error saving data to SQL Server using Entity Framework: " + ex.Message);
            }
        }


        /// <summary>
        /// ////////////////
        /// </summary>
        /// <returns></returns>

        // Helper method to get columns from the SQL Server table
        private IEnumerable<DataColumn> GetSqlServerTableColumns()
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'NewsArticle'", connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable schemaTable = reader.GetSchemaTable();
                List<DataColumn> columns = schemaTable.Rows.OfType<DataRow>().Select(row => schemaTable.Columns.Cast<DataColumn>().Select(column => row[column]).FirstOrDefault()).Select(data => new DataColumn(data.ToString())).ToList();

                reader.Close();
                return columns;
            }
        }

        public static DataTable ConvertToDataTableNewArticle(ICollection<NewsArticleModel> data)
        {
            DataTable dataTable = new DataTable();

            // Assume the first item in the collection is not null
            if (data != null && data.Count > 0)
            {
                // Get the properties of the NewsArticleModel class
                var properties = data.First().GetType().GetProperties();

                // Create the columns in the DataTable based on the properties
                foreach (var property in properties)
                {
                    Type columnType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    dataTable.Columns.Add(property.Name, columnType);
                }

                // Populate the DataTable with data from the collection
                foreach (var item in data)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                    {
                        row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        [HttpGet]
        public async Task<IActionResult> ByCategory()
        {
            List<DataHistoryArticle> data = await _dbcontext.DataHistoryArticle.ToListAsync();

            return View(data);
        }

        //ByCategory

        //public IActionResult ViewAction() { return View(); }
        //public IActionResult Index2() { return View(); }


        //private DbContextOptions<ApplicationDbContext> GetDbContextOptions() { }


    }
}


/*
         //[HttpGet]
        public async Task<IActionResult> ByCategory()
        {
            // Get the list of categories from the database
            List<DC_NewsCategoryCR> categories = await _dbcontext.DC_NewsCategoryCR.ToListAsync();

            // Pass the list of categories to the view using ViewBag
            ViewBag.Categories = new SelectList(categories, "CategoryNewsName", "CategoryNewsName");

            // You can adjust the query to filter data based on the selected category
            List<DataHistoryArticle> data = await _dbcontext.DataHistoryArticle.ToListAsync();

            return View(data);
        }
 
 */