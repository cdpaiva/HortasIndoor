using HortasIndoor.Core.Models;
using HortasIndoor.Core.ViewModels;
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

            ApplicationUser user;
            List<Post> posts;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Profile/{id}"))
                {
                    user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    //return View(user);
                }

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Post"))
                {
                    posts = await res.Content.ReadFromJsonAsync<List<Post>>();
                    posts = posts.OrderByDescending(p => p.Created_at).ToList();
                    //return View(user);
                }
            }
            return View(new InitialPageViewModel(user, posts));
        }

        public async Task<IActionResult> User([FromQuery]string id)
        {
            //var id = HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Gallery/{id}"))
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    if (user != null)
                    {
                        var viewModel = new GalleryViewModel(user);
                        return View("User", viewModel);
                    }
                    else
                    {
                        return Problem("Não foi possível recuperar o usuário");
                    }
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
