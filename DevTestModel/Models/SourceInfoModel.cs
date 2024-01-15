using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTestModel.Models
{
    public class SourceInfoModel
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SourceId { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public string? Source { get; set; }

        public string Id { get; set; }



    }
}
