using DevTestModel.Models;

namespace DevTestModel.ExtensionMethods
{
    public static class GoldPriceModelExtensions
    {
        public static List<GoldPriceModel> GetLastFourItems(this IQueryable<GoldPriceModel> queryable)
        {
            return queryable
                .OrderByDescending(x => x.DateOfUpload)
                .Take(4)
                .ToList();
        }
    }
}
