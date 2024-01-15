using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTestModel.Models
{
    public class DataHistoryArticle
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        public string Id { get; set; }
        public string Guid { get; set; }
        public long PublishedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
        public string Lang { get; set; }
        public string Upvotes { get; set; }
        public string Downvotes { get; set; }
        public string Categories { get; set; }
        public int? SourceInfoId { get; set; }
        public int? NewsApiResponseApiResponseId { get; set; }
        public bool? Active { get; set; }
        public DateTime? DateAndTimeInserted { get; set; }
    }

}
