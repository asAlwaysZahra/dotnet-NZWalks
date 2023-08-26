using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult.Succeeded)
            {
                // add roles to user
                if (request.Roles != null && request.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, request.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully! Please login.");
                    }
                }
            }

            return BadRequest("There is a problem in your request, idk what!");
        }
    }
}
