using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository,
            ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.logger = logger;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Username);

            if (user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

                if (checkPassword)
                {
                    // get roles
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // create token
                        string token = tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = token,
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password wrong.");

        }
    }
}
