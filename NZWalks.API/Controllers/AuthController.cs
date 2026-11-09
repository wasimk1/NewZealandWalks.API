using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityresult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityresult.Succeeded)
            {
                //Add roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityresult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityresult.Succeeded)
                    {
                        return Ok("User registerd, Please login");
                    }
                }
            }
            return BadRequest("Somthing went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var useremail = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (useremail != null)
            {
                var checkuser = await userManager.CheckPasswordAsync(useremail, loginRequestDto.Password);
                if (checkuser)
                {
                    var roles = await userManager.GetRolesAsync(useremail);
                    if (roles != null)
                    {
                        //Create JwtToken
                        var jwtToken = tokenRepository.CreateJWTToken(useremail, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken=jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password incorrect");
        }
    }
}
