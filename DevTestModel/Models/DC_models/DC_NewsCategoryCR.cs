using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTestModel.Models.DC_models
{
    public class DC_NewsCategoryCR
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        //public NewsArticleModel Category { get; set; }
        //[Required]
        //[ForeignKey(nameof(Category))]
        public string? CategoryNewsName { get; set; }
        public string? ShortCategoryName { get; set; }
        public bool? Active { get; set; } = true;

        [DisplayFormat()]
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
