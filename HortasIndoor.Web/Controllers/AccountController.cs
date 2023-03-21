using Microsoft.AspNetCore.Mvc;
using HortasIndoor.Core.Models;
using System.Text;
using Newtonsoft.Json;

namespace HortasIndoor.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            using(var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

                using(var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Login", content)){
                    if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return View();
                    }
                    
                    var responseContent = await response.Content.ReadFromJsonAsync<TokenResponse>();

                    HttpContext.Session.SetString("Token", responseContent.Token);
                    HttpContext.Session.SetString("Id", responseContent.Id);

                    return RedirectToAction("Index", "Profile");
                }
            }
        }

        public IActionResult Register() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDetails details)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(details), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Register", stringContent);

                return Redirect("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Login");
        }
    }
}
