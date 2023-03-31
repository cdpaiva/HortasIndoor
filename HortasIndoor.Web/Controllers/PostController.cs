using HortasIndoor.Core.Models;
using HortasIndoor.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HortasIndoor.Web.Controllers
{
    public class PostController : Controller
    {
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(NewPostViewModel postViewModel)
        {
            var userId = HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            var post = new Post
            {
                Text= postViewModel.Text,
                Created_at = DateTime.Now.ToString(),
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.PostAsJsonAsync<Post>($"https://localhost:5001/api/Post/{userId}", post))
                {
                    //var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    //var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    return RedirectToAction("Index", "Profile");
                }
            }
        }

        public async Task<IActionResult> Like(int postId)
        {
            var userId = HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.PostAsJsonAsync($"https://localhost:5001/api/Post/{userId}/{postId}", new { }))
                {
                    //var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    //var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    return RedirectToAction("Index", "Profile");
                }
            }
        }
    }
}
