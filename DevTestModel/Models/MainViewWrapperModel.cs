namespace DevTestModel.Models
{
    public class MainViewWrapperModel
    {
        public GoldPriceModel SingleItem { get; set; }
        public IEnumerable<GoldPriceModel> CollectionItems { get; set; }
    }
}
