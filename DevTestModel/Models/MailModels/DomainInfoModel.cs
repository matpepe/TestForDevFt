namespace DevTestModel.Models.MailModels
{
    public class DomainInfoModel
    {
        public string Id { get; set; }

        public string Domain { get; set; }

        public string IsActive { get; set; }

        public string IsPrivate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
