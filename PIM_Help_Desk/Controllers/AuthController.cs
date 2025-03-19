using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;
using PIM_Help_Desk.Services.AuthService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PIM_Help_Desk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            return Ok(await _authService.Register(request));        
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            return Ok(await _authService.Login(request));
        }


    }
}
