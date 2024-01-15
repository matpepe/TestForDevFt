using System.ComponentModel.DataAnnotations;

namespace DevTestModel.Models
{
    public class GoldPriceModel
    {
        [Key]
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public string Metal { get; set; }
        public string Currency { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public double Price { get; set; }
        public double PriceGram22K { get; set; }
        public double CH { get; set; }
        public double CHP { get; set; }
        //[NotMapped]
        public bool? Active { get; set; }
        //[NotMapped]
        public DateTime? DateOfUpload { get; set; } = DateTime.Now;
        //public GoldPriceModel? SingleInstance { get; set; }
        //public List<GoldPriceModel>? LastFourItems { get; set; }
    }

}
