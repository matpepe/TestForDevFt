namespace DevTestModel.Models.ScanModels
{
    public class EthScanModelOne
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<InternalTransactionEth> Result { get; set; }
    }
}
