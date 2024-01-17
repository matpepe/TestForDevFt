using Microsoft.AspNetCore.Mvc;
using SmorcIRL.TempMail;

namespace TestWebAppMulti.Controllers
{
    public class MailController : Controller
    {
        private readonly MailClient _mailClient;

        public MailController()
        {
            // Use the default API URL (mail.tm)
            _mailClient = new MailClient();
        }

        public MailController(Uri baseUri)
        {
            // Use a custom API URL
            _mailClient = new MailClient(baseUri);
        }

        public IActionResult Index()
        {
            try
            {
                var domains = _mailClient.GetAvailableDomains();
                ViewBag.AvailableDomains = domains; // Pass the list to ViewBag
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get available domains: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string action, string email, string password, string optionalPassword)
        {
            try
            {
                switch (action.ToLower())
                {
                    case "register":
                        await _mailClient.Register(email, password);
                        ViewData["Message"] = "Registration successful";
                        break;
                    case "getavailabledomains":
                        var domains = await _mailClient.GetAvailableDomains();
                        return View("GetAvailableDomains", domains);
                    case "getfirstavailabledomainname":
                        var domain = await _mailClient.GetFirstAvailableDomainName();
                        ViewData["FirstAvailableDomain"] = domain;
                        break;
                    case "generateaccount":
                        await _mailClient.GenerateAccount(optionalPassword);
                        ViewData["Message"] = "Account generated successfully";
                        break;
                    case "getaccountinfo":
                        var accountInfo = await _mailClient.GetAccountInfo();
                        return View("GetAccountInfo", accountInfo);
                    case "deleteaccount":
                        await _mailClient.DeleteAccount();
                        ViewData["Message"] = "Account deleted successfully";
                        break;
                    default:
                        ViewData["ErrorMessage"] = "Invalid action";
                        break;
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Operation failed: {ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> Register(string email, string password)
        {
            try
            {
                await _mailClient.Register(email, password);
                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetAvailableDomains()
        {
            try
            {
                var domains = await _mailClient.GetAvailableDomains();
                return View(domains);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get available domains: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetFirstAvailableDomainName()
        {
            try
            {
                var domain = await _mailClient.GetFirstAvailableDomainName();
                return View(domain);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get first available domain name: {ex.Message}");
            }
        }

        public async Task<IActionResult> GenerateAccount(string optionalPassword)
        {
            try
            {
                await _mailClient.GenerateAccount(optionalPassword);
                return Ok("Account generated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to generate account: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetAccountInfo()
        {
            try
            {
                var accountInfo = await _mailClient.GetAccountInfo();
                return View(accountInfo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get account info: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                await _mailClient.DeleteAccount();
                return Ok("Account deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete account: {ex.Message}");
            }
        }
    }
}
