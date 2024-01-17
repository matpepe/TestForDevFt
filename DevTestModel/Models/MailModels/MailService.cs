using SmorcIRL.TempMail;
using SmorcIRL.TempMail.Models;

namespace DevTestModel.Models
{

    public class MailService
    {

        public string Email { get; set; }
        public DateTime Created { get; set; }
        public int Quota { get; set; }
        public string Domain { get; set; }

        private readonly MailClient _mailClient;

        public MailService(string apiUrl)
        {
            _mailClient = new MailClient(new Uri(apiUrl));
        }

        public async Task<bool> RegisterAccount(string email, string password)
        {
            try
            {
                await _mailClient.Register(email, password);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccount()
        {
            try
            {
                await _mailClient.DeleteAccount();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AccountInfo> GetAccountInfo()
        {
            try
            {
                return await _mailClient.GetAccountInfo();
            }
            catch
            {
                return null;
            }
        }
    }
}
