namespace DevTestModel.Models.MailModels
{
    public class AccountInfoModel
    {
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }

        public int Quota { get; set; }
    }
}
