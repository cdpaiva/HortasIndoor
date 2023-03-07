using HortasIndoor.Api.Configuration;
using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HortasIndoor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerSettings jwtBearerTokenSettings;
        private readonly UserManager<User> userManager;

        public AuthController(IOptions<JwtBearerSettings> jwtTokenOptions, UserManager<User> userManager)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDetails userDetails)
        {

            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new User() { UserName = userDetails.UserName, Email = userDetails.Email };

            var result = await userManager.CreateAsync(identityUser, userDetails.Password);


            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Registration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            if(!ModelState.IsValid || credentials == null )
            {
                return new BadRequestObjectResult(new { Message = "Login Failed" });
            }

            User user = await ValidateUser(credentials);

            if(user == null)
            {
                return new BadRequestObjectResult(new { Message = "Login Failed" });
            }

            var token = GenerateToken(user);
            return Ok(new { Token = token, Message = "Success" });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(new { Token = "", Message = "Logged Out" });
        }

        private async Task<User?> ValidateUser(LoginCredentials credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.Username);

            if(user != null)
            {
                var passwordVerification = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, credentials.Password);
                return passwordVerification == PasswordVerificationResult.Failed ? null : user;
            }
            return null;
        }

        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
            }),

                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
