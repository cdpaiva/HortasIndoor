using HortasIndoor.Core.Interfaces;
using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HortasIndoor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _repository.GetAll();
            return Ok(posts);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create(string id, Post post)
        {
            if (post == null)
            {
                return Problem("Não foi recebido um objeto para ser salvo.");
            }
            var newPost = await _repository.Create(id, post);
            if(newPost.User == null) {
                return BadRequest("Usuário não encontrado");
            }
            return Ok();
        }

        [HttpPost]
        [Route("{userId}/{postId}")]
        public async Task<IActionResult> Like(string userId, int postId)
        {
            await _repository.AddLike(postId, userId);
            return Ok();
        }
    }
}
