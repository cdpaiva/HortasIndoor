using Microsoft.AspNetCore.Mvc;
using HortasIndoor.Core.Models;
using System.Text;
using Newtonsoft.Json;

namespace HortasIndoor.Web.Controllers
{
    public class AccountController : Controller
    {
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

                using(var response = await httpClient.PostAsync("http://localhost:5085/api/Auth/Login", content)){
                    if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return View();
                    }
                    
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Token = apiResponse;
                    return View();

                }
            }
        }
    }
}
