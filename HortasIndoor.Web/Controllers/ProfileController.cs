using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
            logger.LogInformation("Index - profile");
            List<ApplicationUser> users;

            var token = HttpContext.Session.GetString("token");

            logger.LogInformation(token);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.GetAsync("https://localhost:5001/api/Profile/Users"))
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    logger.LogInformation(content);
                    logger.LogInformation(status);
                    //users = await res.Content.ReadFromJsonAsync<List<ApplicationUser>>();

                }
            }
            return View();
        }
    }
}
