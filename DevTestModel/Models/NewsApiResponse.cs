using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTestModel.Models
{
    public class NewsApiResponse
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApiResponseId { get; set; }

        public int Type { get; set; }
        public string Message { get; set; }

        [NotMapped]
        public object[] Promoted { get; set; }

        public ICollection<NewsArticleModel> Data { get; set; }

        [ForeignKey(nameof(Id))] // Specify the foreign key property here
        public string Id { get; set; }

        public NewsArticleModel NewsArticle { get; set; } // Navigation property
    }


}
