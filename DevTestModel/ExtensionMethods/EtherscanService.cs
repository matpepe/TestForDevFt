using DevTestModel.Models.ScanModels;
using Newtonsoft.Json;

namespace DevTestModel.ExtensionMethods
{
    public class EtherscanService
    {
        private readonly HttpClient _client;

        public EtherscanService()
        {
            _client = new HttpClient();
        }

        public async Task<EthScanModelOne> GetInternalByBlock()
        {
            // Construct the URL with the provided parameters
            string apiUrl = "https://api.etherscan.io/api";
            string module = "account";
            string action = "txlistinternal";
            string apiKeyToken = "YourApiKeyToken";
            string startBlock = "13481773";
            string endBlock = "13491773";
            string offset = "10";
            string sort = "asc";

            string url = $"{apiUrl}?module={module}&action={action}&startblock={startBlock}&endblock={endBlock}&offset={offset}&sort={sort}&apikey={apiKeyToken}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into EthScanModelOne
                    EthScanModelOne ethScanModel = JsonConvert.DeserializeObject<EthScanModelOne>(result);

                    return ethScanModel;
                }
                catch (HttpRequestException ex)
                {
                    // Handle or log the exception
                    Console.WriteLine($"Error making HTTP request: {ex.Message}");
                    throw;
                }
                catch (JsonException ex)
                {
                    // Handle or log the exception
                    Console.WriteLine($"Error deserializing JSON response: {ex.Message}");
                    throw;
                }
                // Add more catch blocks as needed for different exceptions
            }
        }

    }

}
