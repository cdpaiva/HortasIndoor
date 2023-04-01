using HortasIndoor.Core.Models;
using HortasIndoor.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HortasIndoor.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(ILogger<GalleryController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("Id");
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
                        return View(viewModel);
                    }
                    else
                    {
                        return Problem("Não foi possível recuperar o usuário");
                    }
                }
            }
        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> UploadPhoto(PhotoViewModel photoModel)
        {
            var id = HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            var memoryStream = new MemoryStream();
            await photoModel.File.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            memoryStream.Dispose();

            var photo = new Photo
            {
                Bytes = bytes,
                Description = photoModel.Description,
                FileExtension = photoModel.File.ContentType,
                Size = bytes.Length
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.PostAsJsonAsync<Photo>($"https://localhost:5001/api/Gallery/{id}", photo))
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    //var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    return RedirectToAction("Index", "Gallery");
                }
            }
        }

        public async Task<IActionResult> DeletePhoto(int id)
        {
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.DeleteAsync($"https://localhost:5001/api/Gallery/{id}"))
                {
                    return RedirectToAction("Index", "Gallery");
                }
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.PhotoId = id;
            var userId = HttpContext.Session.GetString("Id");
            var token = HttpContext.Session.GetString("Token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Gallery/{userId}"))
                {
                    var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    if (user != null)
                    {
                        user.Photos = user.Photos.Where(p => p.Id ==id).ToList();
                        var viewModel = new GalleryViewModel(user);
                        return View(viewModel);
                    }
                    else
                    {
                        return Problem("Não foi possível recuperar o usuário");
                    }
                }
            }
        }
    }
}
