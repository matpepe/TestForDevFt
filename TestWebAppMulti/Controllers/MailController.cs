using DevTestModel.Data;
using Microsoft.AspNetCore.Mvc;
using SmorcIRL.TempMail;
using SmorcIRL.TempMail.Models;

namespace TestWebAppMulti.Controllers
{
    public class MailController : Controller
    {
        //_mailClientTm , API implementation
        ApplicationDbContext _dbcontext;
        IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly MailClient _mailClientTm;
        private readonly MailClient _mailClientGw;
        // Inject MailClient through constructor (you can use dependency injection)

        public MailController()
        {
            var apiUriTm = new Uri("https://api.mail.tm/");
            var apiUriGw = new Uri("https://api.mail.gw/");

            _mailClientTm = new MailClient(apiUriTm);
            _mailClientGw = new MailClient(apiUriGw);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string registeredAddress = TempData["RegisteredAddress"] as string;
                ViewBag.RegisteredAddress = registeredAddress;
                // Check API Availability and store the result in ViewBag
                var apiAvailabilityResponse = await CheckApiAvailability();
                ViewBag.CheckApiAvailabilityResult = apiAvailabilityResponse;

                // Get Available Domains and store the result in ViewBag
                var availableDomains = await _mailClientTm.GetAvailableDomains();
                ViewBag.AvailableDomains = availableDomains;

                // Get First Available Domain Name and store the result in ViewBag
                var firstAvailableDomainName = await _mailClientTm.GetFirstAvailableDomainName();
                ViewBag.FirstAvailableDomainName = firstAvailableDomainName;

                var domains = new DomainInfo[] { /* your domain data */ };
                var accountInfo = new AccountInfo(); // Replace with your actual AccountInfo data

                // Create a Tuple with the correct types
                var model = new Tuple<IEnumerable<DomainInfo>, AccountInfo>(domains, accountInfo);

                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception
                //.LogError(ex, "Error in Index action.");

                // Handle exceptions, for example, if there's an issue connecting to the API
                return StatusCode(500, "Error in Index action.");
            }
        }

        public async Task<IActionResult> GetAvailableDomains()
        {
            // Choose the appropriate client based on your needs
            var client = _mailClientTm; // or _mailClientGw

            var domains = await client.GetAvailableDomains();
            return View(domains);
        }

        public async Task<IActionResult> GetFirstAvailableDomainName()
        {
            // Choose the appropriate client based on your needs
            var client = _mailClientTm; // or _mailClientGw

            var domainName = await client.GetFirstAvailableDomainName();
            return View((object)domainName);
        }

        public async Task<IActionResult> GenerateAccount(string password = null)
        {
            // Choose the appropriate client based on your needs
            var client = _mailClientTm; // or _mailClientGw

            await client.GenerateAccount(password);
            return RedirectToAction(nameof(Index)); // Redirect to some action after generating account
        }

        public async Task<IActionResult> GetAccountInfo()
        {
            // Choose the appropriate client based on your needs
            var client = _mailClientTm; // or _mailClientGw
            AccountInfo info = await client.GetAccountInfo();

            //var accountInfo = await client.GetAccountInfo();
            return View(info);
        }

        public async Task<IActionResult> DeleteAccount()
        {
            // Choose the appropriate client based on your needs
            var client = _mailClientTm; // or _mailClientGw

            await client.DeleteAccount();
            return RedirectToAction(nameof(Index)); // Redirect to some action after deletion
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount(string fullAddress, string password)
        {
            try
            {
                await _mailClientTm.Register(fullAddress, password);

                TempData["RegisteredAddress"] = fullAddress;

                return RedirectToAction(nameof(Index));
                //return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }
        public async Task<IActionResult> CheckApiAvailability()
        {
            try
            {
                // Use _mailClientTm or _mailClientGw based on your requirements
                var availableDomains = await _mailClientTm.GetAvailableDomains();

                // Handle the API response as needed
                if (availableDomains != null)
                {
                    return Ok("API is reachable. Available domains: " + string.Join(", ", availableDomains.Select(d => d.Domain)));
                }
                else
                {
                    return StatusCode(500, "API call was not successful.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError(ex, "Error checking API availability.");

                // Handle exceptions, for example, if there's an issue connecting to the API
                return StatusCode(500, "Error checking API availability.");
            }
        }
    }
}
