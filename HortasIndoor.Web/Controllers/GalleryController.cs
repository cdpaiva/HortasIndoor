﻿using HortasIndoor.Core.Models;
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

                using (var res = await httpClient.GetAsync($"https://localhost:5001/api/Profile/{id}"))
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var status = res.StatusCode.ToString();

                    var user = await res.Content.ReadFromJsonAsync<ApplicationUser>();
                    return View(user);
                }
            }
        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> UploadPhoto(PhotoViewModel photoModel)
        {
            _logger.LogInformation("Started the POST request on the cliente");
            _logger.LogInformation($"Image received {photoModel.File.Length}");

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

            _logger.LogInformation("Created a new Photo Object");

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
    }
}
