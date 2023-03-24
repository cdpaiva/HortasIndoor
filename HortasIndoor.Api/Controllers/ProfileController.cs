using HortasIndoor.Core.Interfaces;
using HortasIndoor.Core.Models;
using HortasIndoor.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HortasIndoor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _repository;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileRepository repository, ILogger<ProfileController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> Users()
        {
            _logger.LogInformation("Get all users");
            return Ok(_repository.GetAll());
        }

        [Authorize]
        [HttpGet]
        [Route("/api/Profile/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            _logger.LogInformation("Get user by Id");
            return Ok(_repository.GetById(id));
        }

        [Authorize]
        [HttpPut]
        [Route("/api/Profile")]
        public async Task<IActionResult> Update(ApplicationUser user)
        {
            await _repository.Update(user);
            return Ok();
        }
    }
}
