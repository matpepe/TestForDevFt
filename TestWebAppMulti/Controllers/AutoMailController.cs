using DevTestModel.Models;
using Microsoft.AspNetCore.Mvc;
using SmorcIRL.TempMail;
using SmorcIRL.TempMail.Models;
//https://github.com/SmorcIRL/mail.tm/tree/master use it for testing 

namespace TestWebAppMulti.Controllers
{
    public class AutoMailController : Controller
    {
        private readonly MailService _mailService;
        MailClient client = new MailClient();

        public AutoMailController(MailService mailService)
        {
            _mailService = mailService;
            MailClient client = new MailClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            MailClient client = new MailClient();

            DomainInfo[] domains = await client.GetAvailableDomains();
            ViewBag.Domains = domains;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            if (await _mailService.RegisterAccount(email, password))
            {
                ViewBag.Message = "Account registered successfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to register account.";
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            if (await _mailService.DeleteAccount())
            {
                ViewBag.Message = "Account deleted successfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to delete account.";
            }

            return View("Index");
        }

        public async Task<IActionResult> GetAccountInfo()
        {
            var accountInfo = await _mailService.GetAccountInfo();

            //if (accountInfo != null)
            //{
            //    var accountInfoModel = new AccountInfoModel
            //    {
            //        Email = accountInfo.Email,
            //        Created = accountInfo.Created,
            //        Quota = accountInfo.Quota
            //    };

            //    return View(accountInfoModel);
            //}

            return View(); // Handle the case where accountInfo is null
        }
    }
}

