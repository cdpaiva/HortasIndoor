using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace HortasIndoor.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var id= HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Profile/{id}"))
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    return View(user);
                }
            }
        }

        public IActionResult Edit()
        {
            var id = HttpContext.Session.GetString("Id");
            var user = new ApplicationUser { Id= id };
            return View(user);
        }

        public async Task<IActionResult> Update(ApplicationUser user)
        {
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:5001/api/Profile", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return View();
                    }
                    logger.LogInformation(response.StatusCode.ToString());
                    logger.LogInformation(await response.Content.ReadAsStringAsync());
                }
            }

            return RedirectToAction("Index");
        }
    }
}
