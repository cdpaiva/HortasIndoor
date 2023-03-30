using HortasIndoor.Core.Interfaces;
using HortasIndoor.Core.Models;
using HortasIndoor.DataAccess.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HortasIndoor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IProfileRepository _repository;
        private readonly IPhotoRepository _photoRepository;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IProfileRepository repository,IPhotoRepository photoRepository, ILogger<GalleryController> logger)
        {
            _repository = repository;
            _photoRepository = photoRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> SavePhoto(string id, Photo photo)
        {
            _logger.LogInformation("New request to get all friends for id " + id);
            if (photo == null)
            {
                return Problem("Não foi recebido um objeto para ser salvo.");
            }
            var user = await _repository.AddPhoto(id, photo);

            if (user.Photos.Any())
            {
                return Ok("Foto salva com sucesso");
            }

            return Problem("Não foi possível salvar a foto.");

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPhotos(string id)
        {
            _logger.LogInformation("New request to get all photos for id " + id);

            var user = await _repository.GetWithGallery(id);

            if (user == null)
            {
                return Problem("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await _photoRepository.DeletePhoto(id);
            return Ok();
        }
    }
}
