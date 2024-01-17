using Newtonsoft.Json;

namespace DevTestModel.Models.ScanModels
{
    public class EtherscanService
    {
        public async Task<EthScanModelOne> GetInternalTransactionsAsync(string apiUrl, string startBlock, string endBlock, string offset, string sort, string apiKeyToken)
        {
            string url = $"{apiUrl}?module=account&action=txlistinternal&startblock={startBlock}&endblock={endBlock}&offset={offset}&sort={sort}&apikey={apiKeyToken}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EthScanModelOne>(result);
                }
                else
                {
                    // Handle the error
                    return null;
                }
            }
        }
    }

}
